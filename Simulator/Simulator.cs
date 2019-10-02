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

		protected readonly int commandPort;
		protected readonly int telloStatePort;

		private IPEndPoint localIpEndPoint;
		private IPEndPoint commandIpEndPoint;
		private IPEndPoint telloStateIpEndPoint;

		private readonly UdpClient simUDPClient;
		private bool simulatorRunning = true;

		private readonly DroneState state;

		private readonly Thread stateBroadcastThread;
		private readonly Thread serverThread;

		private readonly uint maxRetries;
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
				commandIpEndPoint = new IPEndPoint(IPAddress.Parse(LOCALHOST), this.commandPort);

				//simUDPClient = new UdpClient(commandPort)
				simUDPClient = new UdpClient(commandIpEndPoint)
					{Client = {SendTimeout = timeout, ReceiveTimeout = timeout}};

				// udpSender = new UdpClient();
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
			serverThread = new Thread(setupServer);
		}

		public void start()
		{
			serverThread.Start();
		}

		public void setupServer()
		{
			byte[] bytes = simUDPClient.Receive(ref localIpEndPoint);
			string msg = Utils.decodeBytes(bytes);
			if (msg == Command.getKeyword())
			{
				telloStateIpEndPoint = new IPEndPoint(localIpEndPoint.Address, telloStatePort);
				stateBroadcastThread.Start();

				state.setInCommandMode(true);
				sendMessage(Ok.getKeyword());

				serverLoop();
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
					if (simulatorRunning)
					{
						Console.WriteLine("Encountered an error in simulator getResponse function.");
						Console.WriteLine(e.Message);
					}

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
			while (simulatorRunning)
			{
				Console.WriteLine("New loop!!!!");
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
					if (msgType == typeof(Command)) // Command
					{
						Console.WriteLine("got a command yay!");
						simResponse = Ok.getKeyword();
					}
					else if (msgType.IsSubclassOf(typeof(ManeuverBase))) // Maneuvers
					{
						Console.WriteLine("got a maneuver yay!");
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
						Console.WriteLine("got a query yay!");
						simResponse = getQueryResult(msgType);
					}
				}
				else // User has sent an invalid message
				{
					if (simulatorRunning)
					{
						Console.WriteLine("ERROR: Simulator has encountered an invalid message from user: " + resp);
						setErrorState(true);
					}

					simResponse = Error.getKeyword();
				}

				Console.WriteLine("Server sending! " + simResponse);
				sendMessage(simResponse);
			}
		}

		private void stateBroadcastLoop()
		{
			while (simulatorRunning)
			{
				string stateString = Status.getMessageTextFromState(state);
				byte[] bytes = Utils.encodeString(stateString);
				//Console.WriteLine("Sending state: " + stateString);
				simUDPClient.Send(bytes, bytes.Length, telloStateIpEndPoint);
				Thread.Sleep(SLEEP_TIME_MS);
			}
		}

		public void stop()
		{
			// fucking die
			// stateBroadcastThread.Abort();
			// serverThread.Abort();
			simulatorRunning = false;
			stateBroadcastThread.Join();
			serverThread.Join();
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