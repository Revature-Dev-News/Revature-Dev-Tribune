namespace MvcApp.Client.Models.Author
{
    public class AuthorViewModel
    {
        public long EntityId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public AuthorViewModel() {}

        public AuthorViewModel(long entityId, string name, string email, string password) 
        {
            EntityId = entityId;
            Name = name;
            Email = email;
            Password = password;
        }
    }
}