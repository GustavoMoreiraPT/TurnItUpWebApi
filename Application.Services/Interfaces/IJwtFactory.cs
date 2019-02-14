using Application.Dto.Users;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IJwtFactory
    {
	    Task<AccessToken> GenerateEncodedToken(string id, string userName);

		Task<AccessToken> GenerateEncodedToken(string userName, ClaimsIdentity identity);

        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}
