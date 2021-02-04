using StoreApi.Domain.Models.Abstracts;
using System.Collections.Generic;

namespace StoreApi.Domain.Models
{
  public class Reader : AUser
  {
    public string Username { get; set; }
    // If we have time, we can use in the client and repo
    public List<ReaderTopic> ReaderTopics { get; set; } 
  }
}
