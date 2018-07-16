﻿using SharpExchange.Api.V22.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpExchange.Api.V22.Endpoints
{
	public static class Reputation
	{
		/// <summary>
		/// Gets a subset of the reputation changes experienced
		/// by the users identified by a set of ids.
		/// </summary>
		public static Task<Result<ReputationChange[]>> GetByUserIdsAsync(IEnumerable<int> userIds, QueryOptions options = null)
		{
			userIds.ThrowIfNullOrEmpty(nameof(userIds));
			options = options.GetDefaultIfNull();

			var idsStr = userIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/users/{idsStr}/reputation";

			return ApiRequestScheduler.ScheduleRequestAsync<ReputationChange[]>(endpoint, options);
		}
	}
}
