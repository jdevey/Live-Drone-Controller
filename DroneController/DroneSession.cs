﻿﻿using System;
using System.Collections.Generic;

namespace DroneController
{
  public class DroneSession
  {
    private const string START_MISSION_SLEEP_TIME = "200";
    private const string DEFAULT_SLEEP_TIME = "3000";

    private static string[] rotateMission =
    {
      "sleep " + START_MISSION_SLEEP_TIME,
      "takeoff",
      "sleep " + DEFAULT_SLEEP_TIME,
      "cw 360",
      "sleep " + DEFAULT_SLEEP_TIME,
      "land"
    };

    private static string[] leftRightMission =
    {
      "sleep " + START_MISSION_SLEEP_TIME,
      "takeoff",
      "sleep " + DEFAULT_SLEEP_TIME,
      "left 50",
      "right 50",
      "sleep " + DEFAULT_SLEEP_TIME,
      "land"
    };

    private static string[] squareMission =
    {
      "sleep " + START_MISSION_SLEEP_TIME,
      "takeoff",
      "sleep " + DEFAULT_SLEEP_TIME,
      "right 50",
      "sleep " + DEFAULT_SLEEP_TIME,
      "cw 90",
      "sleep " + DEFAULT_SLEEP_TIME,
      "right 50",
      "sleep " + DEFAULT_SLEEP_TIME,
      "cw 90",
      "sleep " + DEFAULT_SLEEP_TIME,
      "right 50",
      "sleep " + DEFAULT_SLEEP_TIME,
      "cw 90",
      "sleep " + DEFAULT_SLEEP_TIME,
      "right 50",
      "sleep " + DEFAULT_SLEEP_TIME,
      "cw 90",
      "sleep " + DEFAULT_SLEEP_TIME,
      "land"
    };

    private DroneUDPClient droneDroneUdpClient;

    private static readonly List<Mission> missionList = new List<Mission>();

    private static bool isInErrorState;

    public DroneSession(ref string droneIP, ref int dronePort)
    {
      droneDroneUdpClient = new DroneUDPClient();

      addMission(ref rotateMission);
      addMission(ref leftRightMission);
      addMission(ref squareMission);
      
      droneDroneUdpClient.startConnection();
    }

    public void addMission(ref string[] actionList)
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
            setErrorState(true);
          }
          droneActions.Add(new SleepAction(action, sleepMilliseconds));
        }
        else
        {
          droneActions.Add(new Maneuver(action));
        }
      }
      missionList.Add(new Mission(ref droneActions));
    }

    public void executeMission(int missionNumber)
    {
      if (missionNumber < 1 || missionNumber > missionList.Count)
      {
        Console.WriteLine("ERROR: Invalid mission number. There are currently "
          + missionList.Count + " missions available.");
        setErrorState(true);
        return;
      }
      missionList[missionNumber - 1].execute(ref droneDroneUdpClient);
    }

    public DroneUDPClient getUDPClient()
    {
      return droneDroneUdpClient;
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
