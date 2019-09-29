//﻿using System;
//using System.Collections.Generic;
//using System.Threading;
//using DroneController;
//
//namespace UnitTests
//{
//	public class DroneSimulator : UDPBase
//	{
//		private readonly HashSet<string> validCommands = new HashSet<string>()
//		{
//			"command",
//			"takeoff",
//			"land",
//			"up",
//			"down",
//			"left",
//			"right",
//			"forward",
//			"back",
//			"cw",
//			"ccw",
//			"flip",
//			"go",
//			"stop"
//		};
//
//		private readonly Thread thread;
//
//		public DroneSimulator(ref string droneIP, ref int dronePort) :
//			base(ref droneIP, ref dronePort)
//		{
//			thread = new Thread(udpLoop);
//		}
//		
//		public void start()
//		{
//			thread.Start();
//			Thread.Sleep(1000); // Give the thread time to start before sending a message
//		}
//
//		private void udpLoop()
//		{
//			while (true)
//			{
//				string messageSent = getResponse();
//				string commandType = messageSent.Split(null)[0];
//				bool validCommand = validCommands.Contains(commandType);
//				sendMessage(validCommand ? "ok" : "error");
//			}
//		}
//
//
//		public void closeClient()
//		{
//			thread.Join();
//		}
//	}
//}