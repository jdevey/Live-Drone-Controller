﻿﻿using System;
using System.Threading;

namespace Shared.MessageTypes.Enhancements
{
  public class SleepAction : Message
  {
    private readonly int sleepTime;

    public SleepAction(string text) : base(text)
    {
      string[] args = text.Split(' ');
      sleepTime = int.Parse(args[1]);
    }
		
    public static string getKeyword()
    {
      return "sleep";
    }

    public void activate()
    {
      Thread.Sleep(sleepTime);
    }
  }
}
