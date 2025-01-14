namespace SR.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public List<string> Genres { get; set; }
}