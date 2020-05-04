using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorChat.Models
{
	public class ChatMessage
	{
		public string Username { get; set; }
		public DateTime DateSent { get; set; }
		public string Text { get; set; }
	}
}
