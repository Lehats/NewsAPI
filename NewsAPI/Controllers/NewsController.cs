using Microsoft.AspNetCore.Mvc;
using NewsAPI.Entities;
using NewsAPI.Helpers;
using NewsAPI.Services.News;
using System.ComponentModel.DataAnnotations;

namespace NewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        /// <summary>
        /// Allows you to fetch news articles based on keywords.
        /// </summary>
        /// <param name="keywords">Filter for title, description or content of the news article.
        /// Use commas to separate multiple keywords. Text between commas is one keyword and must match the result exactly. Only one keyword must occur in the article to be returned.
        /// </param>
        /// <param name="authorName">Filter for author's name. If provided, must match exactly.</param>
        /// <param name="amount">If provided, returns the first N filtered news articles.</param>
        [HttpGet("GetNewsArticles")]
        public async Task<ActionResult<Response<NewsArticle>>> GetNewsArticles([Required]string keywords, string? authorName, int? amount)
        {
            var modifiedKeywords = keywords.CombineKeyWords();
            var result = await _newsService.GetNewsArticles(modifiedKeywords, authorName, amount);
            return Ok(result);
        }

        /// <summary>
        /// Allows you to fetch news articles with a specific title.
        /// </summary>
        /// <param name="title">Filter for title of the news article.
        /// Title must match the result exactly.
        /// </param>
        [HttpGet("GetNewsArticlesById")]
        public async Task<ActionResult<Response<NewsArticle>>> GetNewsArticlesByTitle(string title)
        {
            var modifiedTitle = title.CombineKeyWords();
            var result = await _newsService.GetNewsArticlesByTitle(modifiedTitle);
            return Ok(result);
        }
    }
}
