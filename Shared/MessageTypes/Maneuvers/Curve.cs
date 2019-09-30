namespace Shared.MessageTypes.Maneuvers
{
	public class Curve : ManeuverBase
	{
		public Curve(string text) : base(text)
		{
		}
		
		public static string getKeyword()
		{
			return "curve";
		}
	}
}