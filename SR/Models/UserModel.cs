namespace SR.Models;

public class UserModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public List<string> Genres { get; set; }
    public List<string> FavMovies { get; set; }
}