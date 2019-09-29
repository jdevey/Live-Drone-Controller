//﻿using System;
//using System.Threading;
//using NUnit.Framework;
//using DroneController;
//using UnitTests;
//
//namespace DroneController
//{
//	[TestFixture]
//	public class SleepActionTests
//	{
//		[Test]
//		public void Main()
//		{
//			DroneUDPClient udpClient1 = new DroneUDPClient(
//				ref TestConstants.droneIp,
//				ref TestConstants.dronePort);
//			udpClient1.startConnection();
//				
//			Assert.IsFalse(udpClient1.getErrorState(), "Drone in error state.");
//			
//			SleepAction sleepAction = new SleepAction("sleep 1000", 1000);
//			Assert.IsFalse(udpClient1.getErrorState(), "Drone in error state.");
//		}
//	}
//}