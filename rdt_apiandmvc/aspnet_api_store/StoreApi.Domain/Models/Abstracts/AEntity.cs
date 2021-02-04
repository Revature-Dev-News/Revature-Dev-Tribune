namespace StoreApi.Domain.Models.Abstracts
{
  public abstract class AEntity
  {
     public long EntityId { get; set; }
     protected AEntity(){}
  }

}
