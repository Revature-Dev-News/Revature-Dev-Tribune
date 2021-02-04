using System.Collections.Generic;
using MvcApp.Client.Models.Author;
using MvcApp.Client.Models.Shared;

namespace MvcApp.Client.Models.Reader
{
    public class ArticleTopicBundleViewModel
    {
        public List<ArticleViewModel> Articles { get; set; }
        public List<ViewTopicModel> Topics { get; set; }
        public string ChosenTopicToFilterBy { get; set; }

        public ArticleTopicBundleViewModel (List<ArticleViewModel> articlesToBundle, List<ViewTopicModel> topicsToBundle)
        {
            Articles = articlesToBundle;
            Topics = topicsToBundle;
        }
    }
}