using NewsAPI.Entities;

namespace NewsAPI.DataAccess;

public interface IDataAccess
{
    Task<List<NewsArticle>> GetNewsArticlesFromGNewsApi(string keyWords, string? authorName, int? amount);
    Task<List<NewsArticle>> GetNewsArticlesByTitleFromGNewsApi(string title);

}