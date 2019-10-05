namespace Shared.DefaultMissions
{
	public class DefaultMission1
	{
		public static readonly string[] rotateMission =
		{
			"sleep " + DefaultConstants.START_MISSION_SLEEP_TIME,
			"takeoff",
			"sleep " + DefaultConstants.DEFAULT_SLEEP_TIME,
			"cw 360",
			"sleep " + DefaultConstants.DEFAULT_SLEEP_TIME,
			"land"
		};
	}
}