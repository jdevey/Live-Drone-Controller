namespace Shared.MessageTypes.Queries
{
	public class SpeedQuery : QueryBase
	{
		public SpeedQuery(string text) : base(text)
		{
		}
		
		public static string getKeyword()
		{
			return "speed";
		}
	}
}