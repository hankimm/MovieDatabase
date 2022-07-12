using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviePro.Models.Database;
using MoviePro.Models.TMDB;

namespace MoviePro.Services.Interfaces
{
    public interface IDataMappingService
    {
        Task<Movie> MapMovieDetailAsync(MovieDetail movie);
        ActorDetail MapActorDetail(ActorDetail actor);
    }
}
