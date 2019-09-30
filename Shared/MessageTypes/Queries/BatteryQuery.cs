namespace Shared.MessageTypes.Queries
{
	public class BatteryQuery : QueryBase
	{
		public BatteryQuery(string text) : base(text)
		{
		}
		
		public static string getKeyword()
		{
			return "battery";
		}
	}
}