﻿﻿using System;

namespace DroneController
{
  public class Maneuver : DroneAction
  {
    public Maneuver(string stringRep_)
    {
      stringRep = stringRep_;
    }

    public override void execute(ref DroneUDPClient droneUdpClient)
    {
      Console.WriteLine("Now executing \"" + stringRep + "\" command.");
      droneUdpClient.sendMessage(stringRep);
      string response = droneUdpClient.getResponse();
      if (response != "ok")
      {
        droneUdpClient.setErrorState(true);
      }
    }
  }
}
