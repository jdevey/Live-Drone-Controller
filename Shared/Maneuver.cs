using System;
using System.Collections.Generic;
using Shared;

namespace DroneController
{
	public class Maneuver : Message//DroneAction
	{
		private static readonly HashSet<string> KEYWORD_LIST = new HashSet<string>
		{
			"up",
			"down",
			"left",
			"right",
			"forward",
			"back",
			"cw",
			"ccw",
			"flip",
			"go",
			"curve",
			"jump"
		};

		public Maneuver(string messageText) : base(messageText)
		{
			// base(messageText);
			//this.messageText = messageText;
		}

		public static HashSet<string> getKeyWordList()
		{
			return KEYWORD_LIST;
		}

//		protected override string getMessageText()
//		{
//			return messageText;
//		}
		
//		public override string getMessageType()
//		{
//			return "maneuver";
//		}

		public override void execute(ref DroneUDPClient droneUdpClient)
		{
			Console.WriteLine("Now executing \"" + getMessageText() + "\" command.");
			droneUdpClient.sendMessage(getMessageText());
			string response = droneUdpClient.getResponse();
			if (response != "ok")
			{
				droneUdpClient.setErrorState(true);
			}
		}
	}
}