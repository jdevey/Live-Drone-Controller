namespace Shared.MessageTypes.Maneuvers
{
	public class Go : ManeuverBase
	{
		public Go(string text) : base(text)
		{
		}
		
		public static string getKeyword()
		{
			return "go";
		}
	}
}