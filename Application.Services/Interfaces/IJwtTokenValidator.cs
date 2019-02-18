using System.Security.Claims;

namespace Application.Services.Interfaces
{
	public interface IJwtTokenValidator
	{
		ClaimsPrincipal GetPrincipalFromToken(string token, string signingKey);
	}
}
