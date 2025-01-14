using SR.Entities;

namespace SR.Services.Interfaces;

public interface ITMDBService
{
    Task<MovieTMDB?> GetMovieDetailsByIdAsync(string movieId);
}