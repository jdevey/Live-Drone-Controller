﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DroneController
{
	public abstract class OldUDPBase
	{
		protected readonly string droneIP;
		protected readonly int dronePort;

		private readonly UdpClient droneUDPClient;
		private IPEndPoint remoteIPEndPoint;

		private bool isInErrorState;

		public OldUDPBase(ref string droneIP_, ref int dronePort_)
		{
			droneIP = droneIP_;
			dronePort = dronePort_;

			try
			{
				remoteIPEndPoint = new IPEndPoint(IPAddress.Parse(droneIP), dronePort);
				droneUDPClient = new UdpClient(droneIP, dronePort)
					{Client = {SendTimeout = 3000, ReceiveTimeout = 3000 }};
				int SIO_UDP_CONNRESET = -1744830452;
				droneUDPClient.Client.IOControl(
					(IOControlCode)SIO_UDP_CONNRESET,
					new byte[] { 0, 0, 0, 0 },
					null
				);

				droneUDPClient.Connect(droneIP, dronePort);
			}
			catch (Exception e)
			{
				Console.WriteLine("ERROR: Invalid IP or port.");
				Console.WriteLine(e);
				setErrorState(true);
			}
		}
		
		public void sendMessage(string msg)
		{
			byte[] msgBytes = Encoding.UTF8.GetBytes(msg);
			droneUDPClient.Send(msgBytes, msgBytes.Length);
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
							Console.WriteLine("ERROR: UDP base has encountered an error state.");
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