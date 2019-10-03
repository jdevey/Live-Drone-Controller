﻿﻿using System;
 using System.Collections.Generic;
  using Shared;
  using Simulator;

 namespace DroneController
{
  public class Flyer
  {
    public static void Main(string[] args)
    {
      if (args.Length < 3)
      {
        Console.WriteLine("ERROR: Please specify a drone IP, command port, state port, desired timeout (ms)," +
        " maximum number of retries, and desired missions. If a value is left blank," + 
        " a reasonable default will be used instead.\n" +
          "Examples:\n" +
          "./DroneController ip=127.0.0.1 cmdport=8889 stateport=8890 timeout=2000 retries=5 missions=1\n" +
          "./DroneController stateport=8890 timeout=2000 retries=5 missions=1,3\n" +
          "./DroneController ip=127.0.0.1 cmdport=8889 stateport=8890 missions=1,2,3\n");
        return;
      }

      string droneIP = DefaultConstants.LOCALHOST;
      int cmdport = DefaultConstants.DEFAULT_COMMAND_PORT;
      int stateport = DefaultConstants.DEFAULT_TELLO_STATE_PORT;
      int timeout = DefaultConstants.DEFAULT_TIMEOUT;
      uint retries = DefaultConstants.DEFAULT_MAX_RETRIES;
      List<int> desiredMissions = new List<int>();

      foreach (string param in args)
      {
        if (param.StartsWith("ip"))
        {
          droneIP = param.Split('=')[1];
        }
        else if (param.StartsWith("cmdport"))
        {
          cmdport = int.Parse(param.Split('=')[1]);
        }
        else if (param.StartsWith("stateport"))
        {
          stateport = int.Parse(param.Split('=')[1]);
        }
        else if (param.StartsWith("timeout"))
        {
          timeout = int.Parse(param.Split('=')[1]);
        }
        else if (param.StartsWith("retries"))
        {
          retries = uint.Parse(param.Split('=')[1]);
        }
        else if (param.StartsWith("missions"))
        {
          string[] missionStrings = param.Split('=')[1].Split(',');
          foreach (string mission in missionStrings)
          {
            desiredMissions.Add(int.Parse(mission));
          }
        }
        else
        {
          Console.WriteLine("WARNING: Invalid commandline argument found: " + param);
        }
      }

      Simulator.Simulator simulator = new Simulator.Simulator(
        commandPort: cmdport,
        telloStatePort: stateport,
        timeout: timeout,
        maxRetries: retries
      );
      simulator.start();

      Controller controller = new Controller(
        droneIp: droneIP,
        commandPort: cmdport,
        telloStatePort: stateport,
        timeout: timeout,
        maxRetries: retries
      );

      foreach (int desiredMission in desiredMissions)
      {
        Console.WriteLine("--- Now executing mission " + desiredMission + " ---");
        controller.executeMission(desiredMission);
        Console.WriteLine();
      }

      controller.stop();
      simulator.stop();
    }
  }
}
