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
		
		public override void updateState(DroneState state)
		{
			// Drone flys down approximately one meter upon landing
			state.move(0, -100, 0);
			
			state.setHasTakenOff(false);
		}
	}
}