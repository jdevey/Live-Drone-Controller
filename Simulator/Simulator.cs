using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Shared;
using Shared.MessageTypes;
using Shared.MessageTypes.Queries;
using Shared.MessageTypes.Responses;

namespace Simulator
{
	public class Simulator : ErrorState
	{
		private const int SLEEP_TIME_MS = 100;

		private SimulatorComm simulatorComm;

		private readonly DroneState state;

		private readonly Thread stateBroadcastThread;
		private readonly Thread serverThread;

		public Simulator(int commandPort = DefaultConstants.DEFAULT_COMMAND_PORT,
			int telloStatePort = DefaultConstants.DEFAULT_TELLO_STATE_PORT,
			int timeout = DefaultConstants.DEFAULT_TIMEOUT,
			uint maxRetries = DefaultConstants.DEFAULT_MAX_RETRIES)
		{
			state = new DroneState();

			simulatorComm = new SimulatorComm(commandPort, telloStatePort, timeout, maxRetries);

			stateBroadcastThread = new Thread(stateBroadcastLoop);
			serverThread = new Thread(setupServer);
		}

		public void start()
		{
			serverThread.Start();
		}

		public void setupServer()
		{
			if (simulatorComm == null || simulatorComm.getLocalIpEndPoint() == null)
			{
				Console.WriteLine("h");
			}

			string msg = "";
			byte[] bytes;
			try
			{
				bytes = simulatorComm.getUdpClient().Receive(ref simulatorComm.getLocalIpEndPoint());
				msg = Utils.decodeBytes(bytes);
			}
			catch (Exception e)
			{
				Console.WriteLine("ERROR: Failed to set up simulator.");
				setErrorState(true);
			}
			if (msg == Command.getKeyword())
			{
				simulatorComm.getTelloStateIpEndPoint() =
					new IPEndPoint(simulatorComm.getLocalIpEndPoint().Address, simulatorComm.telloStatePort);
				stateBroadcastThread.Start();

				state.setInCommandMode(true);
				simulatorComm.sendMessage(Ok.getKeyword(),
					simulatorComm.getUdpClient(), simulatorComm.getLocalIpEndPoint());

				serverLoop();
			}
		}

		private string getQueryResult(Type msgType)
		{
			string simResponse = Error.getKeyword();
			if (msgType == typeof(TimeQuery))
			{
				simResponse = Utils.formatDouble(state.getMotorTime());
			}
			else if (msgType == typeof(BatteryQuery))
			{
				simResponse = state.getBatteryPercentage().ToString();
			}
			else if (msgType == typeof(SpeedQuery))
			{
				simResponse = Utils.pythagorean3D(state.getSpeedX(), state.getSpeedY(), state.getSpeedZ());
			}

			return simResponse;
		}

		private void serverLoop()
		{
			while (simulatorComm.getIsCommunicationLive())
			{
				string resp = simulatorComm.getResponse(simulatorComm.getUdpClient(), simulatorComm.getLocalIpEndPoint());
				Message msg = MessageFactory.createMessage(resp);
				if (msg == null)
				{
					Console.WriteLine("ERROR: Simulator received invalid message.");
					simulatorComm.sendMessage(Error.getKeyword(),
						simulatorComm.getUdpClient(), simulatorComm.getLocalIpEndPoint());
					continue;
				}

				Type msgType = msg.GetType();
				string simResponse = Error.getKeyword();

				// Messages from the user should either be "command", a maneuver, or a query
				if (msgType == typeof(Command) ||
				    msgType.IsSubclassOf(typeof(QueryBase)) ||
				    msgType.IsSubclassOf(typeof(ManeuverBase)))
				{
					if (msgType == typeof(Command)) // Command
					{
						simResponse = Ok.getKeyword();
					}
					else if (msgType.IsSubclassOf(typeof(ManeuverBase))) // Maneuvers
					{
						simResponse = Ok.getKeyword();
						try
						{
							(msg as ManeuverBase).updateState(state);
						}
						catch (NullReferenceException e)
						{
							Console.WriteLine("ERROR: Attempted to update state via a class that lacks such a function.");
							Console.WriteLine(e);
						}
					}
					else if (msgType.IsSubclassOf(typeof(QueryBase))) // Queries
					{
						simResponse = getQueryResult(msgType);
					}
				}
				else // User has sent an invalid message
				{
					if (simulatorComm.getIsCommunicationLive())
					{
						Console.WriteLine("ERROR: Simulator has encountered an invalid message from user: " + resp);
						setErrorState(true);
					}

					simResponse = Error.getKeyword();
				}

				simulatorComm.sendMessage(simResponse,
					simulatorComm.getUdpClient(), simulatorComm.getLocalIpEndPoint());
			}
		}

		private void stateBroadcastLoop()
		{
			while (simulatorComm.getIsCommunicationLive())
			{
				state.setMotorTime(state.getMotorTime() + 100);
				state.setStateSetCount(state.getStateSetCount() + 1);
				if (state.getStateSetCount() % 2 == 0)
				{
					state.setBatteryPercentage(state.getBatteryPercentage() - 1);
					state.setHighTemperature(state.getHighTemperature() + 1);
				}
				string stateString = Status.getMessageTextFromState(state);
				simulatorComm.sendMessage(stateString,
					simulatorComm.getUdpClient(), simulatorComm.getTelloStateIpEndPoint());
				Thread.Sleep(SLEEP_TIME_MS);
			}
		}

		public void stop()
		{
			simulatorComm.setIsCommunicationLive(false);
			if (stateBroadcastThread.IsAlive)
			{
				stateBroadcastThread.Join();//.Abort();
			}
			if (serverThread.IsAlive)
			{
				serverThread.Join();//.Abort();
			}
		}

		public SimulatorComm getSimulatorComm()
		{
			return simulatorComm;
		}
	}
}