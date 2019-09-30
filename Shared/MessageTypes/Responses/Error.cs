namespace Shared.MessageTypes.Responses
{
	public class Error : ResponseBase
	{
		public Error(string text) : base(text)
		{
		}
		
		public static string getKeyword()
		{
			return "error";
		}
	}
}