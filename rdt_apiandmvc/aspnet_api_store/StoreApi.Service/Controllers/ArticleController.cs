using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StoreApi.Domain.Models;

namespace StoreApi.Service.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ArticleController : ControllerBase
  {
    private readonly RDTRepo _repo;

    public ArticleController(RDTRepo repository)
    {
      _repo = repository;
    }

    [HttpGet("articles")]
    public async Task<IActionResult> GetArticle()
    {
      var Article = _repo.GetArticles();
      System.Console.WriteLine(Article);
      return await Task.FromResult(Ok(Article));
    }
    [HttpPost("create_article")]
    public async Task<IActionResult> CreateArticle(Article article)
    {
      // get the author and put it into article (similar reasoning to the comment directly below this one)
      article.Author = _repo.GetAuthors().FirstOrDefault(a => a.EntityId == article.Author.EntityId);

      // get the assoc topic and add it to the article (needed bc it looks like even when translating the topic view model to topic in an article, it still isn't equiv)
      article.Topic = _repo.GetTopics().FirstOrDefault(t => t.EntityId == article.Topic.EntityId);

      var createdArticle = _repo.CreateArticle(article);

      var articleJsonStr = JsonConvert.SerializeObject(createdArticle);

      return await Task.FromResult(Ok(articleJsonStr));
    }
    [HttpPut("update_article")]
    public async Task<IActionResult> UpdateArticle(Article article)
    {
      _repo.UpdateArticle(article);
      return await Task.FromResult(Ok());
    }

    [HttpDelete("delete_article/{id}")]
    public async Task<IActionResult> DeleteArticle(long id)
    {
      _repo.DeleteArticle(id);
      return await Task.FromResult(Ok());
    }

    [HttpGet("GetArticleByTopic/{topicName}")]
    public async Task<IActionResult> GetArticlesByTopic(string topicName)
    {
      var articles = _repo.GetArticlesByTopic(topicName);
      return await Task.FromResult(Ok(articles));
    }

    [HttpGet("GetArticleByEmail")]
    public async Task<IActionResult> GetArticlesByAuthorEmail(Author author)
    {
      var article = _repo.GetArticlesByGivenEmail(author.Email);
      return await Task.FromResult(Ok(article));
    }
  }
}
