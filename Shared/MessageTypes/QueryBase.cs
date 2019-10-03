using System.Net;
using System.Net.Mime;
using System.Net.Sockets;

namespace Shared.MessageTypes
{
	public class QueryBase : Message
	{
		public QueryBase(string text) : base(text)
		{
		}

		public void activate(UdpClient udpClient, IPEndPoint remoteIpEndPoint)
		{
			string text = getMessageText();
			byte[] bytes = Utils.encodeString(text);
			udpClient.Send(bytes, bytes.Length, remoteIpEndPoint);
		}
	}
}