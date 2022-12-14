namespace NewsAPI.Entities;

public class NewsArticle
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Content { get; set; } = "";
    public string Url { get; set; } = "";
    public string Image { get; set; } = "";
    public DateTime PublishedAt { get; set; } = new DateTime();
    public Source Source { get; set; } = new Source();
}