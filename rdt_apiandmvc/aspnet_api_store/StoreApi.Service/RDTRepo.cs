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
// WORK IN THIS!! all the articles that include the given string in the search box
// on the MVC ...dynamic typing
        public IEnumerable<Article> GetArticlesByGivenTitle(string title)
        {
            return _ctx.Articles.Where(a => a.Title == title);
        }

        public IEnumerable<Article> GetArticles()
        {
            return _ctx.Articles.Include(a=>a.Topic).Include(a => a.Author);
        }

        public IEnumerable<Article> GetArticlesByGivenEmail(string email)
        {
          //  Recieved an email and given the article that the AUTHOR wrote
            return _ctx.Articles.Where(a => a.Author.Email == email).Include(a => a.Author).Include(a => a.Topic);
        }

        public Article CreateArticle(Article article)
        {
            _ctx.Articles.Add(article);
            _ctx.SaveChanges();
            var rArticle = _ctx.Articles.OrderByDescending(a => a.EntityId)
                       .FirstOrDefault();
            return rArticle;
        }

        public void UpdateArticle(Article article)
        {
          _ctx.Update(article);
          _ctx.SaveChanges();
        }

        public void DeleteArticle(long id)
        {
          var ArticleToDelete = _ctx.Articles.FirstOrDefault<Article>(a => a.EntityId == id);

          if (ArticleToDelete == null)
          {
            return;
          }
          _ctx.Remove(ArticleToDelete);
          _ctx.SaveChanges();
        }

        // reader endpoints
        public void CreateReader(Reader reader)
        {
          _ctx.Readers.Add(reader);
          _ctx.SaveChanges();
        }

        public List<Article> GetArticlesByTopic(string topicName)
        {
          return _ctx.Articles.Where(a => a.Topic.Name == topicName).Include(a=>a.Topic).Include(a => a.Author).ToList();
        }

        public IEnumerable<Reader> GetReaders()
        {
          return _ctx.Readers;
        }

        public Author GetAuthorIfValidCredential(Author author)
        {
          var isEmailOk =_ctx.Authors.FirstOrDefault(a => a.Email == author.Email);
          if( isEmailOk == null){
            return null;
          }
          var isPasswordOk = _ctx.Authors.FirstOrDefault(ax =>ax.Password == author.Password && ax.Email == author.Email);
          return isPasswordOk!=null? isPasswordOk:null;
        }

        public bool CheckIfReaderExists(Reader reader)
        {
          var EmailExists = _ctx.Readers.FirstOrDefault(a => a.Email == reader.Email);
          var UserExists = _ctx.Readers.FirstOrDefault(a => a.Username == reader.Username);
          return EmailExists != null || UserExists != null? true : false;

        }

         public Reader GetReaderIfValidCredential(Reader reader)
        {

          var isEmailOk =_ctx.Readers.FirstOrDefault(a => a.Email == reader.Email);
          if( isEmailOk == null){
            return null;
          }
          var isPasswordOk = _ctx.Readers.FirstOrDefault(ax =>ax.Password == reader.Password && ax.Email == reader.Email);
          return isPasswordOk!=null? isPasswordOk:null;
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _ctx.Authors;
        }

// LIKE ARTICLE















    }
}
