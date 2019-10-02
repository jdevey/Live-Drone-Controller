using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Shared;
using Shared.MessageTypes;
using Shared.MessageTypes.Queries;
using Shared.MessageTypes.Responses;

namespace Simulator
{
	public class Simulator
	{
		private const int SLEEP_TIME_MS = 100;
		
		private const string LOCALHOST = "127.0.0.1";
		private const string METAHOST = "0.0.0.0";

		protected readonly string remoteIp;

		protected readonly int localPort;
		protected readonly int commandPort;
		protected readonly int telloStatePort;

		private IPEndPoint localIpEndPoint;
		private IPEndPoint commandIpEndPoint;
		private IPEndPoint telloStateIpEndPoint;

		private readonly UdpClient simUDPClient;

		private DroneState state;

		private Thread stateBroadcastThread;
		private Thread serverThread;

		private uint maxRetries;
		private bool isInErrorState;

		public Simulator(string remoteIp = "127.0.0.1", int commandPort = 8889,
				int telloStatePort = 8890, int timeout = 3000, uint maxRetries = 3)
		{
			state = new DroneState();
			
			this.remoteIp = remoteIp;
			this.commandPort = commandPort;
			this.telloStatePort = telloStatePort;
			this.maxRetries = maxRetries;

			try
			{
				commandIpEndPoint = new IPEndPoint(IPAddress.Parse(LOCALHOST), commandPort);
				
				//simUDPClient = new UdpClient(commandPort)
					simUDPClient = new UdpClient(localIpEndPoint)
					{Client = {SendTimeout = timeout, ReceiveTimeout = timeout}};
				
					localIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
				// We'll learn the drone controller's ip address and port when we receive
				// a datagram. Until then, we have to wait.
			}
			catch (Exception e)
			{
				Console.WriteLine("ERROR: Invalid IP or port.");
				Console.WriteLine(e);
				setErrorState(true);
			}

			stateBroadcastThread = new Thread(stateBroadcastLoop);
			serverThread = new Thread(serverLoop);
		}

		public void start()
		{
			byte[] bytes = simUDPClient.Receive(ref localIpEndPoint);
			string msg = Utils.decodeBytes(bytes);
			if (msg == Command.getKeyword())
			{
				telloStateIpEndPoint = new IPEndPoint(localIpEndPoint.Address, telloStatePort);
				stateBroadcastThread.Start();
				serverThread.Start();
				
				state.setInCommandMode(true);
				sendMessage(Ok.getKeyword());
			}
		}

		public void sendMessage(string msg)
		{
			byte[] msgBytes = Utils.encodeString(msg);
			simUDPClient.Send(msgBytes, msgBytes.Length, localIpEndPoint);
		}

		public string getResponse()
		{
			uint numRetries = maxRetries;

			while (numRetries-- > 0)
			{
				try
				{
					byte[] receiveBytes = simUDPClient.Receive(ref localIpEndPoint);
					if (receiveBytes.Length > 0)
					{
						if (Encoding.UTF8.GetString(receiveBytes) == Error.getKeyword())
						{
							Console.WriteLine("ERROR: Simulator has encountered an error state.");
							setErrorState(true);
						}

						return Encoding.UTF8.GetString(receiveBytes);
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					return "";
				}
			}

			Console.WriteLine("ERROR: Timed out. Failed to receive from controller.");
			setErrorState(true);
			return "";
		}

		private string getQueryResult(Type msgType)
		{
			string simResponse = Error.getKeyword();
			if (msgType == typeof(TimeQuery))
			{
				simResponse = Utils.formatDouble(state.getCurrentFlightTime());
			}
			else if (msgType == typeof(BatteryQuery))
			{
				simResponse = state.getBatteryPercentage().ToString();
			}
			else if (msgType == typeof(SpeedQuery))
			{
				simResponse = Utils.pythagorean3D(state.getSpeedX(), state.getSpeedY(), state.getSpeedZ());
			}

			return simResponse;
		}

		private void serverLoop()
		{
			while (true)
			{
				string resp = getResponse();
				Message msg = MessageFactory.createMessage(resp);
				if (msg == null)
				{
					Console.WriteLine("ERROR: Simulator received invalid message.");
					sendMessage(Error.getKeyword());
					continue;
				}
				
				Type msgType = msg.GetType();
				string simResponse = Error.getKeyword();

				// Messages from the user should either be "command", a maneuver, or a query
				if (msgType == typeof(Command) ||
				    msgType.IsSubclassOf(typeof(QueryBase)) ||
				    msgType.IsSubclassOf(typeof(ManeuverBase)))
				{
					sendMessage(simResponse);
					if (msgType == typeof(Command)) // Command
					{
						simResponse = Ok.getKeyword();
					}
					else if (msgType.IsSubclassOf(typeof(ManeuverBase))) // Maneuvers
					{
						simResponse = Ok.getKeyword();
						try
						{
							(msg as ManeuverBase).updateState(state);
						}
						catch (NullReferenceException e)
						{
							Console.WriteLine("ERROR: Attempted to update state via a class that lacks such a function.");
							Console.WriteLine(e);
						}
					}
					else if (msgType.IsSubclassOf(typeof(QueryBase))) // Queries
					{
						simResponse = getQueryResult(msgType);
					}
				}
				else // User has sent an invalid message
				{
					Console.WriteLine("ERROR: Simulator has encountered an invalid message from user: " + resp);
					setErrorState(true);
					simResponse = Error.getKeyword();
				}
				sendMessage(simResponse);
			}
		}

		private void stateBroadcastLoop()
		{
			while (true)
			{
				string stateString = Status.getMessageTextFromState(state);
				byte[] bytes = Utils.encodeString(stateString);
				Console.WriteLine("Sending state: " + stateString);
				simUDPClient.Send(bytes, bytes.Length, telloStateIpEndPoint);
				Thread.Sleep(SLEEP_TIME_MS);
			}
		}

		public void stopSimulator()
		{
			stateBroadcastThread.Join();
		}

		public void setErrorState(bool errorState)
		{
			isInErrorState = errorState;
		}

		public bool getErrorState()
		{
			return isInErrorState;
		}
	}
}