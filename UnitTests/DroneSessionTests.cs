//﻿﻿using System;
//using NUnit.Framework;
//using DroneController;
//
//namespace UnitTests
//{
//	[TestFixture]
//	public class DroneSessionTests
//	{
//		[Test]
//		public void Main()
//		{
//			Server server = new Server(
//				// TestConstants.serverPort);
//				8891);
// 			server.start();
//
////			DroneSimulator droneSimulator = new DroneSimulator(
////				ref TestConstants.serverIp,
////				ref TestConstants.serverPort);
////			droneSimulator.start();
//			
////			UDPSocket s = new UDPSocket();
////			s.Server("127.0.0.1", 8889);
////			// s.Client("127.0.0.1", 8889);
////			s.start();
//
//			DroneSession droneSession = new DroneSession(
//				ref TestConstants.droneIp,
//				ref TestConstants.dronePort);
//			
//			Assert.IsFalse(droneSession.getUDPClient().getErrorState(), "Drone in error state.");
//			Assert.AreEqual(droneSession.getUDPClient(), droneSession.getUDPClient());
//			
//			droneSession.addMission(ref TestConstants.leftRightMission);
//			Assert.IsFalse(droneSession.getErrorState(), "Drone in error state.");
//
//			droneSession.addMission(ref TestConstants.badMission);
//			Assert.IsTrue(droneSession.getErrorState(), "Drone not in error state.");
//			droneSession.setErrorState(false);
//
//			droneSession.executeMission(1);
//			Assert.IsFalse(droneSession.getErrorState(), "Drone in error state.");
//
//			droneSession.executeMission(0);
//			Assert.IsTrue(droneSession.getErrorState(), "Drone not in error state.");
//			droneSession.setErrorState(false);
//
//			droneSession.executeMission(7);
//			Assert.IsTrue(droneSession.getErrorState(), "Drone not in error state.");
//			droneSession.setErrorState(false);
//
//			server.closeClient();
////			droneSimulator.closeClient();
//		}
//	}
//}