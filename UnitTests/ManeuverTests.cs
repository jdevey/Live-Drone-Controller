//﻿using System;
//using System.Threading;
//using NUnit.Framework;
//using DroneController;
//
//namespace UnitTests
//{
//	[TestFixture]
//	public class ManeuverTests
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
//			Maneuver maneuver1 = new Maneuver("takeoff");
//			maneuver1.execute(ref udpClient1);
//			Assert.IsFalse(udpClient1.getErrorState(), "Drone in error state.");
//			
//			Maneuver maneuver2 = new Maneuver("land");
//			maneuver2.execute(ref udpClient1);
//			Assert.IsFalse(udpClient1.getErrorState(), "Drone in error state.");
//			
//			Maneuver maneuver3 = new Maneuver("invalid maneuver");
//			maneuver3.execute(ref udpClient1);
//			Assert.IsTrue(udpClient1.getErrorState(), "Drone not in error state.");
//		}
//	}
//}