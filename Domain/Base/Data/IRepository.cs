using Domain.Base.DomainObjects;

namespace Domain.Base.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
         IUnitOfWork UnitOfWork { get; }
    }
}