namespace SR.Entities;

public class Movie
{
    public string Id { get; set; }
    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int Runtime { get; set; }
    public List<string> Genres { get; set; }
    public double VoteAverage { get; set; }
    public int VoteCount { get; set; }
    public double ImdbRating { get; set; }
    public int ImdbVotes { get; set; }
}