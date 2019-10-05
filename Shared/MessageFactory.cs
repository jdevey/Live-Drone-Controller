using System;
using System.Collections.Generic;
using System.Linq;
using Shared.MessageTypes;
using Shared.MessageTypes.Responses;
using Shared.MessageTypes.Queries;
using Shared.MessageTypes.Maneuvers;
using Shared.MessageTypes.Enhancements;

namespace Shared
{
	public static class MessageFactory
	{
		// Iterate through the string until a non-letter or the end of the
		// command is reached.
		public static string getCommandType(string text)
		{
			for (int i = 0; i < text.Length; ++i)
			{
				if (!char.IsLetter(text[i]))
				{
					return text.Substring(0, i);
				}
			}

			return text;
		}
		
		// Determine the type of message and return an appropriate object
		// base on a variety of metrics
		public static Message createMessage(string text)
		{
			Message message = null;

			string commandType = getCommandType(text);

			if (text.StartsWith(Command.getKeyword()))
				message = new Command(text);
			else if (text.StartsWith(Status.getKeyword()))
				message = new Status(text);
			else if (text.StartsWith(Ok.getKeyword()))
				message = new Ok(text);
			else if (text.StartsWith(Error.getKeyword()))
				message = new Error(text);
			else if (text.StartsWith(SleepAction.getKeyword()))
				message = new SleepAction(text);
			else if (text.StartsWith(PrintState.getKeyword()))
				message = new PrintState(text);
			else if (text.StartsWith(PrintBroadcastCount.getKeyword()))
				message = new PrintBroadcastCount(text);
			else if (text.StartsWith(BatteryQuery.getKeyword()))
				message = new BatteryQuery(text);
			else if (text.StartsWith(SpeedQuery.getKeyword()))
				message = new SpeedQuery(text);
			else if (text.StartsWith(TimeQuery.getKeyword()))
				message = new TimeQuery(text);
			else if (text.StartsWith(Curve.getKeyword()))
				message = new Curve(text);
			else if (text.StartsWith(Flip.getKeyword()))
				message = new Flip(text);
			else if (text.StartsWith(Go.getKeyword()))
				message = new Go(text);
			else if (text.StartsWith(Jump.getKeyword()))
				message = new Jump(text);
			else if (text.StartsWith(Land.getKeyword()))
				message = new Land(text);
			else if (text.StartsWith(Takeoff.getKeyword()))
				message = new Takeoff(text);
			else if (Rotate.getKeywords().Any(keyword => text.StartsWith(keyword)))
				message = new Rotate(text);
			else if (DirectionalMove.getKeywords().Any(keyword => text.StartsWith(keyword)))
				message = new DirectionalMove(text);
			else
				message = new DataResponse(text);
			
			return message;
		}
		
		public static DirectionalMove convertFlipToDirectionalMove(Flip flip)
		{
			switch (flip.getDirection())
			{
				case 'r':
					return new DirectionalMove("right 10");
				case 'l':
					return new DirectionalMove("left 10");
				case 'f':
					return new DirectionalMove("forward 10");
				case 'b':
					return new DirectionalMove("back 10");
				default:
					return new DirectionalMove("forward 10");
			}
		}
	}
}