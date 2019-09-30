namespace Shared.MessageTypes.Maneuvers
{
	public class Takeoff : ManeuverBase
	{
		public Takeoff(string text) : base(text)
		{
		}
		
		public static string getKeyword()
		{
			return "takeoff";
		}
	}
}