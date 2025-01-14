using System.Net;
using System.Text.Json;
using Microsoft.VisualBasic.FileIO;
using Recombee.ApiClient;
using Recombee.ApiClient.ApiRequests;
using Recombee.ApiClient.Bindings;
using SR.Dtos;
using SR.Entities;
using SR.Models;
using SR.Services.Interfaces;
using User = SR.Entities.User;

namespace SR.Services.Implementations;

public class RecombeeService : IRecombeeService
{
    private readonly RecombeeClient _client;
    private readonly ITMDBService _tmdbService;
    
    // List<User> users = new List<User>
    //         {
    //         new User
    //         {
    //             Id = Guid.Parse("fa5c95f4-80c6-4ee2-ae64-93ec7ab20668"),
    //             FullName = "Lavinia Munteanu",
    //             Email = "lavinia.munteanu@example.com",
    //             Genres = new List<string> { "Drama", "Romance", "Adventure" }
    //         },
    //         new User
    //         {
    //             Id = Guid.Parse("2d2a83cf-40ad-4efb-9b57-8b9a795573a9"),
    //             FullName = "Diana Lupu",
    //             Email = "diana.lupu@example.com",
    //             Genres = new List<string> { "Sci-Fi", "Action", "Thriller" }
    //         },
    //         new User
    //         {
    //             Id = Guid.Parse("68833ae7-b508-4a38-b97e-dc0357ff29b3"),
    //             FullName = "Radu Matei",
    //             Email = "radu.matei@example.com",
    //             Genres = new List<string> { "Comedy", "Drama", "Family" }
    //         },
    //         new User
    //         {
    //             Id = Guid.Parse("e3f6ec7c-4305-4262-84c7-e2f56ad03d34"),
    //             FullName = "Mihail Iacob",
    //             Email = "mihail.iacob@example.com",
    //             Genres = new List<string> { "Horror", "Mystery", "Thriller" }
    //         },
    //         new User
    //         {
    //             Id = Guid.Parse("6a7f2f9e-7a07-4a52-a5f7-20c65185a690"),
    //             FullName = "Raluca Popescu",
    //             Email = "raluca.popescu@example.com",
    //             Genres = new List<string> { "Romance", "Adventure", "Action" }
    //         },
    //         new User
    //         {
    //             Id = Guid.Parse("104ffb9d-2125-4456-b9a2-e07b6cb4ea0b"),
    //             FullName = "Alexandru Călinescu",
    //             Email = "alexandru.calinescu@example.com",
    //             Genres = new List<string> { "Science Fiction", "Action", "Adventure" }
    //         },
    //         new User
    //         {
    //             Id = Guid.Parse("5fc6603f-8d56-49d2-a77d-76e72d828d2b"),
    //             FullName = "Georgiana Dima",
    //             Email = "georgiana.dima@example.com",
    //             Genres = new List<string> { "Fantasy", "Adventure", "Action" }
    //         },
    //         new User
    //         {
    //             Id = Guid.Parse("6d61a847-489f-4d2d-bb3e-90053387f563"),
    //             FullName = "Daniela Drăghici",
    //             Email = "daniela.draghici@example.com",
    //             Genres = new List<string> { "Mystery", "Science Fiction", "Thriller" }
    //         },
    //         new User
    //         {
    //             Id = Guid.Parse("cd7955ac-75be-4097-b775-7bb8ac2c9a6d"),
    //             FullName = "Vlad Popa",
    //             Email = "vlad.popa@example.com",
    //             Genres = new List<string> { "Comedy", "Action", "Romance" }
    //         },
    //         new User
    //         {
    //             Id = Guid.Parse("5976a4fa-d0ff-4888-bc6c-e2e6180fc4d7"),
    //             FullName = "Simona Chiriac",
    //             Email = "simona.chiriac@example.com",
    //             Genres = new List<string> { "Documentary", "Drama", "Historical" }
    //         },
    //             new User
    //         {
    //             Id = Guid.Parse("d4f5b67c-8fa1-45b9-8d4c-bfbb13f8b25f"),
    //             FullName = "Ion Popescu",
    //             Email = "ion.popescu@example.com",
    //             Genres = new List<string> { "Action", "Drama", "Comedy" }
    //         },
    //         new User
    //         {
    //             Id = Guid.Parse("c324f9cd-d4c1-4527-9c69-31e3cdb22191"),
    //             FullName = "Maria Ionescu",
    //             Email = "maria.ionescu@example.com",
    //             Genres = new List<string> { "Romance", "Fantasy", "Horror" }
    //         },
    //         new User
    //         {
    //             Id = Guid.Parse("ecb7a0fb-9333-4a97-b96a-5a883f0cc083"),
    //             FullName = "George Vasilescu",
    //             Email = "george.vasilescu@example.com",
    //             Genres = new List<string> { "Science Fiction", "Tragedy", "Mystery" }
    //         },
    //         new User
    //         {
    //             Id = Guid.Parse("f870e279-089e-4f6c-b5be-8be1b89c6cc5"),
    //             FullName = "Ana Georgescu",
    //             Email = "ana.georgescu@example.com",
    //             Genres = new List<string> { "Action", "Adventure", "Thriller" }
    //         },
    //         new User
    //         {
    //             Id = Guid.Parse("fced91b0-9271-496e-98ed-736fa2b9f9b1"),
    //             FullName = "Andrei Dumitru",
    //             Email = "andrei.dumitru@example.com",
    //             Genres = new List<string> { "Comedy", "Family", "Animation" }
    //         },
    //         new User
    //         {
    //             Id = Guid.Parse("88b12262-5c2f-4ad7-bb1b-1f56cd8f22c2"),
    //             FullName = "Elena Popa",
    //             Email = "elena.popa@example.com",
    //             Genres = new List<string> { "Horror", "Thriller", "Mystery" }
    //         },
    //         new User
    //         {
    //             Id = Guid.Parse("b5d7038d-206e-40b5-b218-1a5e6a31b88d"),
    //             FullName = "Vasile Ionescu",
    //             Email = "vasile.ionescu@example.com",
    //             Genres = new List<string> { "Romance", "Drama", "Historical" }
    //         },
    //         new User
    //         {
    //             Id = Guid.Parse("779fc735-3a72-402b-b48c-89832f3bce1c"),
    //             FullName = "Cristian Toma",
    //             Email = "cristian.toma@example.com",
    //             Genres = new List<string> { "Action", "Adventure", "Fantasy" }
    //         },
    //         new User
    //         {
    //             Id = Guid.Parse("cf1ac4c9-38cd-4a1d-b38f-15a0b44cd713"),
    //             FullName = "Ioana Stanescu",
    //             Email = "ioana.stanescu@example.com",
    //             Genres = new List<string> { "Science Fiction", "Action", "Thriller" }
    //         },
    //         new User
    //         {
    //             Id = Guid.Parse("4a89f87b-7195-42db-b88f-c80e78ae5775"),
    //             FullName = "Mihai Marin",
    //             Email = "mihai.marin@example.com",
    //             Genres = new List<string> { "Documentary", "Drama", "Adventure" }
    //         }
    //     };

    public RecombeeService(string databaseId, string secretToken, ITMDBService tmdbService)
    {
        _client = new RecombeeClient(databaseId, secretToken);
        _tmdbService = tmdbService;
    }

    // public async Task<IEnumerable<HttpStatusCode>> AddItemsAsync(int startIndex, int endIndex)
    // {
    //     try
    //     {
    //         List<Movie> movies = new List<Movie>();
    //         using (TextFieldParser textFieldParser = new TextFieldParser(@"./TMDB_all_movies.csv"))
    //         {
    //             textFieldParser.TextFieldType = FieldType.Delimited;
    //             textFieldParser.SetDelimiters(",");
    //             textFieldParser.ReadFields();
    //             int index = 0;
    //
    //             while (index < startIndex)
    //             {
    //                 textFieldParser.ReadFields();
    //                 index++;
    //             }
    //             
    //             while (!textFieldParser.EndOfData && index <= endIndex)
    //             {
    //                 index++;
    //                 string[]? movieData = textFieldParser.ReadFields();
    //                 
    //                 if (movieData != null)
    //                 {
    //                     if (!string.IsNullOrEmpty(movieData[1]) && !string.IsNullOrEmpty(movieData[5]) &&
    //                         !string.IsNullOrEmpty(movieData[7]) && !string.IsNullOrEmpty(movieData[15]) &&
    //                         !string.IsNullOrEmpty(movieData[2]) && !string.IsNullOrEmpty(movieData[3]) &&
    //                         !string.IsNullOrEmpty(movieData[25]) && !string.IsNullOrEmpty(movieData[26]) &&
    //                         !string.IsNullOrEmpty(movieData[0]))
    //                     {
    //                         DateTime date;
    //                         if (!DateTime.TryParse(movieData[5], out date))
    //                         {
    //                             continue;
    //                         }
    //                         
    //                         int runtime;
    //                         
    //                         if (!int.TryParse(movieData[7].Split('.',',')[0], out runtime))
    //                         {
    //                             continue;
    //                         }
    //                         
    //                         int voteCount;
    //                         
    //                         if (!int.TryParse(movieData[3].Split('.',',')[0], out voteCount))
    //                         {
    //                             continue;
    //                         }
    //                         
    //                         int imdbVotes;
    //                         
    //                         if (!int.TryParse(movieData[26].Split('.',',')[0], out imdbVotes))
    //                         {
    //                             continue;
    //                         }
    //                         
    //                         double imdbRating;
    //                         
    //                         if (!double.TryParse(movieData[25], out imdbRating))
    //                         {
    //                             continue;
    //                         }
    //                         
    //                         double voteAverage;
    //                         
    //                         if (!double.TryParse(movieData[2], out voteAverage))
    //                         {
    //                             continue;
    //                         }
    //                         
    //                         List<string> genreList = new List<string>(movieData[15].Split(','));
    //                         genreList = genreList.ConvertAll(genre => genre.Trim());
    //                         
    //                         movies.Add(new Movie()
    //                         {
    //                             Id = movieData[0],
    //                             Title = movieData[1],
    //                             ReleaseDate = date,
    //                             Runtime = runtime,
    //                             Genres = genreList,
    //                             VoteAverage = voteAverage,
    //                             VoteCount = voteCount,
    //                             ImdbRating = imdbRating,
    //                             ImdbVotes = imdbVotes,
    //                         });   
    //                     }
    //                 }
    //             }
    //         }
    //         
    //         var requests = new List<Request>();
    //
    //         foreach (var movie in movies)
    //         {
    //             requests.Add(new AddItem(movie.Id));
    //         }
    //         var batchResponse = await _client.SendAsync(new Batch(requests));
    //         
    //         foreach (var movie in movies)
    //         {
    //             requests.Add(new SetItemValues(movie.Id, new Dictionary<string, object>
    //             {
    //                 { "title", movie.Title },
    //                 { "release_date", movie.ReleaseDate },
    //                 { "runtime", movie.Runtime },
    //                 { "genres", movie.Genres },
    //                 { "vote_average", movie.VoteAverage },
    //                 { "vote_count", movie.VoteCount },
    //                 { "imdb_rating", movie.ImdbRating },
    //                 { "imdb_votes", movie.ImdbVotes }
    //             }));
    //         }
    //         batchResponse = await _client.SendAsync(new Batch(requests));
    //         
    //         return null;
    //     }
    //     catch (Exception ex)
    //     {
    //         throw new ApplicationException($"Error adding item: {ex.Message}", ex);
    //     }
    // }
    
    // public async Task AddUserPropertiesAsync()
    // {
    //     try
    //     {
    //         await _client.SendAsync(new AddUserProperty("email", "string"));
    //         await _client.SendAsync(new AddUserProperty("full_name", "string"));
    //         await _client.SendAsync(new AddUserProperty("preferred_genres", "set"));
    //     }
    //     catch (Exception ex)
    //     {
    //         throw new ApplicationException($"Error adding item: {ex.Message}", ex);
    //     }
    // }
    
    // public async Task AddItemPropertiesAsync()
    // {
    //     try
    //     {
    //         await _client.SendAsync(new AddItemProperty("title", "string"));
    //         await _client.SendAsync(new AddItemProperty("release_date", "timestamp"));
    //         await _client.SendAsync(new AddItemProperty("runtime", "int"));
    //         await _client.SendAsync(new AddItemProperty("genres", "set"));
    //         await _client.SendAsync(new AddItemProperty("vote_average", "double"));
    //         await _client.SendAsync(new AddItemProperty("vote_count", "int"));
    //         await _client.SendAsync(new AddItemProperty("imdb_rating", "double"));
    //         await _client.SendAsync(new AddItemProperty("imdb_votes", "int"));
    //     }
    //     catch (Exception ex)
    //     {
    //         throw new ApplicationException($"Error adding item: {ex.Message}", ex);
    //     }
    // }
    
    // public async Task UpdateUsers()
    // {
    //     List<int> movies = new List<int>()
    //     {
    //         100, 10000, 1105592, 1105600, 1105744, 1105802, 235260, 235389, 235443, 235549, 235585, 235592, 304693,
    //         304708, 344823, 344831, 344656, 344530, 506973
    //     };
    //     try
    //     {
    //         foreach (var user in users)
    //         {
    //             HashSet<int> moviesIds = new HashSet<int>();
    //
    //             for (int i = 0; i < new Random().Next() % 4 + 1; i++)
    //             {
    //                 moviesIds.Add(movies[new Random().Next() % movies.Count]);
    //             }
    //             
    //             await _client.SendAsync(new SetUserValues(user.Id.ToString(), new Dictionary<string, object>
    //             {
    //                 { "fav_movies", moviesIds },
    //             }));
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         throw new ApplicationException($"Error adding item: {ex.Message}", ex);
    //     }
    // }
    
    public async Task<string?> GetUserId(string email)
    {
        try
        {
            var filter = $"'email' == \"{email}\"";
            var clients = await _client.SendAsync(new ListUsers(filter));
            var client = clients.FirstOrDefault();

            if (client == null)
            {
                return null;
            }
            else
            {
                return client.UserId;
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error get userId: {ex.Message}", ex);
        }
    }

    public async Task<List<MovieDto>> GetMovie(string search)
    {
        try
        {
            var movies = new List<MovieDto>();
            var filter = $"\"{search}\" in 'title'";
            var items = (await _client.SendAsync(new ListItems(filter, count: 5, returnProperties: true,
                includedProperties: ["title", "release_date", "imdb_rating", "vote_average", "genres"]))).ToList();

            foreach (var item in items)
            {
                var movieTmdb = await _tmdbService.GetMovieDetailsByIdAsync(item.ItemId);
                movies.Add(new MovieDto()
                {
                    Id = item.ItemId,
                    Title = item.Values["title"]?.ToString() ?? "Unknown",
                    PosterPath = movieTmdb?.Poster_path != null ? "https://image.tmdb.org/t/p/original" + movieTmdb.Poster_path : "https://motivatevalmorgan.com/wp-content/uploads/2016/06/default-movie-768x1129.jpg",
                    Description = movieTmdb?.Overview ?? "No description available",
                    ImdbRating = item.Values["imdb_rating"]?.ToString() ?? "Unknown",
                    TmdbRating = item.Values["vote_average"]?.ToString() ?? "Unknown",
                    Genres = item.Values["genres"]?.ToString() ?? "Unknown",
                });
            }
            return movies;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error add bookmark: {ex.Message}", ex);
        }
    }
    
    public async Task<string?> AddUser(UserModel user)
    {
        try
        {
            throw new Exception("nu e bine");
            var userId = Guid.NewGuid().ToString();
            await _client.SendAsync(new AddUser(userId));
            await _client.SendAsync(new SetUserValues(userId, new Dictionary<string, object>
            {
                { "email", user.Email },
                { "preferred_genres", user.Genres },
                { "full_name", user.FullName },
                { "fav_movies", user.FavMovies },
                
            }));
            
            var filter = $"'email' == \"{user.Email}\"";
            var clients = await _client.SendAsync(new ListUsers(filter));
            var client = clients.FirstOrDefault();

            if (client == null)
            {
                return null;
            }
            else
            {
                return client.UserId;
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error add user", ex);
        }
    }
    
    public async Task AddView(InteractionModel model)
    {
        try
        {
            await _client.SendAsync(new AddDetailView(model.UserId.ToString(), model.MovieId));
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error add view item: {ex.Message}", ex);
        }
    }
    
    public async Task AddLike(InteractionModel model)
    {
        try 
        {
            await _client.SendAsync(new AddRating(model.UserId.ToString(), model.MovieId, 1));
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error add like item: {ex.Message}", ex);
        }
    }
    
    public async Task AddDislike(InteractionModel model)
    {
        try 
        {
            await _client.SendAsync(new AddRating(model.UserId.ToString(), model.MovieId, -1));
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error add dislike item: {ex.Message}", ex);
        }
    }
    
    public async Task AddBookmark(InteractionModel model)
    {
        try
        {
            await _client.SendAsync(new AddBookmark(model.UserId.ToString(), model.MovieId));
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error add bookmark: {ex.Message}", ex);
        }
    }

    public async Task<List<MovieDto>> GetLikes(Guid userId)
    {
        try
        {
            var ratings = await _client.SendAsync(new ListUserRatings(userId.ToString()));
            var movies = new List<MovieDto>();

            if (ratings != null)
            {
                var ids = ratings.Where(rating => Math.Abs(rating.RatingValue - 1) < 0.1).ToList().ConvertAll(rating => rating.ItemId);
                var filter = "'itemId' in " + "{" + string.Join(", ", ids.Select(s => $"\"{s}\"")) + "}";
                var items = (await _client.SendAsync(new ListItems(filter, returnProperties: true,
                    includedProperties: ["title", "release_date", "imdb_rating", "vote_average", "genres"]))).ToList();

                foreach (var item in items)
                {
                    var movieTmdb = await _tmdbService.GetMovieDetailsByIdAsync(item.ItemId);
                    movies.Add(new MovieDto()
                    {
                        Id = item.ItemId,
                        Title = item.Values["title"]?.ToString() ?? "Unknown",
                        PosterPath = movieTmdb?.Poster_path != null ? "https://image.tmdb.org/t/p/original" + movieTmdb.Poster_path : "https://motivatevalmorgan.com/wp-content/uploads/2016/06/default-movie-768x1129.jpg",
                        Description = movieTmdb?.Overview ?? "No description available",
                        ImdbRating = item.Values["imdb_rating"]?.ToString() ?? "Unknown",
                        TmdbRating = item.Values["vote_average"]?.ToString() ?? "Unknown",
                        Genres = item.Values["genres"]?.ToString() ?? "Unknown",
                    });
                }
            }
            return movies;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error add bookmark: {ex.Message}", ex);
        }
    }
    
    public async Task<List<MovieDto>> GetBookmarks(Guid userId)
    {
        try
        {
            var bookmarks = await _client.SendAsync(new ListUserBookmarks(userId.ToString()));
            var movies = new List<MovieDto>();

            if (bookmarks != null)
            {
                var ids = bookmarks.ToList().ConvertAll(bookmark => bookmark.ItemId);
                var filter = "'itemId' in " + "{" + string.Join(", ", ids.Select(s => $"\"{s}\"")) + "}";;
                var items = (await _client.SendAsync(new ListItems(filter, returnProperties: true,
                    includedProperties: ["title", "release_date", "imdb_rating", "vote_average", "genres"]))).ToList();

                foreach (var item in items)
                {
                    var movieTmdb = await _tmdbService.GetMovieDetailsByIdAsync(item.ItemId);
                    movies.Add(new MovieDto()
                    {
                        Id = item.ItemId,
                        Title = item.Values["title"]?.ToString() ?? "Unknown",
                        PosterPath = movieTmdb?.Poster_path != null ? "https://image.tmdb.org/t/p/original" + movieTmdb.Poster_path : "https://motivatevalmorgan.com/wp-content/uploads/2016/06/default-movie-768x1129.jpg",
                        Description = movieTmdb?.Overview ?? "No description available",
                        ImdbRating = item.Values["imdb_rating"]?.ToString() ?? "Unknown",
                        TmdbRating = item.Values["vote_average"]?.ToString() ?? "Unknown",
                        Genres = item.Values["genres"]?.ToString() ?? "Unknown",
                    });
                }
            }

            movies.Sort((a, b) => String.Compare(a.Title, b.Title, StringComparison.Ordinal));
            return movies;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error add bookmark: {ex.Message}", ex);
        }
    }
    
    public async Task<RecommendationDto> GetRecommendation(Guid userId)
    {
        var user = await _client.SendAsync(new GetUserValues(userId.ToString()));
        
        List<string> genres = JsonSerializer.Deserialize<List<string>>(user.Values["preferred_genres"].ToString());
        string genreFilter = string.Join(",", genres.Select(g => $"\"{g}\""));

        string filter = null;
        if (new Random().Next() % 2 == 0)
        {
            filter = $"'genres' & {{ {genreFilter} }} != {{}}";
        }
        
        string booster = null;
        
        if (new Random().Next() % 2 == 0)
        {
            booster = "if 'imdb_rating' <= 0.3 OR 'vote_average' < 0.3 then 2.0 else 1.0";   
        }
        else
        {
            booster = "if 'release_date' < 1262304000 then 2.0 else 1.0"; 
        }
        
        var request = new RecommendItemsToUser(
            userId.ToString(),
            20,
            filter: filter,
            booster: booster,
            returnProperties: true,
            includedProperties: ["title", "release_date", "imdb_rating", "vote_average", "genres"]
        );
        request.Timeout = new TimeSpan(0, 0, 10000);
        
        try
        {
            RecommendationResponse recommendationResponse = await _client.SendAsync(request);
            var moviesList = new List<MovieDto>();

            foreach (var movie in recommendationResponse.Recomms)
            {
                if (movie == null)
                {
                    continue;
                }
                var movieTmdb = await _tmdbService.GetMovieDetailsByIdAsync(movie.Id);

                moviesList.Add(new MovieDto()
                {
                    Id = movie.Id,
                    Title = movie.Values["title"]?.ToString() ?? "Unknown",
                    PosterPath = movieTmdb?.Poster_path != null ? "https://image.tmdb.org/t/p/original" + movieTmdb.Poster_path : "https://motivatevalmorgan.com/wp-content/uploads/2016/06/default-movie-768x1129.jpg",
                    Description = movieTmdb?.Overview ?? "No description available",
                    ImdbRating = movie.Values["imdb_rating"]?.ToString() ?? "Unknown",
                    TmdbRating = movie.Values["vote_average"]?.ToString() ?? "Unknown",
                    Genres = movie.Values["genres"]?.ToString() ?? "Unknown",
                });
            }
            return new RecommendationDto()
            {
                Movies = moviesList,
                RecommendationId = recommendationResponse.RecommId,
            };
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error adding item: {ex.Message}", ex);
        }
    }
    
    public async Task<RecommendationDto> GetNextRecommendation(string recommendationId, Guid userId)
    {
        try
        {
            Random random = new Random();
            if (random.Next() % 2 == 0)
            {
                return await GetRecommendation(userId);
            }

            var request = new RecommendNextItems(recommendationId, 20);
            request.Timeout = new TimeSpan(0, 0, 10000);
            RecommendationResponse recommendationResponse = await _client.SendAsync(request);
            var moviesList = new List<MovieDto>();

            foreach (var movie in recommendationResponse.Recomms)
            {
                if (movie == null)
                {
                    continue;
                }
                var movieTmdb = await _tmdbService.GetMovieDetailsByIdAsync(movie.Id);
                if (movieTmdb == null)
                {
                    continue;
                }

                moviesList.Add(new MovieDto()
                {
                    Id = movie.Id,
                    Title = movie.Values["title"]?.ToString() ?? "Unknown",
                    PosterPath = movieTmdb.Poster_path != null ? "https://image.tmdb.org/t/p/original" + movieTmdb.Poster_path : "https://motivatevalmorgan.com/wp-content/uploads/2016/06/default-movie-768x1129.jpg",
                    Description = movieTmdb.Overview ?? "No description available",
                    ImdbRating = movie.Values["imdb_rating"]?.ToString() ?? "Unknown",
                    TmdbRating = movie.Values["vote_average"]?.ToString() ?? "Unknown",
                    Genres = movie.Values["genres"]?.ToString() ?? "Unknown",
                });
            }

            return new RecommendationDto()
            {
                Movies = moviesList,
                RecommendationId = recommendationResponse.RecommId,
            };
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error adding item: {ex.Message}", ex);
        }
    }


    // public async Task AddViews()
    // {
    //     var requests = new List<Request>();
    //     
    //     for (int i = 0; i < 10000; i++)
    //     {
    //         Random random = new Random();
    //         string userId = users[random.Next() % 20].Id.ToString();
    //         string movieId = (random.Next() % 1300000).ToString();
    //         
    //         requests.Add(new AddDetailView(userId, movieId));
    //
    //         if (random.Next() % 2 == 0)
    //         {
    //             requests.Add(new AddBookmark(userId, movieId));
    //         }
    //     }
    //     
    //     await _client.SendAsync(new Batch(requests));
    // }
    //
    // public async Task GetRecommendations()
    // {
    //     for (int i = 0; i < 10000; i++)
    //     {
    //         Random random = new Random();
    //         string userId = users[random.Next() % 20].Id.ToString();
    //         Console.WriteLine(userId);
    //
    //         var request = new RecommendItemsToUser(userId, 1, returnProperties: true);
    //         request.Timeout = new TimeSpan(0, 0, 1000);
    //         
    //         RecommendationResponse recommendationResponse = await _client.SendAsync(request);
    //         if (true)
    //         {
    //             var request2 = new AddDetailView(userId, recommendationResponse.Recomms.ElementAt(0).Id);
    //             request2.Timeout = new TimeSpan(0, 0, 1000);
    //             await _client.SendAsync(request2);
    //         }
    //     }
    // }
    
}