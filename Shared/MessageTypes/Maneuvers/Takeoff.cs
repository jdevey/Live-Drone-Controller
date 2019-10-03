namespace Shared.MessageTypes.Maneuvers
{
	public class Takeoff : ManeuverBase
	{
		public Takeoff(string text) : base(text)
		{
		}
		
		public static string getKeyword()
		{
			return "takeoff";
		}
		
		public override void updateState(DroneState state)
		{
			state.setHasTakenOff(true);
			
			// Drone flys up approximately one meter upon takeoff
			state.move(0, 0, 100);
		}
	}
}