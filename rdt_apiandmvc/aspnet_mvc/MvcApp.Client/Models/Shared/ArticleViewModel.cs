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

        public string[] TopicImageUrls { get; set; } // This is temporary.

        public ArticleViewModel()
        {
            AvailableTopics = new List<SelectListItem>();
            TopicImageUrls = GetTopicImageUrls();
        }

        public ArticleViewModel(List<SelectListItem> topicSelectListItems)
        {
            AvailableTopics = topicSelectListItems;
            TopicImageUrls = GetTopicImageUrls();
        }

        private string[] GetTopicImageUrls()
        {
            return new [] {
                "https://www.ionos.com/digitalguide/fileadmin/DigitalGuide/Teaser/operating-system-t.jpg",
                "https://www.theburnin.com/wp-content/uploads/2019/12/VR-Industry-Growth.jpg",
                "https://miro.medium.com/max/798/1*57__j14aNQfmPZyFoS1yRg.png",
                "https://www.westagilelabs.com/blog/wp-content/uploads/2019/12/software-762486_1920.jpg",
                "https://miro.medium.com/max/800/1*cDO5wuA0NdevLb45zHRvog.jpeg",
                "https://media.geeksforgeeks.org/wp-content/cdn-uploads/machineLearning3.png",
                "https://static3.seekingalpha.com/uploads/2018/7/5/saupload_31811136-v2_xlarge.jpg",
                "https://www.simplilearn.com/ice9/free_resources_article_thumb/Best-Programming-Languages-to-Start-Learning-Today.jpg",
                "https://volument.com/blog/img/hn-dirt-big.png",
                "https://www.goodcore.co.uk/blog/wp-content/uploads/2019/09/what-is-a-database-management-system.png",
                "https://cdn-res.keymedia.com/cms/images/ca/155/0319_637171637373959129.jpg"
            };
        }

    }
}
