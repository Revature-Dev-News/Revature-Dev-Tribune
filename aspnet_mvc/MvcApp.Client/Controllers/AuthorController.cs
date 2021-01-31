using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using MvcApp.Client.Models.Author;
using System.Collections.Generic;
using Newtonsoft.Json;
using MvcApp.Client.Models.Shared;

namespace MvcApp.Client.Controllers
{
  [Route("[controller]")] // route parser
  public class AuthorController : Controller // test change
  {
    private string apiUrl = "https://localhost:5001/";
    private HttpClient _http;

    public AuthorController(){

      HttpClientHandler clientHandler = new HttpClientHandler();
      clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
      _http = new HttpClient(clientHandler);
    }


    [HttpGet]
    public IActionResult Home()
    {
        ViewBag.Title = "Login";
        return View("Home");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthorViewModel authorViewModel)
    {
        System.Console.WriteLine("Login");

        System.Console.WriteLine("Email: " + authorViewModel.Email);
        System.Console.WriteLine("Password: " + authorViewModel.Password);

        var response = await _http.GetAsync(apiUrl + "Article/articles");

        var jsonResponse = await response.Content.ReadAsStringAsync();
        //System.Console.WriteLine(jsonResponse);

        var ObjOrderList = JsonConvert.DeserializeObject<List<ArticleViewModel>>(jsonResponse);
        //ObjOrderList.ForEach(m => System.Console.WriteLine(m.Name));

        return await Task.FromResult(View("AuthorMain", ObjOrderList));

        /*
        return View("AuthorMain", new List<ArticleViewModel> {
          new ArticleViewModel("First", false),
          new ArticleViewModel("Second", false),
          new ArticleViewModel("Third", true),
          new ArticleViewModel("Four", true),
          new ArticleViewModel("Five", true),
          new ArticleViewModel("Six", true),
        });
        */
    }

    /*
    [HttpGet("show_article_creator")]
    public async Task<IActionResult> ShowArticleCreator()
    {
      // Get the topics
      var response = await _http.GetAsync(apiUrl + "Topic/topics");

      if (response.IsSuccessStatusCode)
      {
        var JsonResponse = await response.Content.ReadAsStringAsync();

        var TopicViewModels = JsonConvert.DeserializeObject<List<TopicViewModel>>(JsonResponse);
        return await Task.FromResult(View("ArticleCreator", TopicViewModels));
      }
      return View("Error");
    }
    */

    [HttpGet("view_article")]
    public IActionResult ViewArticle()
    {
      return View("AuthorArticleViewer", "tempValue");
    }

    [HttpGet("temp")]
    public async Task<IActionResult> Get()
    {
      var response = await _http.GetAsync(apiUrl + "Topic/topics");

      if (response.IsSuccessStatusCode)
      {
        var jsonResponse = await response.Content.ReadAsStringAsync();
        // System.Console.WriteLine(jsonResponse);

        var ObjOrderList = JsonConvert.DeserializeObject<List<TopicViewModel>>(jsonResponse);
        return await Task.FromResult(View("home", ObjOrderList));
      }
      return View("Error");
    }
  }
}
