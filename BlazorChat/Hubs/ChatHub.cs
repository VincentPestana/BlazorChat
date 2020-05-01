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
		public async Task<string> CreateRoom(string roomName, string createdByUser)
		{
			// Check if name exists
			if (RoomExists(roomName))
				return "Room name exists";

			_rooms.Add(roomName, new ChatRoom { CreatedByUser = createdByUser });

			return "";
		}

		public async Task<string> JoinRoom(string roomName, string userName)
		{

		}

		private bool RoomExists(string roomName)
		{
			return _rooms.ContainsKey(roomName);
		}
	}
}
