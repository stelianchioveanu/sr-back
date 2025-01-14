using Microsoft.AspNetCore.Mvc;
using SR.Models;
using SR.Services.Interfaces;

namespace SR.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecombeeController : Controller
{
    private readonly IRecombeeService _recombeeService;

    public RecombeeController(IRecombeeService recombeeService)
    {
        _recombeeService = recombeeService;
    }
    
    // [HttpPost("AddItemProperties")]
    // public async Task<IActionResult> AddItemProperties()
    // {
    //     await _recombeeService.AddItemPropertiesAsync();
    //     return Ok();
    // }
    //
    // [HttpPost("AddUserProperties")]
    // public async Task<IActionResult> AddUserProperties()
    // {
    //     await _recombeeService.AddUserPropertiesAsync();
    //     return Ok();
    // }
    //
    // [HttpPost("AddItems/{startIndex}/{endIndex}")]
    // public async Task<IActionResult> AddItems([FromRoute] int startIndex, [FromRoute] int endIndex)
    // {
    //     return Ok(await _recombeeService.AddItemsAsync(startIndex, endIndex));
    // }
    
    // [HttpPost("UpdateUsers")]
    // public async Task<IActionResult> UpdateUsers()
    // {
    //     await _recombeeService.UpdateUsers();
    //     return Ok();
    // }
    
    [HttpPost("AddNewUser")]
    public async Task<ActionResult<string>> AddNewUser([FromBody] UserModel user)
    {
        var userId = await _recombeeService.AddUser(user);
        if (userId == null)
        {
            return Ok();
        }
        return Ok(userId);
    }
    
    [HttpGet("GetUser/{email}")]
    public async Task<ActionResult<string>> GetUser([FromRoute] string email)
    {
        var userId = await _recombeeService.GetUserId(email);
        if (userId == null)
        {
            return Ok();
        }
        return Ok(userId);
    }
    
    [HttpGet("GetMovie")]
    public async Task<IActionResult> GetMovie([FromQuery] string search)
    {
        var movies = await _recombeeService.GetMovie(search);
        return Ok(movies);
    }
    
    [HttpPost("AddView")]
    public async Task<IActionResult> AddView([FromBody] InteractionModel model)
    {
        await _recombeeService.AddView(model);
        return Ok();
    }
    
    [HttpPost("AddLike")]
    public async Task<IActionResult> AddLike([FromBody] InteractionModel model)
    {
        await _recombeeService.AddLike(model);
        return Ok();
        
    }
    
    [HttpPost("AddDislike")]
    public async Task<IActionResult> AddDislike([FromBody] InteractionModel model)
    {
        await _recombeeService.AddDislike(model);
        return Ok();
        
    }
    
    [HttpPost("AddBookmark")]
    public async Task<IActionResult> AddBookmark([FromBody] InteractionModel model)
    {
        await _recombeeService.AddBookmark(model);
        return Ok();
        
    }
    
    [HttpGet("GetBookmarks")]
    public async Task<IActionResult> GetBookmarks([FromQuery] Guid userId)
    {
        return Ok(await _recombeeService.GetBookmarks(userId));
    }
    
    [HttpGet("GetLikes")]
    public async Task<IActionResult> GetLikes([FromQuery] Guid userId)
    {
        return Ok(await _recombeeService.GetLikes(userId));
    }
    
    [HttpGet("GetRecommendation")]
    public async Task<IActionResult> GetRecommendation([FromQuery] Guid userId)
    {
        return Ok(await _recombeeService.GetRecommendation(userId));
    }
    
    [HttpGet("GetNextRecommendation")]
    public async Task<IActionResult> GetNextRecommendation([FromQuery] string recommendationId, [FromQuery] Guid userId)
    {
        return Ok(await _recombeeService.GetNextRecommendation(recommendationId, userId));
        
    }
    
    // [HttpPost("AddViews")]
    // public async Task<IActionResult> AddViews()
    // {
    //     await _recombeeService.AddViews();
    //     return Ok();
    // }
    //
    // [HttpGet("GetRecommendations")]
    // public async Task<IActionResult> GetRecommendations()
    // {
    //     await _recombeeService.GetRecommendations();
    //     return Ok();
    //     
    // }
}