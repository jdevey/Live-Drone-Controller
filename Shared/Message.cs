using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
//using DroneController;
using Shared.MessageTypes;
using Shared.MessageTypes.Maneuvers;

namespace Shared
{
	public abstract class Message
	{
		private readonly string messageText;

		protected Message(string messageText)
		{
			this.messageText = messageText;
		}

//		private static List<string> splitArgs(string[] words)
//		{
//			List<string> args = new List<string>();
//			for (uint i = 1; i < words.Length; ++i)
//			{
//				args.Add(words[i]);
//			}
//
//			return args;
//		}

		public virtual string getMessageText()
		{
			return messageText;
		}
	}
}