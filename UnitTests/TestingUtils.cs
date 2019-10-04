using DroneController;

namespace UnitTests
{
	public class TestingUtils
	{
		public static string[] leftRightMission =
		{
			"takeoff",
			"sleep 2000",
			"left 30",
			"right 30",
			"sleep 2000",
			"land"
		};
		
		public static string[] flipBatteryMission =
		{
			"takeoff",
			"sleep 50",
			"left 30",
			"flip f",
			"battery?",
			"right 30",
			"sleep 50",
			"land"
		};
		
		public static string[] badMission1 =
		{
			"takeoff",
			"sleep not_a_number",
			"land"
		};
		
		public static string[] badMission2 =
		{
			"takeoff",
			"ok",
			"land"
		};
		
		public static string serverIp = "127.0.0.1";
		public static int serverPort = 8889;
		
		public static string droneIp = "127.0.0.1";
		public static int dronePort = 8889;

		public struct PortTrio
		{
			public int command;
			public int tello;
			public int local;
			public PortTrio(int command, int tello, int local)
			{
				this.command = command;
				this.tello = tello;
				this.local = local;
			}
		};
		
		// When running tests rapidly in sequence, ports and addresses aren't cleaned up fast enough.
		public static PortTrio[] ports =
		{
			new PortTrio(8889, 8890, 8891), // ControllerCommTests
			new PortTrio(8892, 8893, 8894), // ControllerTests
			new PortTrio(8895, 8896, 8897), // MissionTests
			new PortTrio(8898, 8899, 8900), // ErrorStateTests
			new PortTrio(8901, 8902, 8903),
			new PortTrio(8904, 8905, 8906)
		};
		
		public static Simulator.Simulator generateSim(PortTrio p)
		{
			return new Simulator.Simulator(commandPort: p.command, telloStatePort: p.tello);
		}

		public static Controller generateContr(PortTrio p)
		{
			return new Controller(commandPort: p.command, telloStatePort: p.tello, localPort: p.local);
		}
	}
}