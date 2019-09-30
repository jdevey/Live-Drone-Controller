namespace Shared.MessageTypes.Responses
{
	public class Ok : ResponseBase
	{
		public Ok(string text) : base(text)
		{
		}
		
		public static string getKeyword()
		{
			return "ok";
		}
	}
}