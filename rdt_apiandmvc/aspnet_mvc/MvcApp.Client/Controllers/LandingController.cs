using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcApp.Client.Models.Author;
using MvcApp.Client.Models.Shared;
using Newtonsoft.Json;

namespace MvcApp.Client.Controllers
{
  [Route("[controller]")]

  public class LandingController :Controller
  {
    private string apiUrl = "https://rdtproject2api.azurewebsites.net/";
    private HttpClient _http;


    public LandingController(){
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        _http = new HttpClient(clientHandler);
    }
    [HttpGet("/")]
    public async Task<IActionResult> Home()
    {
      var topics = new List<TopicViewModel>{};
      var article = new List<ArticleViewModel>{};
      var response = await _http.GetAsync(apiUrl + "Article/articles");
      if(response.IsSuccessStatusCode)
      {
        article = JsonConvert.DeserializeObject<List<ArticleViewModel>>(await response.Content.ReadAsStringAsync());
        return await Task.FromResult(View("home",article));
      }
      return Content("NO DATA");
    }
  }
}
