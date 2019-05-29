using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Genres
{
    public class LanguageGenrer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Language { get; set; }

        public int LanguageGroupId { get; set; }

        public virtual GenrerGroup GenrerGroup { get; set; }
    }
}
