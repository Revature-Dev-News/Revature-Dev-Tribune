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
    }

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
