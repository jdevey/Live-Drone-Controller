namespace Shared.MessageTypes
{
	public abstract class ResponseBase : Message
	{
		public ResponseBase(string text) : base(text)
		{
		}
	}
}