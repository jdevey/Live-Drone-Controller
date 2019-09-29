﻿﻿using System;
  using System.Collections.Generic;
  using System.Threading;

namespace DroneController
{
  public class SleepAction : DroneAction
  {
    private static readonly HashSet<string> KEYWORD_LIST = new HashSet<string>
    {
      "sleep"
    };
    // private string m_stringRep;
    private int sleepTime { get; }
    // int sleepTime;

    public SleepAction(string stringRep_, int sleepTime_)
    {
      stringRep = stringRep_;
      sleepTime = sleepTime_;
    }
    
//    public static string getKeyWord()
//    {
//      return "sleep";
//    }
    
    public static HashSet<string> getKeyWordList()
    {
      return KEYWORD_LIST;
    }

    //string DroneAction.stringRep
    //{
    //  get => m_stringRep;
    //  set => m_stringRep = value;
    //}

    public override void execute(ref DroneUDPClient droneUdpClient)
    {
      Console.WriteLine("Now executing \"" + stringRep + "\" command.");
      Thread.Sleep(sleepTime);
    }
  }
}
