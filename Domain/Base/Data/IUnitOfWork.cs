namespace Domain.Base.Data
{
    public interface IUnitOfWork
    {
         Task<bool> Commit();
    }
}