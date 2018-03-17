﻿using System.Collections.Generic;
using System.Net;
using RestSharp;

namespace StackExchange.Chat.Actions.Message
{
	public class ClearStars : ChatAction
	{
		private readonly int messageId;

		internal override Method RequestMethod => Method.POST;

		internal override string Endpoint => $"https://{Host}/messages/{messageId}/unstar";

		internal override bool RequiresFKey => true;

		internal override bool RequiresAuthCookies => true;

		internal override ActionPermissionLevel RequiredPermissionLevel => ActionPermissionLevel.RoomOwner;



		public ClearStars(int messageId)
		{
			this.messageId = messageId;
		}



		internal override object ProcessResponse(HttpStatusCode status, string response)
		{
			return status == HttpStatusCode.OK &&
				response.ToUpperInvariant() == "\"OK\"";
		}
	}
}