using BlazorChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorChat
{
	public class RoomManager
	{
		public Dictionary<string, ChatRoom> Rooms;

		public RoomManager()
		{
			Rooms = new Dictionary<string, ChatRoom>();
		}
	}
}
