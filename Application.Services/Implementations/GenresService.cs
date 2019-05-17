using Application.Dto.Users;
using Application.Services.Interfaces;
using Data.Repository.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Implementations
{
    public class GenresService : IGenresService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public GenresService(ApplicationDbContext context)
        {
            this.applicationDbContext = context;
        }

        public List<GenreDto> GettAllGenres(string language)
        {
            var allGenres = this.applicationDbContext
                .LanguageGenres
                .Where(x => x.Language == language);

            return allGenres.Select(x => new GenreDto { Name = x.Name }).ToList();
        }
    }
}
