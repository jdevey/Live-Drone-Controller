using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Shared.MessageTypes.Responses;

namespace Shared
{
	public class UDPCommBase : ErrorState
	{
		public readonly string droneIp;
		
		public readonly int localPort;
		public readonly int commandPort;
		public readonly int telloStatePort;
		
		private uint maxRetries;
		private bool isCommunicationLive = true;

		public UDPCommBase(
			string droneIp = DefaultConstants.LOCALHOST,
			int localPort = DefaultConstants.DEFAULT_LOCAL_PORT,
			int commandPort = DefaultConstants.DEFAULT_COMMAND_PORT,
			int telloStatePort = DefaultConstants.DEFAULT_TELLO_STATE_PORT,
			int timeout = DefaultConstants.DEFAULT_TIMEOUT,
			uint maxRetries = DefaultConstants.DEFAULT_MAX_RETRIES)
		{
			this.droneIp = droneIp;
			this.localPort = localPort;
			this.commandPort = commandPort;
			this.telloStatePort = telloStatePort;
			this.maxRetries = maxRetries;
		}

		public void sendMessage(string msg, UdpClient updClient, IPEndPoint remoteIpEndpoint)
		{
			byte[] msgBytes = Utils.encodeString(msg);
			try
			{
				updClient.Send(msgBytes, msgBytes.Length, remoteIpEndpoint);
			}
			catch (Exception e)
			{
				Console.WriteLine("ERROR: Failed to send message in UDP base.");
				Console.WriteLine(e);
				setErrorState(true);
			}
		}

		public string getResponse(UdpClient updClient, IPEndPoint remoteIpEndpoint)
		{
			uint numRetries = maxRetries;

			while (numRetries-- > 0)
			{
				try
				{
					byte[] receiveBytes = updClient.Receive(ref remoteIpEndpoint);
					if (receiveBytes.Length > 0)
					{
						if (Utils.decodeBytes(receiveBytes) == Error.getKeyword())
						{
							Console.WriteLine("ERROR: UDP base has encountered an error state.");
							setErrorState(true);
						}

						return Utils.decodeBytes(receiveBytes);
					}
				}
				catch (Exception e)
				{
					if (isCommunicationLive)
					{
						Console.WriteLine(e.Message);
						return "";
					}
				}
			}

			return "";
		}

		public bool getIsCommunicationLive()
		{
			return isCommunicationLive;
		}

		public void setIsCommunicationLive(bool value)
		{
			isCommunicationLive = value;
		}
	}
}