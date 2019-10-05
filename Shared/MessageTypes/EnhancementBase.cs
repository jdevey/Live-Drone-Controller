namespace Shared.MessageTypes
{
	public abstract class EnhancementBase : Message
	{
		protected EnhancementBase(string text) : base(text)
		{
		}

		public virtual void activate()
		{
		}
	}
}