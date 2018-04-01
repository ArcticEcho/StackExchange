﻿using System;

namespace StackExchange.Chat.Events
{
	public interface IChatEventHandler<T>
	{
		event Action<T> OnEvent;
	}

	public interface IChatEventHandler
	{
		event Action OnEvent;
	}
}
