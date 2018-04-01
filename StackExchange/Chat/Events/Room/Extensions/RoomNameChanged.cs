﻿using System;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events.Room.Extensions
{
	public static partial class Extensions
	{
		public static RoomNameChanged AddRoomNameChangedEventHandler<T>(this RoomWatcher<T> rw, Action<Chat.Room> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new RoomNameChanged();

			eventProcessor.OnEvent += () =>
			{
				var room = new Chat.Room(rw.Host, rw.RoomId);

				callback(room);
			};

			rw.EventRouter.EventProcessors.Add(eventProcessor);

			return eventProcessor;
		}
	}
}