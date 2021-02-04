using StoreApi.Domain.Models.Abstracts;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StoreApi.Domain.Models
{
  public class Author : AUser
  {
    public string Name { get; set; }
    //[JsonIgnore]
    //public List<Article> Articles { get; set; }
  }
}
