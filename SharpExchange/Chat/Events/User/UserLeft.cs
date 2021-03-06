﻿using System;
using Newtonsoft.Json.Linq;

namespace SharpExchange.Chat.Events.User
{
	public class UserLeft : ChatEventDataProcessor, IChatEventHandler<int>
	{
		private EventType[] eventType = new[] { EventType.UserLeft };

		public override EventType[] Events => eventType;

		public event Action<int> OnEvent;

		public override void ProcessEventData(EventType _, JToken data)
		{
			var userId = data.Value<int>("user_id");

			OnEvent?.Invoke(userId);
		}
	}
}
