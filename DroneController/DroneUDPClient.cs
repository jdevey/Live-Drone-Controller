using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Shared;

namespace DroneController
{
	public class DroneUDPClient
	{
		private const string LOCALHOST = "127.0.0.1";

		protected readonly string droneIp;

		protected readonly int localPort;
		protected readonly int commandPort;
		protected readonly int telloStatePort;

		private IPEndPoint localIpEndPoint;
		private IPEndPoint commandIpEndPoint;
		private IPEndPoint telloStateIpEndPoint;

		private readonly UdpClient droneUDPClient;
		private readonly UdpClient stateReceiver;

		private bool controllerRunning = true;

		private Thread thread;

		private uint maxRetries;
		private bool isInErrorState;

		public DroneUDPClient(string droneIp = "127.0.0.1", int localPort = 8891,
				int commandPort = 8889, int telloStatePort = 8890, int timeout = 3000, uint maxRetries = 3)
			// TODO implement max retries and customizable timeouts
		{
			this.droneIp = droneIp;
			this.localPort = localPort;
			this.commandPort = commandPort;
			this.telloStatePort = telloStatePort;
			this.maxRetries = maxRetries;

			try
			{
				localIpEndPoint = new IPEndPoint(IPAddress.Parse(LOCALHOST), localPort);
				commandIpEndPoint = new IPEndPoint(IPAddress.Parse(droneIp), commandPort);
				telloStateIpEndPoint = new IPEndPoint(IPAddress.Parse(LOCALHOST), telloStatePort);

				droneUDPClient = new UdpClient(localIpEndPoint)
					{Client = {SendTimeout = timeout, ReceiveTimeout = timeout}};

				stateReceiver = new UdpClient(telloStateIpEndPoint)
					{Client = {SendTimeout = timeout, ReceiveTimeout = timeout}};
			}
			catch (Exception e)
			{
				Console.WriteLine("ERROR: Invalid IP or port.");
				Console.WriteLine(e);
				setErrorState(true);
			}

			thread = new Thread(stateLoop);
		}

		public void sendMessage(string msg)
		{
			byte[] msgBytes = Encoding.UTF8.GetBytes(msg);
			droneUDPClient.Send(msgBytes, msgBytes.Length, commandIpEndPoint);
		}

		public string getResponse()
		{
			uint numRetries = maxRetries;

			while (numRetries-- > 0)
			{
				try
				{
					byte[] receiveBytes = droneUDPClient.Receive(ref commandIpEndPoint);
					if (receiveBytes.Length > 0)
					{
						if (Encoding.UTF8.GetString(receiveBytes) == "error")
						{
							Console.WriteLine("ERROR: Drone has encountered an error state.");
							setErrorState(true);
						}

						string resp = Utils.decodeBytes(receiveBytes);
						Console.WriteLine("Hell yeah brothas " + resp);
						return resp;
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					return "";
				}
			}

			Console.WriteLine("ERROR: Timed out. Failed to receive from drone.");
			setErrorState(true);
			return "";
		}

		public void stateLoop()
		{
			while (controllerRunning)
			{
				byte[] receiveBytes = stateReceiver.Receive(ref commandIpEndPoint);
				string msg = Encoding.UTF8.GetString(receiveBytes);
				// Console.WriteLine(msg);
			}
		}

		public void startConnection()
		{
			sendMessage("command");

			string msg = getResponse();

			if (msg == "ok")
			{
				Console.WriteLine("Connected to drone successfully at " + droneIp +
				                  ":" + commandPort + ".");
				thread.Start();
			}
			else
			{
				Console.WriteLine("ERROR: Failed to connect to drone at " + droneIp +
				                  ":" + commandPort + ".");
				setErrorState(true);
			}
		}

		public void stop()
		{
			controllerRunning = false;
			// thread.Abort();
			thread.Join();
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

//namespace DroneController
//{
//	public class DroneUDPClient : UDPBase
//	{
//		public DroneUDPClient(ref string droneIP, ref int dronePort) :
//			base(ref droneIP, ref dronePort)
//		{}
//
//		public void startConnection()
//		{
//			sendMessage("command");
//
//			string msg = getResponse();
//
//			if (msg == "ok")
//			{
//				Console.WriteLine("Connected to drone successfully at " + droneIP +
//				                  ":" + dronePort + ".");
//			}
//			else
//			{
//				Console.WriteLine("ERROR: Failed to connect to drone at " + droneIP +
//				                  ":" + dronePort + ".");
//				setErrorState(true);
//			}
//		}
//	}
//}