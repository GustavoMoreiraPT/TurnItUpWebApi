using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TurnItUpWebApi.Middleware
{
	public static class SecurityPolicyMiddleware
	{
		public static IServiceCollection AddClaimsPolicy(this IServiceCollection services, IConfiguration configuration)
		{

			services.AddAuthorization(options =>
			{
				options.AddPolicy("PongViewer", policy => policy.RequireClaim("PongVisualizer"));
			});

			return services;
		}
	}
}
