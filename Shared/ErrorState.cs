namespace Shared
{
	public abstract class ErrorState
	{
		private bool isInErrorState;
		
		public virtual void setErrorState(bool errorState)
		{
			isInErrorState = errorState;
		}

		public virtual bool getErrorState()
		{
			return isInErrorState;
		}
	}
}