using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using Shared;
using Shared.MessageTypes;
using Shared.MessageTypes.Enhancements;

namespace DroneController
{
	public class Controller : ErrorState
	{
		private const string START_MISSION_SLEEP_TIME = "200";
		private const string DEFAULT_SLEEP_TIME = "1000";

		public static readonly string[] rotateMission =
		{
			"sleep " + START_MISSION_SLEEP_TIME,
			"takeoff",
			"sleep " + DEFAULT_SLEEP_TIME,
			"cw 360",
			"sleep " + DEFAULT_SLEEP_TIME,
			"land"
		};

		public static readonly string[] leftRightMission =
		{
			"sleep " + START_MISSION_SLEEP_TIME,
			"takeoff",
			"sleep " + DEFAULT_SLEEP_TIME,
			"left 50",
			"right 50",
			"sleep " + DEFAULT_SLEEP_TIME,
			"land"
		};

		public static readonly string[] squareMission =
		{
			"sleep " + START_MISSION_SLEEP_TIME,
			"takeoff",
			"sleep " + DEFAULT_SLEEP_TIME,
			"right 50",
			"sleep " + DEFAULT_SLEEP_TIME,
			"cw 90",
			"sleep " + DEFAULT_SLEEP_TIME,
			"right 50",
			"sleep " + DEFAULT_SLEEP_TIME,
			"cw 90",
			"sleep " + DEFAULT_SLEEP_TIME,
			"right 50",
			"sleep " + DEFAULT_SLEEP_TIME,
			"cw 90",
			"sleep " + DEFAULT_SLEEP_TIME,
			"right 50",
			"sleep " + DEFAULT_SLEEP_TIME,
			"cw 90",
			"sleep " + DEFAULT_SLEEP_TIME,
			"land"
		};

		private readonly DroneState state;

		private ControllerComm controllerUdpClient;

		private static readonly List<Mission> missionList = new List<Mission>();

		private readonly Thread stateLoopThread;

		public Controller(
			string droneIp = DefaultConstants.DEFAULT_DRONE_IP,
			int localPort = DefaultConstants.DEFAULT_LOCAL_PORT,
			int commandPort = DefaultConstants.DEFAULT_COMMAND_PORT,
			int telloStatePort = DefaultConstants.DEFAULT_TELLO_STATE_PORT,
			int timeout = DefaultConstants.DEFAULT_TIMEOUT,
			uint maxRetries = DefaultConstants.DEFAULT_MAX_RETRIES)
		{
			state = new DroneState();
			
			controllerUdpClient = new ControllerComm(droneIp, localPort, commandPort,
				telloStatePort, timeout, maxRetries);

			addMission(rotateMission);
			addMission(leftRightMission);
			addMission(squareMission);

			// controllerUdpClient.startConnection();
			
			stateLoopThread = new Thread(stateLoop);
			if (!controllerUdpClient.getErrorState())
			{
				stateLoopThread.Start();
			}
		}

		public void addMission(string[] actionList)
		{
			List<Message> messages = new List<Message>();
			foreach (string action in actionList)
			{
				Message msg = MessageFactory.createMessage(action);
				
				Type msgType = msg.GetType();

				// Messages from the user should either be "sleep", a maneuver, or a query
				if (msgType == typeof(SleepAction) ||
				    msgType.IsSubclassOf(typeof(QueryBase)) ||
				    msgType.IsSubclassOf(typeof(ManeuverBase)))
				{
					messages.Add(msg);
				}
				else
				{
					Console.WriteLine("ERROR: Attempted to add invalid command to mission: " + action);
					setErrorState(true);
				}
			}

			missionList.Add(new Mission(messages));
		}
		
		public void stateLoop()
		{
			while (controllerUdpClient.getIsCommunicationLive())
			{
				byte[] receiveBytes = controllerUdpClient.getStateRcvClient().Receive(
					ref controllerUdpClient.getCommandIpEndPoint());
				string msg = Utils.decodeBytes(receiveBytes);
				Status status = new Status(msg);
				state.updateFlyingInfo(status);
				state.setStateSetCount(state.getStateSetCount() + 1);
				if (state.getStateSetCount() % 10 == 0)
				{
					Console.WriteLine("State update: " + msg);
				}
			}
		}

		public void executeMission(int missionNumber)
		{
			if (missionNumber < 1 || missionNumber > missionList.Count)
			{
				Console.WriteLine("ERROR: Invalid mission number. There are currently "
				                  + missionList.Count + " missions available.");
				setErrorState(true);
				return;
			}

			missionList[missionNumber - 1].execute(controllerUdpClient, controllerUdpClient.getCommandIpEndPoint(), state);
		}
		
		public void stop()
		{
			controllerUdpClient.setIsCommunicationLive(false);
			if (stateLoopThread.IsAlive)
			{
				stateLoopThread.Join();
				//stateLoopThread.Abort();
			}
		}

		public ControllerComm getUDPClient()
		{
			return controllerUdpClient;
		}
	}
}