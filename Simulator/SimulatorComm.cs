using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Shared;

namespace Simulator
{
	public class SimulatorComm : UDPCommBase
	{
		private IPEndPoint localIpEndPoint;
		private IPEndPoint commandIpEndPoint;
		private IPEndPoint telloStateIpEndPoint;
		
		private readonly UdpClient simUDPClient;

		public SimulatorComm(
			int commandPort = DefaultConstants.DEFAULT_COMMAND_PORT,
			int telloStatePort = DefaultConstants.DEFAULT_TELLO_STATE_PORT,
			int timeout = DefaultConstants.DEFAULT_TIMEOUT,
			uint maxRetries = DefaultConstants.DEFAULT_MAX_RETRIES)
				: base(
					commandPort: commandPort,
					telloStatePort: telloStatePort,
					timeout: timeout,
					maxRetries: maxRetries)
		{
			try
			{
				commandIpEndPoint = new IPEndPoint(IPAddress.Parse(DefaultConstants.LOCALHOST), this.commandPort);

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

		public UdpClient getUdpClient()
		{
			return simUDPClient;
		}
	}
}