﻿﻿using System;
  using System.Collections.Generic;
  using System.Threading;
  using Shared;
  using Shared.MessageTypes;

  namespace DroneController
{
  public class Mission
  {
    private readonly List<Message> actionList;

    public Mission(List<Message> actionList_)
    {
      actionList = actionList_;
    }

    public void execute(ref ControllerComm controllerComm)
    {
      foreach (Message droneAction in actionList)
      {
        Console.WriteLine("Now executing action: " + droneAction.getMessageText());
        Type msgType = droneAction.GetType();
        if (msgType == typeof(SleepAction))
        {
          (droneAction as SleepAction).activate();
        }
        else if (msgType.IsSubclassOf(typeof(QueryBase)))
        {
          (droneAction as QueryBase).activate
            (controllerComm.getDroneCommClient(), controllerComm.getCommandIpEndPoint());
        }
        else if (msgType.IsSubclassOf(typeof(ManeuverBase)))
        {
          (droneAction as ManeuverBase).activate
            (controllerComm.getDroneCommClient(), controllerComm.getCommandIpEndPoint());
        }
      }
    }
  }
}
