﻿﻿using System;
 using System.Collections.Generic;

 namespace DroneController
{
  public class Flyer
  {
    public static void Main(string[] args)
    {
      if (args.Length < 3)
      {
        Console.WriteLine("ERROR: Please specify a drone IP, port, and desired missions.\n" +
          "Examples:\n" +
          "./DroneController 127.0.0.1 8889 1\n" +
          "./DroneController 127.0.0.1 8889 1 3\n" +
          "./DroneController 127.0.0.1 8889 1 2 3\n");
        return;
      }

      string droneIP = args[0]; // 2130706433
      int dronePort = int.Parse(args[1]);
      List<int> desiredMissions = new List<int>();
      for (int i = 2; i < args.Length; ++i)
        desiredMissions.Add(int.Parse(args[i]));

      DroneSession droneSession = new DroneSession(ref droneIP, ref dronePort);

      foreach (int desiredMission in desiredMissions)
      {
        Console.WriteLine("--- Now executing mission " + desiredMission + " ---");
        droneSession.executeMission(desiredMission);
        Console.WriteLine();
      }
    }
  }
}
