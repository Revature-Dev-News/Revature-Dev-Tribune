using System;
using System.Text.Json.Serialization;
using StoreApi.Domain.Models.Abstracts;

namespace StoreApi.Domain.Models
{
    public class Article : AEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string ImagePath { get; set; }
        public bool IsPublished { get; set; }
        public Author Author { get; set; }
        public Topic Topic { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime EditedDate { get; set; }
    }
}
