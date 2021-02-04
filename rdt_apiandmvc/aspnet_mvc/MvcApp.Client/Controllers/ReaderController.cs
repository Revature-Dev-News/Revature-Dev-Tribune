
using Microsoft.AspNetCore.Mvc;
using MvcApp.Client.Models.Shared;
using MvcApp.Client.Models.Reader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text;

namespace MvcApp.Client.Controllers
{
  [Route("[controller]")]
  public class ReaderController:Controller
  {
    private string apiUrl = "https://localhost:5001/";
    private HttpClient _http;

    public ReaderController(){
      HttpClientHandler clientHandler = new HttpClientHandler();
      clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
      _http = new HttpClient(clientHandler);
    }
    
    [HttpGet("ReaderArticles")]
    public async Task<ActionResult> Get()
    {
      System.Console.WriteLine("ReaderArticles");

      List<ViewTopicModel> Topics = null;
      bool TopicsWereReceived = false;

      List<ArticleViewModel> Articles = null;
      bool ArticlesWereReceived = false;

      var tResponse = await _http.GetAsync(apiUrl + "Topic/topics");
      if (tResponse.IsSuccessStatusCode)
      {
        var JsonResponse = await tResponse.Content.ReadAsStringAsync();
        var topicModel = JsonConvert.DeserializeObject<List<ViewTopicModel>>(JsonResponse);

        TempData["TopicVMs"] = JsonResponse;

        Topics = topicModel;
        TopicsWereReceived = true;

        System.Console.WriteLine(topicModel.Count);
      }

      var aResponse = await _http.GetAsync(apiUrl + "article/articles");

      if (aResponse.IsSuccessStatusCode)
      {
          var jsonResponse = await aResponse.Content.ReadAsStringAsync();
          var ObjOrderList = JsonConvert.DeserializeObject<List<ArticleViewModel>>(jsonResponse);
          var articles = ObjOrderList;

          Articles = articles;
          ArticlesWereReceived = true;
      }

      if (TopicsWereReceived && ArticlesWereReceived)
      {
        var ArticleTopicBundleVM = new ArticleTopicBundleViewModel(Articles, Topics);

        return await Task.FromResult(View("ReaderMain", ArticleTopicBundleVM));
      }

      return View("error");
    }

    [HttpGet("signup")]
    public ActionResult SignUp()
    {
      return View("ReaderSignup");
    }

// THIS IS THE SIGNUP ACTION //
    [HttpPost("confirm")]
    public IActionResult Confirm(ReaderViewModel model)
    {
      _http.BaseAddress= new Uri(apiUrl+"Reader/CreateReader");
      var postTask = _http.PostAsJsonAsync<ReaderViewModel>("CreateReader",model);
      postTask.Wait();
      var result = postTask.Result;

      ViewBag.message = result.StatusCode;
      if(result.IsSuccessStatusCode)
      {
          return RedirectToAction("get");
      }
      ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
      return View("signup");
    }

// EOF THE SIGNUP ACTION //

    [HttpGet]
    public IActionResult ReaederLogIn()
    {
      return View("ReaderLogin");
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(ReaderViewModel reader)
    {
      _http.BaseAddress= new Uri(apiUrl+"Reader/ReaderLogin");
      var postTask = await _http.PostAsJsonAsync<ReaderViewModel>("ReaderLogin",reader);
      ViewBag.message = postTask.StatusCode;
      if(postTask.IsSuccessStatusCode)
      {
        return RedirectToAction("ReaderArticles"); // Looks like it will use the actual method name if it can't find the method with the desired path
      }
      ViewBag.type ="Reader";
      return View("ErrorLogin");
    }

    [HttpPost("GetReaderArticles")] //[HttpPost("GetReaderArticles/{topicOption}")] <- was giving issues
    public async Task<ActionResult> GetReaderArticles(ViewTopicModel model)
    {
      //System.Console.WriteLine("ViewTopicModel Name: " + model.Name);
      var topicOption = model.Name;

      var article = new List<ArticleViewModel>(){};
      var response = await _http.GetAsync(apiUrl + "Article/GetArticleByTopic/" + topicOption);

      if (response.IsSuccessStatusCode)
      {

          var jsonResponse = await response.Content.ReadAsStringAsync();
          var ArticleVMs = JsonConvert.DeserializeObject<List<ArticleViewModel>>(jsonResponse);

          var TopicVMs = JsonConvert.DeserializeObject<List<ViewTopicModel>>(TempData["TopicVMs"].ToString());

          TempData["TopicVMs"] = JsonConvert.SerializeObject(TopicVMs);

          var ArticleTopicBundleVM = new ArticleTopicBundleViewModel(ArticleVMs, TopicVMs);
          ArticleTopicBundleVM.ChosenTopicToFilterBy = topicOption;
          return await Task.FromResult(View("TopicSpecificArticles", ArticleTopicBundleVM));
      }
      return View("error");
    }
  }
}
