using System.Collections.Generic;
using StoreApi.Domain.Models.Abstracts;

namespace StoreApi.Domain.Models
{
    public class Topic : AEntity
    {
        public string Name { get; set; }
    }
}
