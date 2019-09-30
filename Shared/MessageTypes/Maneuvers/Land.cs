namespace Shared.MessageTypes.Maneuvers
{
	public class Land : ManeuverBase
	{
		public Land(string text) : base(text)
		{
		}
		
		public static string getKeyword()
		{
			return "land";
		}
	}
}