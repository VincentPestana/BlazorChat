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
		private Dictionary<string, ChatRoom> _rooms;

		public ChatHub()
		{
			
		}

		public async Task<string> CreateRoom(string roomName, string createdByUser)
		{
			// Check if name exists
			if (RoomExists(roomName))
				return "Room name exists";

			_rooms.Add(roomName, new ChatRoom { CreatedByUser = createdByUser });

			return "";
		}

		public async Task JoinRoom(string roomName)
		{
			// Check if name exists
			ChatRoom room = null;
			if (!_rooms.TryGetValue(roomName, out room))
				return;

			room.ConnectionIds.Add(Context.ConnectionId);

			_rooms[roomName] = room;

			await Clients.Caller.SendAsync("RoomJoined", roomName);

			return;
		}

		//public async Task RoomJoined(string roomName)
		//{

		//}

		public async Task<string> SendMessage(string roomName, string message)
		{
			ChatRoom room = null;
			if (!_rooms.TryGetValue(roomName, out room))
				return "Room does not exist";

			await Clients.Clients(room.ConnectionIds).SendAsync("ReceiveMessage", message);

			return "";
		}

		private bool RoomExists(string roomName)
		{
			return _rooms.ContainsKey(roomName);
		}
	}
}
