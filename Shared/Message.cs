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
		private bool valid = true;

		protected Message(string messageText)
		{
			this.messageText = messageText; // TODO fix the messageText shit
		}
		
//		public virtual string getKeyword()
//		{
//			return "";
//		}

		private static List<string> splitArgs(string[] words)
		{
			List<string> args = new List<string>();
			for (uint i = 1; i < words.Length; ++i)
			{
				args.Add(words[i]);
			}

			return args;
		}

		public static Message decode(string text)
		{
			if (!String.IsNullOrEmpty(text))
			{
				return MessageFactory.createMessage(text);
			}

			return null;
		}

		public byte[] encode()
		{
			return Utils.encodeString(messageText);
		}

//		public abstract string getMessageType();

		public virtual string getMessageText()
		{
			return messageText;
		}

		public bool isValid()
		{
			return valid;
		}

		protected void setIsValid(bool newValid)
		{
			this.valid = newValid;
		}
	}
}