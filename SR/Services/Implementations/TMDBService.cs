using SR.Entities;
using SR.Services.Interfaces;

namespace SR.Services.Implementations;

public class TMDBService : ITMDBService
{
    private const string BaseUrl = "https://api.themoviedb.org/3/movie/";
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    
    public TMDBService(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }

    public async Task<MovieTMDB?> GetMovieDetailsByIdAsync(string movieId)
    {
        try
        {
            var requestUrl = $"{BaseUrl}{movieId}?api_key={_apiKey}";
            var movie = await _httpClient.GetFromJsonAsync<MovieTMDB>(requestUrl);
            return movie;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Eroare la apelul API TMDb: {ex.Message}");
            return null;
        }
    }
}