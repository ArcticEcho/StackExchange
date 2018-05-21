﻿using System;
using Newtonsoft.Json.Linq;
using StackExchange.Auth;
using StackExchange.Chat.Events;
using StackExchange.Chat.Events.User.Extensions;
using StackExchange.Net.WebSockets;
using StackExchange.Chat.Actions;
using System.Threading.Tasks;
using StackExchange.Chat;

namespace Demo
{
	public class AllData : ChatEventDataProcessor, IChatEventHandler<string>
	{
		// The type of event we want to process.
		public override EventType Event => EventType.All;

		public event Action<string> OnEvent;

		// Process the incoming JSON data coming from the RoomWatcher's
		// WebSocket. In this example, we just stringify the object and
		// invoke any listeners.
		public override void ProcessEventData(JToken data) => OnEvent?.Invoke(data.ToString());
	}

	public class Program
	{
		// This stuff should ideally be loaded in from a configuration provider.
		private const string roomUrl = "https://chat.stackoverflow.com/rooms/167908";

		private static void Main(string[] args) => Demo().Wait();

		private static async Task Demo()
		{
			// Fetch your account's credentials from somewhere.
			var auth = new EmailAuthenticationProvider("", "");

			// Create an instance of the ActionScheduler. This will
			// allow us to execute chat actions like: posting messages,
			// kicking users, moving messages, etc.
			using (var actionScheduler = new ActionScheduler(auth, roomUrl))
			{
				// Create an instance of the RoomWatcher class. Here we
				// specify (via the type parameter) what WebSocket implementation
				// we'd like to use. This class allows you to subscribe to chat events.
				using (var roomWatcher = new RoomWatcher<DefaultWebSocket>(auth, roomUrl))
				{
					// Subscribe to the UserMentioned event.
					roomWatcher.AddUserMentionedEventHandler(async m =>
					{
						await actionScheduler.CreateReplyAsync("hello!", m.MessageId);

						/// Do stuff ...
					});

					// Besides being able to subscribe to the default events,
					// you can also create (and listen to) your own. Your class must
					// implement the ChatEventDataProcessor class, you can also
					// optionally implement IChatEventHandler or IChatEventHandler<T>.
					var customEventHanlder = new AllData();

					// Add a very basic handler.
					customEventHanlder.OnEvent += data => Console.WriteLine(data);

					// Add our custom event handler so we can
					// begin processing the incoming event data.
					roomWatcher.EventRouter.AddProcessor(customEventHanlder);

					// Post a simple message.
					var messageId = await actionScheduler.CreateMessageAsync("Hello world.");

					while (Console.ReadKey(true).Key != ConsoleKey.Q)
					{

					}
				}
			}
		}
	}
}
