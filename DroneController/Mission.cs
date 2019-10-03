using System;
using System.Collections.Generic;
using System.Net;
using Shared;
using Shared.MessageTypes;
using Shared.MessageTypes.Maneuvers;
using Shared.MessageTypes.Enhancements;

namespace DroneController
{
	public class Mission : ErrorState
	{
		private readonly List<Message> actionList;

		public Mission(List<Message> actionList_)
		{
			actionList = actionList_;
		}

		public void execute(ControllerComm controllerComm, IPEndPoint commandIpEndPoint, DroneState state)
		{
			foreach (Message droneAction in actionList)
			{
				if (state.getBatteryPercentage() < 20)
				{
					Console.WriteLine("WARNING: Drone battery percentage has dropped below 20%. Now landing...");
					setErrorState(true);
					(new Land(Land.getKeyword())).activate(controllerComm.getDroneCommClient(), commandIpEndPoint);
					break;
				}

				if (state.getHighTemperature() > 60)
				{
					Console.WriteLine("WARNING: Drone temperature has exceeded 60 degrees celsius. Now landing...");
					setErrorState(true);
					(new Land(Land.getKeyword())).activate(controllerComm.getDroneCommClient(), commandIpEndPoint);
					break;
				}

				if (state.getBatteryPercentage() < 50 && droneAction.GetType() == typeof(Flip))
				{
					Console.WriteLine("WARNING: Drone battery is below 50%. Flips will now be converted to directional moves.");
					DirectionalMove directionalMove = MessageFactory.convertFlipToDirectionalMove(droneAction as Flip);
					directionalMove.activate(controllerComm.getDroneCommClient(), commandIpEndPoint);
					continue;
				}

				Console.WriteLine("Now executing action: " + droneAction.getMessageText());
				Type msgType = droneAction.GetType();
				if (msgType == typeof(SleepAction))
				{
					(droneAction as SleepAction).activate();
				}
				else if (msgType.IsSubclassOf(typeof(QueryBase)))
				{
					(droneAction as QueryBase).activate
						(controllerComm.getDroneCommClient(), controllerComm.getCommandIpEndPoint());
				}
				else if (msgType.IsSubclassOf(typeof(ManeuverBase)))
				{
					(droneAction as ManeuverBase).activate
						(controllerComm.getDroneCommClient(), controllerComm.getCommandIpEndPoint());
				}
			}
		}
	}
}