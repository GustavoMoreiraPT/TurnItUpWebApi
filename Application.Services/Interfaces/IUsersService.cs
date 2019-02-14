using System.Security.Claims;
using System.Threading.Tasks;
using Application.Dto.Users;
using Domain.Model.Users;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using static Infrastructure.CrossCutting.Helpers.FacebookApiResponses;

namespace Application.Services.Interfaces
{
    public interface IUsersService
    {
        Task<IdentityResult> CreateUserAsync(RegisterDto user, string password);

        Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password);

        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);

        Task<LoginResponse> GenerateToken(
            ClaimsIdentity identity,
            string userName,
            string password,
            string remoteIpAddress,
            JsonSerializerSettings serializerSettings
            );

        Task<AppUser> FindByEmailAsync(string email);

        Task<AppUser> FindByNameAsync(string email);

        Task<IdentityResult> CreateUserAsync(
            AppUser user,
            FacebookUserData facebookUserData,
            string password
            );

        Task<string> AddRefreshToken(
            string token,
            string userName,
            string remoteIpAddress,
            double daysToExpire = 5
            );

        Task<LoginResponse> RefreshToken(ExchangeRefreshTokenRequest refreshTokenRequest);
    }
}
