namespace Shared.MessageTypes
{
	// Represents the string "command" sent when first connecting to the drone
	public class Command : Message
	{
		public Command(string text) : base(text)
		{
		}
	
		public static string getKeyword()
		{
			return "command";
		}
	}
}