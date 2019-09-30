﻿﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DroneController
{
	public class DroneUDPClient : UDPBase
	{
		public DroneUDPClient(ref string droneIP, ref int dronePort) :
			base(ref droneIP, ref dronePort)
		{}

		public void startConnection()
		{
			sendMessage("command");

			string msg = getResponse();

			if (msg == "ok")
			{
				Console.WriteLine("Connected to drone successfully at " + droneIP +
				                  ":" + dronePort + ".");
			}
			else
			{
				Console.WriteLine("ERROR: Failed to connect to drone at " + droneIP +
				                  ":" + dronePort + ".");
				setErrorState(true);
			}
		}
	}
}