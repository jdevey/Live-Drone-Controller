using System;
using System.Threading;

namespace Shared.MessageTypes.Enhancements
{
	public class PrintState : Message
	{
		public PrintState(string text) : base(text)
		{
		}
		
		public static string getKeyword()
		{
			return "printstate";
		}

		public void activate(DroneState state)
		{
			Console.WriteLine(Status.getMessageTextFromState(state));
		}
	}
}