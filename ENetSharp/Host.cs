/*
	ENetSharp
	- Host

	Written By: Ryan Smith
*/
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ENetSharp;

public class Host : IDisposable
{
	/* Constructors */
	#nullable enable
	public Host(IPEndPoint? address, ushort peerCount, byte channelCount, uint incomingBandwidth = 0, uint outgoingBandwidth = 0)
	{
		// Seed
		this.Seed = (uint)((_RNG.Next(1 << 30) << 2) | _RNG.Next(1 << 2));
		// Channels
		if (channelCount == 0)
			channelCount = 1;
		this.ChannelCount = channelCount;
		// Socket
		this._Socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
		this._Socket.Blocking          = false;
		this._Socket.EnableBroadcast   = true;
		this._Socket.ReceiveBufferSize = SIZEOF_BUFFER;
		this._Socket.SendBufferSize    = SIZEOF_BUFFER;
		if (address != null)
			this._Socket.Bind(address);
		// Bandwidth
		this.IncomingBandwidth = incomingBandwidth;
		this.OutgoingBandwidth = outgoingBandwidth;
		// Peers
		if (peerCount == 0)
			peerCount = 1;
		else if (peerCount > MAX_PEERS)
			peerCount = MAX_PEERS;
		this.Peers = new Peer[peerCount];
		for (var i = 0; i < this.Peers.Length; ++i)
			this.Peers[i] = new Peer(this, (ushort)(i + 1));
	}
	#nullable disable
	/* Instance Methods */
	public void Connect(IPEndPoint address, byte channelCount, uint userData = 0)
	{
		if (channelCount == 0)
			channelCount = this.ChannelCount;
		var peer = this.Peers.Select(x => x._State == Peer.State.Disconnected).First();
	}
	public async Task ConnectAsync(IPEndPoint address, byte channelCount, uint userData = 0)
	{
		if (channelCount == 0)
			channelCount = this.ChannelCount;
		var peer = this.Peers.Select(x => x._State == Peer.State.Disconnected).First();
	}
	public void Dispose()
	{
		this._Socket.Dispose();
	}
	/* Properties */
	public readonly Socket _Socket;
	public ushort PeerCount;
	public byte ChannelCount;
	public uint IncomingBandwidth;
	public uint OutgoingBandwidth;
	public readonly Peer[] Peers;
	public readonly uint Seed;
	/* Class Properties */
	private static readonly Random _RNG = new Random();
	private const int     SIZEOF_BUFFER = 256 * 1024;
	internal const ushort MAX_PEERS     = 4095;
}
