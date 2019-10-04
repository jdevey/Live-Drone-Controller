﻿using System;
using System.Collections.Generic;
using DroneController;
 using NUnit.Framework;
 using Shared;

 namespace UnitTests
{
	[TestFixture]
	public class MissionTests
	{
		[Test]
		public void Main()
		{
			Simulator.Simulator simulator = TestingUtils.generateSim(TestingUtils.ports[2]);
			simulator.start();
			Controller controller = TestingUtils.generateContr(TestingUtils.ports[2]);
			controller.getUDPClient().startConnection();

			Assert.IsFalse(simulator.getErrorState(), "Simulator in error state.");
			Assert.IsFalse(controller.getErrorState(), "Controller in error state.");

			List<Message> actions = constToMessages(TestingUtils.flipBatteryMission);
			Assert.AreEqual(actions.Count, 8);
			Mission mission = new Mission(actions);
			
			DroneState state = new DroneState();
			
			mission.execute(controller.getUDPClient(), controller.getUDPClient().getCommandIpEndPoint(), state);
			Assert.IsFalse(controller.getErrorState(), "Controller in error state.");
			Assert.IsFalse(controller.getUDPClient().getErrorState(), "Udp client in error state.");
			
			state.setBatteryPercentage(40);
			mission.execute(controller.getUDPClient(), controller.getUDPClient().getCommandIpEndPoint(), state);
			Assert.IsFalse(controller.getErrorState(), "Controller in error state.");
			Assert.IsFalse(controller.getUDPClient().getErrorState(), "Udp client in error state.");
			
			state.setBatteryPercentage(5);
			mission.execute(controller.getUDPClient(), controller.getUDPClient().getCommandIpEndPoint(), state);
			//Console.WriteLine("Ctrl error state: " + controller.getErrorState());
			Assert.IsTrue(controller.getUDPClient().getErrorState(), "Controller not in error state.");
			controller.getUDPClient().setErrorState(false);
			state.setBatteryPercentage(100);
			
			state.setHighTemperature(80);
			mission.execute(controller.getUDPClient(), controller.getUDPClient().getCommandIpEndPoint(), state);
			Assert.IsTrue(controller.getUDPClient().getErrorState(), "Controller not in error state.");
			
			controller.stop();
			simulator.stop();
		}

		List<Message> constToMessages(string[] actionList)
		{
			List<Message> droneActions = new List<Message>();
			foreach (string action in actionList)
			{
					droneActions.Add(MessageFactory.createMessage(action));
			}

			return droneActions;
		}
	}
}