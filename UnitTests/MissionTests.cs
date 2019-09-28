﻿using System;
using System.Collections.Generic;
using DroneController;
using NUnit.Framework;

namespace UnitTests
{
	[TestFixture]
	public class MissionTests
	{
		[Test]
		public void Main()
		{
			DroneUDPClient udpClient1 = new DroneUDPClient(
				ref TestConstants.droneIp,
				ref TestConstants.dronePort);
			udpClient1.startConnection();
				
			Assert.IsFalse(udpClient1.getErrorState(), "Drone in error state.");

			List<DroneAction> actions = stringToAction(ref TestConstants.leftRightMission);
			Assert.AreEqual(actions.Count, 6);
			Mission mission = new Mission(ref actions);
			mission.execute(ref udpClient1);
			Assert.IsFalse(udpClient1.getErrorState(), "Drone in error state.");
		}

		List<DroneAction> stringToAction(ref string[] actionList)
		{
			List<DroneAction> droneActions = new List<DroneAction>();
			foreach (string action in actionList)
			{
				if (action.StartsWith("sleep"))
				{
					int sleepMilliseconds = 1;
					try
					{
						sleepMilliseconds = int.Parse(action.Split(null)[1]);
					}
					catch (Exception e)
					{
						Console.WriteLine("ERROR: Failed to parse sleep seconds.");
						Console.WriteLine(e.Message);
						Assert.Fail();
					}
					droneActions.Add(new SleepAction(action, sleepMilliseconds));
				}
				else
				{
					droneActions.Add(new Maneuver(action));
				}
			}

			return droneActions;
		}
	}
}