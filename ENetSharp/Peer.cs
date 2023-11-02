/*
	ENetSharp
	- Peer

	Written By: Ryan Smith
*/
using System;
using System.Net;
using System.Net.Sockets;

namespace ENetSharp;

public class Peer
{
	/* Constructors */
	public Peer(Host host, ushort id)
	{
		this.IncomingId = id;
		this._Host = host;
		this.OutgoingId = Host.MAX_PEERS;
		this.ConnectId = 0;
		this._Endpoint = new IPEndPoint(IPAddress.Any, 0);
		this._State = State.Disconnected;
	}
	/* Instance Methods */
	public void Reset()
	{

	}
	/* Properties */
	public State _State;
	public ushort IncomingId;
	public ushort OutgoingId;
	public byte IncomingSessionId = 0xFF;
	public byte OutgoingSessionId = 0xFF;
	public uint ConnectId;
	private EndPoint _Endpoint;
	private readonly Host _Host;
	/* Class Properties */
	/* Sub-Classes */
	public enum State
	{
		Disconnected            = 0,
		Connecting              = 1,
		AcknowledgingConnect    = 2,
		ConnectionPending       = 3,
		ConnectionSucceeded     = 4,
		Connected               = 5,
		DisconnectLater         = 6,
		Disconnecting           = 7,
		AcknowledgingDisconnect = 8,
		Zombie                  = 9 
	}
}
