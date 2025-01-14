namespace SR.Dtos;

public class RecommendationDto
{
    public string RecommendationId { get; set; }
    public IList<MovieDto> Movies { get; set; }
}