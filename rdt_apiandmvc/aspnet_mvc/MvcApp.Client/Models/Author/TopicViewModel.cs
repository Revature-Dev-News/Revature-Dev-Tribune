namespace MvcApp.Client.Models.Author
{
    public class TopicViewModel
    {
        public long EntityId { get; set; }
        public string Name { get; set; }

        public TopicViewModel() {}

        public TopicViewModel(long entityId, string name)
        {
            EntityId = entityId;
            Name = name;
        }
    }
}