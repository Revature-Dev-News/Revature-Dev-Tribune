using StoreApi.Domain.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;



namespace StoreApi.Service
{
    public class RDTRepo
    {
        private RDTContext _ctx;
        //Constructor

        public RDTRepo(RDTContext context)
        {
            _ctx = context;
        }

        public IEnumerable<Topic> GetTopics()
        {
            return _ctx.Topics;
        }

        public IEnumerable<Article> GetArticlesByGivenTitle(string title)
        {
            return _ctx.Articles.Where(a => a.Title == title);
        }

        public IEnumerable<Article> GetArticles()
        {
            return _ctx.Articles.Include(a=>a.Topic).Include(a => a.Author);
        }

        public IEnumerable<Article> GetArticlesByGivenEmail()
        {
            return null;
        }

        public void CreateArticle(Article article)
        {
            _ctx.Articles.Add(article);
            _ctx.SaveChanges();
        }

        public void UpdateArticle(Article article)
        {
          _ctx.Articles.Update(article);
          _ctx.SaveChanges();
        }

        public void DeleteArticle(Article article)
        {
          _ctx.Articles.Remove(article);
          _ctx.SaveChanges();
        }

        // reader endpoints
        public void CreateReader(Reader reader)
        {
          _ctx.Readers.Add(reader);
          _ctx.SaveChanges();
        }

        public IEnumerable<Article> GetArticlesByTopic(Topic topic)
        {
          return _ctx.Articles.Where(a => a.Topic.Name == topic.Name).Include(a=>a.Topic).Include(a => a.Author);
        }




















    }
}
