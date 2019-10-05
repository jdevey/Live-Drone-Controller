using System;

namespace Shared.MessageTypes.Enhancements
{
	public class PrintBroadcastCount : EnhancementBase
	{
		public PrintBroadcastCount(string text) : base(text)
		{
		}
		
		public static string getKeyword()
		{
			return "printbroadcastcount";
		}

		public void activate(DroneState state)
		{
			Console.WriteLine("State has been broadcasted " + state.getStateSetCount() + " times.");
		}
	}
}