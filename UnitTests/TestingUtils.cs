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

		public const string defaultState = "mid:-1;x:0;y:0;z:0;mpry:0,0,0;pitch:0;roll:0;yaw:0;" +
		                                   "vgx:0;vgy:0;vgz:0;" +
		                                   "templ:20;temph:20;" +
		                                   "tof:0;h:0;" +
		                                   "bat:100;baro:0;" +
		                                   "time:0;" +
		                                   "agx:0;agy:0;agz:0";
		
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
			new PortTrio(8889, 8890, 8891), // 0 ControllerCommTests
			new PortTrio(8892, 8893, 8894), // 1 ControllerTests
			new PortTrio(8895, 8896, 8897), // 2 MissionTests
			new PortTrio(8898, 8899, 8900), // 3 ErrorStateTests
			new PortTrio(8901, 8902, 8903), // 4 SimulatorTests
			new PortTrio(8904, 8905, 8906), // 5 SimulatorCommTests
			new PortTrio(8907, 8908, 8909), // 6 UDPCommBaseTests
			new PortTrio(8910, 8911, 8912), // 7 QueryBaseTests
			new PortTrio(8913, 8914, 8915), // 8 BatteryQueryTests
			new PortTrio(8916, 8917, 8918), // 9 SpeedQueryTests
			new PortTrio(8919, 8920, 8921), // 10 TimeQueryTests
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