using System;
using MvcApp.Client.Models.Author;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcApp.Client.Models.Shared

{
    public class ArticleViewModel
    {
        public long EntityId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public AuthorViewModel Author { get; set; }
        public string ImagePath { get; set; }
        public TopicViewModel Topic { get; set; }
        public bool isPublished { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime EditedDate { get; set; }

        public List<SelectListItem> AvailableTopics { get; set; }

        public string ChosenTopic { get; set; }

        public ArticleViewModel()
        {
            AvailableTopics = new List<SelectListItem>();
        }

        public ArticleViewModel(List<SelectListItem> topicSelectListItems)
        {
            AvailableTopics = topicSelectListItems;
        }

    }
}
