using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace TurnItUpWebApi.Middleware
{
	public static class HttpStatusCodeExceptionMiddlewareExtensions
	{
		public static IApplicationBuilder UseHttpStatusCodeExceptionMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<HttpStatusCodeExceptionMiddleware>();
		}
	}
}
