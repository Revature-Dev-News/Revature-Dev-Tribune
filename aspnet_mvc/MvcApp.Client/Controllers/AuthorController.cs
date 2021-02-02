using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using MvcApp.Client.Models.Author;
using System.Collections.Generic;
using Newtonsoft.Json;
using MvcApp.Client.Models.Shared;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json.Serialization;

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

    [HttpGet("author_main")]
    public async Task<IActionResult> ViewAuthorHome()
    {
      var response = await _http.GetAsync(apiUrl + "Article/articles");

      var jsonResponse = await response.Content.ReadAsStringAsync();

      var ArticleVMs = JsonConvert.DeserializeObject<List<ArticleViewModel>>(jsonResponse);

      return await Task.FromResult(View("AuthorMain", ArticleVMs));
    }

    [HttpPost("delete_article/{id}")]
    public async Task<IActionResult> DeleteArticle(long id)
    {
      var ArticleToDelete = new ArticleViewModel();
      ArticleToDelete.EntityId = id;

      System.Console.WriteLine("Id of Article to Delete: " + id);

      _http.BaseAddress = new Uri(apiUrl + "Article/delete_article/" + id);
      var deleteTask = await _http.DeleteAsync(_http.BaseAddress);

      if(deleteTask.IsSuccessStatusCode)
      {
          return RedirectToAction("author_main");

      }
      ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

      return RedirectToAction("author_main");
    }


    [HttpGet("view_article")]
    public IActionResult ViewArticle()
    {
      return View("AuthorArticleViewer", "tempValue");
    }

    [HttpGet("show_article_creator")]
        public async Task<IActionResult> ShowArticleCreator()
        {
            // Get the topics
            var response = await _http.GetAsync(apiUrl + "Topic/topics");

            if (response.IsSuccessStatusCode)
            {
                var JsonResponse = await response.Content.ReadAsStringAsync();

                var TopicViewModels = JsonConvert.DeserializeObject<List<TopicViewModel>>(JsonResponse);

                List<SelectListItem> TopicSelectListItems = new List<SelectListItem>();

                foreach(var topic in TopicViewModels)
                {
                    TopicSelectListItems.Add(new SelectListItem(topic.Name, topic.Name));
                }

                var ArticleViewModel = new ArticleViewModel(TopicSelectListItems);

                //ViewBag.Topics = TopicSelectListItems;
                TempData["TopicVMs"] = SerializeTopicViewModels(TopicViewModels);

                return await Task.FromResult(View("ArticleCreator", ArticleViewModel));
            }
            return View("Error");
        }

        private string SerializeTopicViewModels(List<TopicViewModel> topicVMs)
        {
            return JsonConvert.SerializeObject(topicVMs);
        }

        private List<TopicViewModel> DeserializeTopicViewModels(object modelTempData)
        {
            return JsonConvert.DeserializeObject<List<TopicViewModel>>(modelTempData.ToString());
        }

        [HttpPost("create_article")]
        public async Task<IActionResult> CreateArticle(ArticleViewModel articleVM)
        {
            // add the missing properties
            articleVM.isPublished = false;
            articleVM.ImagePath = "";
            //articleVM.PublishedDate = null;
            articleVM.EditedDate = DateTime.Now;
            articleVM.Author = new AuthorViewModel(3, "Joshwin Greene", "jg@aol.com", "12345");
            
            System.Console.WriteLine("Topic: " + articleVM.Topic);
            //System.Console.WriteLine("Topic Entity Id" + articleVM.Topic.EntityId);
            System.Console.WriteLine("Topic Name: " + articleVM.ChosenTopic);

            var TopicVMs = DeserializeTopicViewModels(TempData["TopicVMs"]);

            var ChosenTopicObject = TopicVMs.Find(t => t.Name == articleVM.ChosenTopic);

            System.Console.WriteLine("CreateArticle - Matched Topic Name: " + ChosenTopicObject.Name);

            articleVM.Topic = ChosenTopicObject;

            foreach(var topic in TopicVMs)
            {
              if (topic.Name == ChosenTopicObject.Name)
              {
                articleVM.AvailableTopics.Add(new SelectListItem(topic.Name, topic.Name, true));
              }
              else
              {
                articleVM.AvailableTopics.Add(new SelectListItem(topic.Name, topic.Name, false));  
              }
            }
            
            // prepare and make request
            _http.BaseAddress = new Uri(apiUrl + "Article/create_article");
            var postTask = await _http.PostAsJsonAsync<ArticleViewModel>("create_article", articleVM);
          
            var articleStr = await postTask.Content.ReadAsStringAsync();
            System.Console.WriteLine("Length of response: " + articleStr.Length);
            System.Console.WriteLine(articleStr);
            var articleObj = JsonConvert.DeserializeObject<ArticleViewModel>(articleStr);
            System.Console.WriteLine("Article Title: " + articleObj.Title);
            //System.Console.WriteLine("Article Topic Name: " + articleObj.Topic.Name);

            articleObj.ChosenTopic = articleVM.ChosenTopic;
            articleObj.AvailableTopics = articleVM.AvailableTopics;

            System.Console.WriteLine("Article Available Topics Count: " + articleObj.AvailableTopics.Count);

            if(postTask.IsSuccessStatusCode)
            {
                System.Console.WriteLine("Success");
                
                TempData["ArticleVM"] = JsonConvert.SerializeObject(articleObj);
                TempData["TopicVMs"] = SerializeTopicViewModels(TopicVMs);
                //return Content("Success");
                return View("ArticleEditor", articleObj);

            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View("show_article_creator");
        }

        [HttpPost("edit_article")]
        public IActionResult EditArticle(ArticleViewModel articleVM)
        {
            var SavedArticleVM = JsonConvert.DeserializeObject<ArticleViewModel>(TempData["ArticleVM"].ToString());

            SavedArticleVM.Title = articleVM.Title;
            SavedArticleVM.ChosenTopic = articleVM.ChosenTopic;
            SavedArticleVM.Body = articleVM.Body;
            SavedArticleVM.EditedDate = DateTime.Now;
            
            //System.Console.WriteLine("Saved Title: " + SavedArticleVM.Title);
            //System.Console.WriteLine("Topic Entity Id" + articleVM.Topic.EntityId);
            //System.Console.WriteLine("Saved Chosen Topic: " + SavedArticleVM.ChosenTopic);
            //System.Console.WriteLine("Saved Chosen Body: " + SavedArticleVM.Body);

            var TopicVMs = DeserializeTopicViewModels(TempData["TopicVMs"]);

            var ChosenTopicObject = TopicVMs.Find(t => t.Name == articleVM.ChosenTopic);

            System.Console.WriteLine("EditArticle - Matched Topic Name: " + ChosenTopicObject.Name);

            SavedArticleVM.Topic = ChosenTopicObject;

            foreach(var topic in TopicVMs)
            {
              if (topic.Name == ChosenTopicObject.Name)
              {
                SavedArticleVM.AvailableTopics.Add(new SelectListItem(topic.Name, topic.Name, true));
              }
              else
              {
                SavedArticleVM.AvailableTopics.Add(new SelectListItem(topic.Name, topic.Name, false));  
              }
            }
            
            // prepare and make request
            _http.BaseAddress = new Uri(apiUrl + "Article/update_article");
            var putTask = _http.PutAsJsonAsync<ArticleViewModel>("update_article", SavedArticleVM);
            putTask.Wait();

            var result = putTask.Result;
            if(result.IsSuccessStatusCode)
            {
                System.Console.WriteLine("Success");
                TempData["ArticleVM"] = JsonConvert.SerializeObject(SavedArticleVM);
                TempData["TopicVMs"] = SerializeTopicViewModels(TopicVMs);
                //return Content("Success");
                return View("ArticleEditor", SavedArticleVM);

            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View("ArticleEditor", articleVM);
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
