using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Shared;
using Shared.MessageTypes;
using Shared.MessageTypes.Responses;

namespace DroneController
{
	public class ControllerComm : UDPCommBase
	{
		private IPEndPoint localIpEndPoint;
		private IPEndPoint commandIpEndPoint;
		private IPEndPoint telloStateIpEndPoint;

		private readonly UdpClient droneUDPClient;
		private readonly UdpClient stateReceiver;

		public ControllerComm(
			string droneIp = DefaultConstants.DEFAULT_DRONE_IP,
			int localPort = DefaultConstants.DEFAULT_LOCAL_PORT, 
			int commandPort = DefaultConstants.DEFAULT_COMMAND_PORT,
			int telloStatePort = DefaultConstants.DEFAULT_TELLO_STATE_PORT,
			int timeout = DefaultConstants.DEFAULT_TIMEOUT,
			uint maxRetries = DefaultConstants.DEFAULT_MAX_RETRIES)
				: base(droneIp, localPort, commandPort, telloStatePort, timeout, maxRetries)
		{

			try
			{
				localIpEndPoint = new IPEndPoint(IPAddress.Parse(DefaultConstants.LOCALHOST), localPort);
				commandIpEndPoint = new IPEndPoint(IPAddress.Parse(droneIp), commandPort);
				telloStateIpEndPoint = new IPEndPoint(IPAddress.Parse(DefaultConstants.LOCALHOST), telloStatePort);

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
		}

//		public void sendMessage(string msg)
//		{
//			byte[] msgBytes = Encoding.UTF8.GetBytes(msg);
//			droneUDPClient.Send(msgBytes, msgBytes.Length, commandIpEndPoint);
//		}
//
//		public string getResponse()
//		{
//			uint numRetries = retr;
//
//			while (numRetries-- > 0)
//			{
//				try
//				{
//					byte[] receiveBytes = droneUDPClient.Receive(ref commandIpEndPoint);
//					if (receiveBytes.Length > 0)
//					{
//						if (Encoding.UTF8.GetString(receiveBytes) == "error")
//						{
//							Console.WriteLine("ERROR: Drone has encountered an error state.");
//							setErrorState(true);
//						}
//
//						string resp = Utils.decodeBytes(receiveBytes);
//						return resp;
//					}
//				}
//				catch (Exception e)
//				{
//					Console.WriteLine(e.Message);
//					return "";
//				}
//			}
//
//			Console.WriteLine("ERROR: Timed out. Failed to receive from drone.");
//			setErrorState(true);
//			return "";
//		}

//		public void stateLoop()
//		{
//			while (getIsCommunicationLive())
//			{
//				byte[] receiveBytes = stateReceiver.Receive(ref commandIpEndPoint);
//				string msg = Encoding.UTF8.GetString(receiveBytes);
//				// Console.WriteLine(msg);
//			}
//		}

		public void startConnection()
		{
			sendMessage(Command.getKeyword(), droneUDPClient, commandIpEndPoint);

			string msg = getResponse(droneUDPClient, commandIpEndPoint);

			if (msg == Ok.getKeyword())
			{
				Console.WriteLine("Connected to drone successfully at " + droneIp +
				                  ":" + commandPort + ".");
			}
			else
			{
				Console.WriteLine("ERROR: Failed to connect to drone at " + droneIp +
				                  ":" + commandPort + ".");
				setErrorState(true);
			}
		}

		public ref IPEndPoint getLocalIpEndPoint()
		{
			return ref localIpEndPoint;
		}

		public ref IPEndPoint getCommandIpEndPoint()
		{
			return ref commandIpEndPoint;
		}

		public ref IPEndPoint getTelloStateIpEndPoint()
		{
			return ref telloStateIpEndPoint;
		}
		
		public UdpClient getDroneCommClient()
		{
			return droneUDPClient;
		}

		public UdpClient getStateRcvClient()
		{
			return stateReceiver;
		}
	}
}
