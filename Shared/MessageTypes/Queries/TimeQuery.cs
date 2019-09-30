namespace Shared.MessageTypes.Queries
{
	public class TimeQuery : QueryBase
	{
		public TimeQuery(string text) : base(text)
		{
		}
		
		public static string getKeyword()
		{
			return "time";
		}
	}
}