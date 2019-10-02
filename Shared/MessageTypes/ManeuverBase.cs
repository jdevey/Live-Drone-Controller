namespace Shared.MessageTypes
{
	public class ManeuverBase : Message
	{
		public ManeuverBase(string text) : base(text)
		{
		}

		public virtual void updateState(DroneState state)
		{
		}
	}
}