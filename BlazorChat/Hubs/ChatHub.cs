using BlazorChat.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorChat.Hubs
{
	public class ChatHub : Hub
	{
		private RoomManager _roomManager;
		//private Dictionary<string, ChatRoom> _rooms;

		public ChatHub(RoomManager roomManager)
		{
			_roomManager = roomManager;
		}

		public async Task CreateRoom(string roomName, string createdByUser)
		{
			// Check if name exists
			if (RoomExists(roomName))
				return;

			_roomManager.Rooms.Add(roomName, new ChatRoom { CreatedByUser = createdByUser });

			await Clients.Caller.SendAsync("RoomCreated", roomName);
		}

		public async Task JoinRoom(string roomName)
		{
			// Check if name exists
			ChatRoom room = null;
			if (!_roomManager.Rooms.TryGetValue(roomName, out room))
				return;

			room.ConnectionIds.Add(Context.ConnectionId);

			_roomManager.Rooms[roomName] = room;

			await Clients.Caller.SendAsync("RoomJoined", roomName);

			return;
		}

		//public async Task RoomJoined(string roomName)
		//{

		//}

		public async Task SendMessage(string roomName, string message)
		{
			ChatRoom room = null;
			if (!_roomManager.Rooms.TryGetValue(roomName, out room))
				return;

			await Clients.Clients(room.ConnectionIds).SendAsync("ReceiveMessage", message);

			return;
		}

		private bool RoomExists(string roomName)
		{
			return _roomManager.Rooms.ContainsKey(roomName);
		}
	}
}
