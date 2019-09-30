﻿﻿using System.Collections.Generic;

namespace DroneController
{
  public class Mission
  {
    private readonly List<DroneAction> actionList;

    public Mission(ref List<DroneAction> actionList_)
    {
      actionList = actionList_;
    }

//    public void execute(ref DroneUDPClient droneUdpClient)
//    {
//      foreach (DroneAction droneAction in actionList)
//      {
//        droneAction.execute(ref droneUdpClient);
//      }
//    }
  }
}
