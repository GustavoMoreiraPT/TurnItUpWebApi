using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.Users;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Application.Services.Interfaces
{
    public interface IUsersService
    {
        Task<IdentityResult> CreateUserAsync(RegisterDto user, string password);

        Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password);

        Task<string> GenerateToken(ClaimsIdentity identity, string userName, JsonSerializerSettings serializerSettings);
    }
}
