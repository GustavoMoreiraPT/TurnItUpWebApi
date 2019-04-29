using Application.Dto.Countries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Interfaces
{
    public interface ICountriesService
    {
        List<Country> GettAllCountries(string language);
    }
}
