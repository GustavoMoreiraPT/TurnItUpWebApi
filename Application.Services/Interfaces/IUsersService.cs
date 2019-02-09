using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
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

        Task<string> GenerateToken(ClaimsIdentity identity, string userName, JsonSerializerSettings serializerSettings);

        Task<AppUser> FindByEmailAsync(string email);

        Task<AppUser> FindByNameAsync(string email);

        Task<IdentityResult> CreateUserAsync(AppUser user, FacebookUserData facebookUserData, string password);
    }
}
