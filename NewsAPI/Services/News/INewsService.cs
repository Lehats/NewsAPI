using NewsAPI.Entities;

namespace NewsAPI.Services.News;

public interface INewsService
{
    Task<Response<NewsArticle>> GetNewsArticles(string keyWords, string? authorName, int? amount);

    Task<Response<NewsArticle>> GetNewsArticlesByTitle(string title);
}