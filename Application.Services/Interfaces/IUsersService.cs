﻿using System.Security.Claims;
using System.Threading.Tasks;
using Application.Dto.Users;
using Domain.Model.Users;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using static Infrastructure.CrossCutting.Helpers.FacebookApiResponses;
using Application.Dto.Musicians;
using Application.Dto.Users.Responses;
using System;

namespace Application.Services.Interfaces
{
    public interface IUsersService
    {
        Task<RegisterResponseDto> CreateUserAsync(RegisterCreateDto user, string password);

        Task<RegisterEditResponseDto> EditUserAsync(Guid customerId, ClaimsIdentity identity, RegisterEditDto user);

        Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password);

        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);

        Task<LoginResponse> GenerateToken(
            ClaimsIdentity identity,
            string userName,
            string password,
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
            double daysToExpire = 5
            );

        Task<LoginResponse> RefreshToken(ExchangeRefreshTokenRequest refreshTokenRequest);

        Task<int> GetCustomerIdByToken(string token);

		Task AddClaimToUser(ClaimsIdentity identity, string claimType, string claimValue);
	}
}
