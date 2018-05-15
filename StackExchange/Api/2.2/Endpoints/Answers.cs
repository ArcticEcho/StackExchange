﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Api.v22.Types;
using StackExchange.Net;

namespace StackExchange.Api.v22.Endpoints
{
	public static class Answers
	{
		public static Result<Answer[]> GetAll(QueryOptions options = null)
		{
			return GetAllAsync(options).Result;
		}

		public static async Task<Result<Answer[]>> GetAllAsync(QueryOptions options = null)
		{
			if (options == null)
			{
				options = new QueryOptions
				{
					Site = Constants.DefaultSite
				};
			}
			else if (string.IsNullOrEmpty(options.Site))
			{
				options.Site = Constants.DefaultSite;
			}

			var url = $"{Constants.BaseApiUrl}/answers?{options.Query}";

			var result = await HttpRequest.GetWithStatusAsync(url);

			if (string.IsNullOrEmpty(result.Body))
			{
				return new Result<Answer[]>
				{
					ErrorId = (int)result.Status,
					ErrorName = result.Status.ToString()
				};
			}

			return JsonConvert.DeserializeObject<Result<Answer[]>>(result.Body);
		}

		public static Result<Answer[]> GetByIds(IEnumerable<int> ids, QueryOptions options = null)
		{
			throw new NotImplementedException();
		}

		public static Result<Answer[]> GetByQuestionIds(IEnumerable<int> questionIds, QueryOptions options = null)
		{
			throw new NotImplementedException();
		}

		public static Result<Answer[]> GetByUserIds(IEnumerable<int> userIds, QueryOptions options = null)
		{
			throw new NotImplementedException();
		}

		public static Result<Answer[]> GetTopAnswersByUserByTags(int userId, IEnumerable<string> tags, QueryOptions options = null)
		{
			throw new NotImplementedException();
		}
	}
}
