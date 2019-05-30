using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.ValueObjects
{
    public class LanguageCountry
    {
        public int Id { get; set; }

        public int CountryGroupId { get; set; }

        public string Name { get; set; }

        public string Language { get; set; }

        public virtual CountryGroup Group { get; set; }
    }
}
