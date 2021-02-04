using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoreApi.Domain.Models;


namespace StoreApi.Service.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TopicController : ControllerBase
  {
    private readonly RDTRepo _repo;

    public TopicController(RDTRepo repository)
    {
      _repo = repository;
    }

    [HttpGet("topics")]
    public async Task<IActionResult> GetTopics()
    {
      var Topics = _repo.GetTopics();
      System.Console.WriteLine("TEST");
      System.Console.WriteLine(Topics);
      // var Topics = _ctx.Topics;

      return await Task.FromResult(Ok(Topics));
    }
  }
}















