namespace Shared.MessageTypes.Maneuvers
{
	public class Flip : ManeuverBase
	{
		public Flip(string text) : base(text)
		{
		}
		
		public static string getKeyword()
		{
			return "flip";
		}
	}
}