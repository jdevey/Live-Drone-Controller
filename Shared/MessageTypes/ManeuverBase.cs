using System.Net;
using System.Net.Sockets;

namespace Shared.MessageTypes
{
	public class ManeuverBase : Message
	{
		public ManeuverBase(string text) : base(text)
		{
		}
		
		public void activate(UdpClient udpClient, IPEndPoint remoteIpEndPoint)
		{
			string text = getMessageText();
			byte[] bytes = Utils.encodeString(text);
			udpClient.Send(bytes, bytes.Length, remoteIpEndPoint);
		}

		public virtual void updateState(DroneState state)
		{
		}
	}
}