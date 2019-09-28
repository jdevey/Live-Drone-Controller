using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading; 

namespace DroneController
{
	public class MyUDP
	{
		private readonly HashSet<string> validCommands = new HashSet<string>()
		{
			"command",
			"takeoff", 
			"land",
			"up",
			"down",
			"left",
			"right",
			"forward",
			"back",
			"cw",
			"ccw",
			"flip",
			"go",
			"stop"
		};
		
		protected readonly string Ip;
		protected readonly int remotePort;
		protected readonly int localPort;

		private readonly UdpClient droneUDPClient;
		private IPEndPoint remoteIPEndPoint;

		private bool isInErrorState;
		private bool isHome;

		private readonly Thread threadLoop;

		public MyUDP(string Ip_, int remotePort_, int localPort_, bool isHome_)
		{
			this.Ip = Ip_;
			this.remotePort = remotePort_;
			this.localPort = localPort_;
			isHome = isHome_;
			
			threadLoop = new Thread(udpLoop);

			try
			{
				droneUDPClient = new UdpClient(this.Ip, this.localPort)
					{Client = {SendTimeout = 3000, ReceiveTimeout = 3000 }};
				remoteIPEndPoint = new IPEndPoint(IPAddress.Parse(this.Ip), this.remotePort);
				// if (!isHome) droneUDPClient.Connect(remoteIPEndPoint);
			}
			catch (Exception e)
			{
				Console.WriteLine("ERROR: Invalid IP or ports.");
				Console.WriteLine(e);
				setErrorState(true);
			}
		}

		public void start()
		{
			threadLoop.Start();
		}

		void udpLoop() {
			while(true)
			{
				byte[] dataBuffer = droneUDPClient.Receive(ref remoteIPEndPoint);
				string messageReceived = Encoding.UTF8.GetString(dataBuffer);
				string commandType = messageReceived.Split(null)[0];
				bool validCommand = validCommands.Contains(commandType);
				byte[] messageToSend = Encoding.UTF8.GetBytes(validCommand ? "ok" : "error");

				Console.WriteLine(Encoding.ASCII.GetString(dataBuffer, 0, dataBuffer.Length));
				
				droneUDPClient.Send(messageToSend, messageToSend.Length, remoteIPEndPoint);
			}
		}
		
		public void sendMessage(string msg)
		{
			byte[] msgBytes = Encoding.UTF8.GetBytes(msg);
			Console.WriteLine("I'm just gonna send it!!!" + msg);
			droneUDPClient.Send(msgBytes, msgBytes.Length);//, remoteIPEndPoint);
		}

		public string getResponse()
		{
			uint numRetries = 3;

			while (numRetries-- > 0)
			{
				try
				{
					byte[] receiveBytes = droneUDPClient.Receive(ref remoteIPEndPoint);
					if (receiveBytes.Length > 0)
					{
						if (Encoding.UTF8.GetString(receiveBytes) == "error")
						{
							Console.WriteLine("ERROR: Drone has encountered an error state.");
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

			Console.WriteLine("ERROR: Timed out. Failed to receive from drone.");
			setErrorState(true);
			return "";
		}
		
		public void closeClient()
		{
			threadLoop.Join();
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
