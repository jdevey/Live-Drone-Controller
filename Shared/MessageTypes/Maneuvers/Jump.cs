namespace Shared.MessageTypes.Maneuvers
{
	public class Jump : ManeuverBase
	{
		public Jump(string text) : base(text)
		{
		}
		
		public static string getKeyword()
		{
			return "jump";
		}
	}
}