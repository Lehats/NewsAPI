namespace NewsAPI.Entities;

public class GNewsApiResponse
{
    public int Count { get; set; }
    public List<NewsArticle> Articles { get; set; } = new List<NewsArticle>();
}