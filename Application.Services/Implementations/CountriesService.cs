using Application.Dto.Countries;
using Application.Services.Interfaces;
using Data.Repository.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Implementations
{
    public class CountriesService : ICountriesService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CountriesService(ApplicationDbContext context)
        {
            this.applicationDbContext = context;
        }

        public List<Country> GettAllCountries(string language)
        {
            var allCountries = this.applicationDbContext
                .Countries
                .Where(x => x.Language == language);

            return allCountries.Select(x => new Country { Name = x.Name, Id = x.Id, GroupId = x.CountryGroupId }).ToList();
        }
    }
}
