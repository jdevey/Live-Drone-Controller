namespace Shared.DefaultMissions
{
	public class DefaultMission2
	{
		public static readonly string[] leftRightMission =
		{
			"sleep " + DefaultConstants.START_MISSION_SLEEP_TIME,
			"takeoff",
			"sleep " + DefaultConstants.DEFAULT_SLEEP_TIME,
			"left 50",
			"right 50",
			"sleep " + DefaultConstants.DEFAULT_SLEEP_TIME,
			"land"
		};
	}
}