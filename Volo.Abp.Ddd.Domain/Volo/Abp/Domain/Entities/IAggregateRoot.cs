namespace Volo.Abp.Domain.Entities
{
    public interface IAggregateRoot : IEntity
    {
    }

    public interface IAggregateRoot<Tkey> : IEntity<Tkey>, IAggregateRoot
    {
    }
}