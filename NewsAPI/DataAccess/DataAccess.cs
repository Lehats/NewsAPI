using NewsAPI.Entities;
using NewsAPI.Helpers;
using Newtonsoft.Json;

namespace NewsAPI.DataAccess;

public class DataAccess : IDataAccess
{
    private readonly HttpClient _client;
    private readonly string _token = "925d2cb933f8d012fe447aba723deef6";
    private readonly string _baseUrl;
    public DataAccess()
    {
        _client = new HttpClient();
        _baseUrl = $"https://gnews.io/api/v4/search?token={_token}";
    }

    public async Task<List<NewsArticle>> GetNewsArticlesFromGNewsApi(string keyWords, string? authorName, int? amount)
    {
        var requestUrl = $"{_baseUrl}&q={keyWords}";
        requestUrl = amount != null ? $"{requestUrl}&max={amount}" : requestUrl;

        var response = await _client.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();

        var data = JsonConvert.DeserializeObject<GNewsApiResponse>(responseBody);
        if (data == null) return new List<NewsArticle>();

        return authorName != null ? data.Articles.Where(x => x.Source.Name.Equals(authorName)).ToList() : data.Articles;
    }

    public async Task<List<NewsArticle>> GetNewsArticlesByTitleFromGNewsApi(string title)
    {
        var requestUrl = $"{_baseUrl}&q={title}&in=title";

        var response = await _client.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();

        var data = JsonConvert.DeserializeObject<GNewsApiResponse>(responseBody);
        return data?.Articles.Where(x => x.Title.Equals(title.RemoveQuotes())).ToList() ?? new List<NewsArticle>();
    }

}