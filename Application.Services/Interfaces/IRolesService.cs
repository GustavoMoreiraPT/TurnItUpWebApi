using Application.Dto.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Interfaces
{
    public interface IRolesService
    {
        List<RoleDto> GetAllRoles(string language);
    }
}
