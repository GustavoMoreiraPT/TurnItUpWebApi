using Application.Dto.Users;
using Application.Services.Interfaces;
using Data.Repository.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Services.Implementations
{
    public class RolesService : IRolesService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public RolesService(ApplicationDbContext context)
        {
            this.applicationDbContext = context;
        }

        public List<RoleDto> GetAllRoles(string language)
        {
            var allRoles = this.applicationDbContext
                .LanguageRoles
                .Where(x => x.Language == language);

            return allRoles.Select(x => new RoleDto { Name = x.Name }).ToList();
        }
    }
}
