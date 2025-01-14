using System.Net;
using Recombee.ApiClient.Bindings;
using SR.Dtos;
using SR.Entities;
using SR.Models;

namespace SR.Services.Interfaces;

public interface IRecombeeService
{
    // Task<IEnumerable<HttpStatusCode>> AddItemsAsync(int startIndex, int endIndex);
    // Task AddItemPropertiesAsync();
    // Task AddUserPropertiesAsync();
    // Task UpdateUsers();
    Task<string?> AddUser(UserModel user);
    Task<string?> GetUserId(string email);
    Task<List<MovieDto>> GetMovie(string search);
    Task AddLike(InteractionModel model);
    Task AddDislike(InteractionModel model);
    Task AddView(InteractionModel model);
    Task AddBookmark(InteractionModel model);
    Task<List<MovieDto>> GetLikes(Guid userId);
    Task<List<MovieDto>> GetBookmarks(Guid userId);
    Task<RecommendationDto> GetRecommendation(Guid userId);
    Task<RecommendationDto> GetNextRecommendation(string recommendationId, Guid userId);
}