using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using DroneController;

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

		private static List< string > splitArgs(string[] words)
		{
			List<string> args = new List<string>();
			for (uint i = 1; i < words.Length; ++i)
			{
				args.Add(words[i]);
			}
			return args;
		}

		public static Message decode(byte[] bytes, int offset, int length) {
			Message message = null;
			if (bytes!=null) {
				length = Math.Min(bytes.Length, offset + length);

				string data = Encoding.UTF8.GetString(bytes).Substring(offset, length);
				data = data.Trim();

				if (Status.getKeyWordList().Any(keyword => data.StartsWith(keyword)))
				//if (data.StartsWith(Status.getKeyWord()))
					message = new Status(data);

				if (Maneuver.getKeyWordList().Any(keyword => data.StartsWith(keyword)))
				{
					string[] words = data.Split(' ');
					List<string> args = splitArgs(words);
					message = new Maneuver(words[0], args);
				}

				// TODO: decode all of the other kinds of messages based on what the message starts with.  If it doesn't
				//       start with a recognized key word, assume it is an info message (i.e., a reply to a query)
			}
			return message;
		}

		public byte[] encode()
		{
			return Encoding.UTF8.GetBytes(getMessageText());
		}

//		public abstract string getMessageType();

		public string getMessageText() { return messageText; }

		public bool isValid() { return valid; }

		protected void setIsValid(bool newValid) { this.valid = newValid; }
	}
}