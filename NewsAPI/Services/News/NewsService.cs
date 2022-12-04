using NewsAPI.DataAccess;
using NewsAPI.Entities;

namespace NewsAPI.Services.News;

public class NewsService : INewsService
{
    private readonly IDataAccess _dataAccess;

    public NewsService(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public async Task<Response<NewsArticle>> GetNewsArticles(string keyWords, string? authorName, int? amount)
    {
        var result = await _dataAccess.GetNewsArticlesFromGNewsApi(keyWords, authorName, amount);
        return new Response<NewsArticle>
        {
            Count = result.Count,
            Articles = result
        };
    }

    public async Task<Response<NewsArticle>> GetNewsArticlesByTitle(string title)
    {
        var result = await _dataAccess.GetNewsArticlesByTitleFromGNewsApi(title);
        return new Response<NewsArticle>
        {
            Count = result.Count,
            Articles = result
        };
    }
}