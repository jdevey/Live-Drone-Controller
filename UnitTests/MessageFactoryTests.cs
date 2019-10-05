using NUnit.Framework;
using Shared;
using Shared.MessageTypes;
using Shared.MessageTypes.Enhancements;
using Shared.MessageTypes.Maneuvers;
using Shared.MessageTypes.Queries;
using Shared.MessageTypes.Responses;

namespace UnitTests
{
	[TestFixture]
	public class MessageFactoryTests
	{
		[Test]
		public void Main()
		{
			Message command = MessageFactory.createMessage("command");
			Assert.AreEqual(command.GetType(), typeof(Command));
			Message status = MessageFactory.createMessage(
				"mid:-1;x:0;y:0;z:0;mpry:0,0,0;pitch:0;roll:0;yaw:0;" +
				"vgx:0;vgy:0;vgz:0;" +
				"templ:0;temph:0;" +
				"tof:0;h:0;" +
				"bat:0;baro:0;" +
				"time:0;" +
				"agx:0.0;agy:0.0;agz:0.0");
			Assert.AreEqual(status.GetType(), typeof(Status));
			Message ok = MessageFactory.createMessage("ok");
			Assert.AreEqual(ok.GetType(), typeof(Ok));
			Message error = MessageFactory.createMessage("error");
			Assert.AreEqual(error.GetType(), typeof(Error));
			Message sleep = MessageFactory.createMessage("sleep 10");
			Assert.AreEqual(sleep.GetType(), typeof(SleepAction));
			Message printstate = MessageFactory.createMessage("printstate");
			Assert.AreEqual(printstate.GetType(), typeof(PrintState));
			Message printbroadcastcount = MessageFactory.createMessage("printbroadcastcount");
			Assert.AreEqual(printbroadcastcount.GetType(), typeof(PrintBroadcastCount));
			Message batteryquery = MessageFactory.createMessage("battery?");
			Assert.AreEqual(batteryquery.GetType(), typeof(BatteryQuery));
			Message speedyquery = MessageFactory.createMessage("speed?");
			Assert.AreEqual(speedyquery.GetType(), typeof(SpeedQuery));
			Message timequery = MessageFactory.createMessage("time?");
			Assert.AreEqual(timequery.GetType(), typeof(TimeQuery));
			Message curve = MessageFactory.createMessage("curve 0 0 0 0 0 0 0");
			Assert.AreEqual(curve.GetType(), typeof(Curve));
			Message flip = MessageFactory.createMessage("flip r");
			Assert.AreEqual(flip.GetType(), typeof(Flip));
			Message go = MessageFactory.createMessage("go 0 0 0 0");
			Assert.AreEqual(go.GetType(), typeof(Go));
			Message jump = MessageFactory.createMessage("jump 0 0 0 0");
			Assert.AreEqual(jump.GetType(), typeof(Jump));
			Message land = MessageFactory.createMessage("land");
			Assert.AreEqual(land.GetType(), typeof(Land));
			Message takeoff = MessageFactory.createMessage("takeoff");
			Assert.AreEqual(takeoff.GetType(), typeof(Takeoff));
			Message rotate = MessageFactory.createMessage("cw 90");
			Assert.AreEqual(rotate.GetType(), typeof(Rotate));
			Message back = MessageFactory.createMessage("back 10");
			Assert.AreEqual(back.GetType(), typeof(DirectionalMove));

			string expectedType = "cw";
			string fullCmd = "cw 30";
			Assert.AreEqual(MessageFactory.getCommandType(fullCmd), expectedType);
			Assert.AreEqual(MessageFactory.getCommandType(expectedType), expectedType);

			Flip f = MessageFactory.createMessage("flip f") as Flip;
			Assert.AreEqual(MessageFactory.convertFlipToDirectionalMove(f).getY(), 10);
			Flip b = MessageFactory.createMessage("flip b") as Flip;
			Assert.AreEqual(MessageFactory.convertFlipToDirectionalMove(b).getY(), -10);
			Flip r = MessageFactory.createMessage("flip r") as Flip;
			Assert.AreEqual(MessageFactory.convertFlipToDirectionalMove(r).getX(), 10);
			Flip l = MessageFactory.createMessage("flip l") as Flip;
			Assert.AreEqual(MessageFactory.convertFlipToDirectionalMove(l).getX(), -10);
			Flip fakeFlip = MessageFactory.createMessage("flip d") as Flip;
			fakeFlip.setDirection('d');
			Assert.AreEqual(MessageFactory.convertFlipToDirectionalMove(fakeFlip).getY(), 10);
			
		}
	}
}