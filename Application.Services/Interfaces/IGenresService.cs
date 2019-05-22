using Application.Dto.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Interfaces
{
    public interface IGenresService
    {
        List<GenreDto> GettAllGenres(string language);
    }
}
