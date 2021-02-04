using StoreApi.Domain.Models;
using StoreApi.Domain.Models.Abstracts;

namespace StoreApi.Domain.Models
{
    public class ReaderTopic : AEntity
    {
        public Reader Reader { get; set; }

        public Topic Topic { get; set; }

        public ReaderTopic() {}

        public ReaderTopic(Reader reader, Topic topic)
        {
            Reader = reader;
            Topic = topic;
        }
    }
}