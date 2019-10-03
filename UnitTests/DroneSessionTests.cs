//﻿﻿using System;
//using NUnit.Framework;
//using DroneController;
//
//namespace UnitTests
//{
//	[TestFixture]
//	public class controllerTests
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
//			controller controller = new controller(
//				ref TestConstants.droneIp,
//				ref TestConstants.dronePort);
//			
//			Assert.IsFalse(controller.getUDPClient().getErrorState(), "Drone in error state.");
//			Assert.AreEqual(controller.getUDPClient(), controller.getUDPClient());
//			
//			controller.addMission(ref TestConstants.leftRightMission);
//			Assert.IsFalse(controller.getErrorState(), "Drone in error state.");
//
//			controller.addMission(ref TestConstants.badMission);
//			Assert.IsTrue(controller.getErrorState(), "Drone not in error state.");
//			controller.setErrorState(false);
//
//			controller.executeMission(1);
//			Assert.IsFalse(controller.getErrorState(), "Drone in error state.");
//
//			controller.executeMission(0);
//			Assert.IsTrue(controller.getErrorState(), "Drone not in error state.");
//			controller.setErrorState(false);
//
//			controller.executeMission(7);
//			Assert.IsTrue(controller.getErrorState(), "Drone not in error state.");
//			controller.setErrorState(false);
//
//			server.closeClient();
////			droneSimulator.closeClient();
//		}
//	}
//}