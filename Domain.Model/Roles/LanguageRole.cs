using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Roles
{
    public class LanguageRole
    {
        public int Id { get; set; }

        public int RoleGroupId { get; set; }

        public string Name { get; set; }

        public string Language { get; set; }

        public virtual RoleGroup Group { get; set; }
    }
}
