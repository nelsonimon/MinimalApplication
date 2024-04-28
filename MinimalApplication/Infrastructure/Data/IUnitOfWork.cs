namespace MinimalApplication.Infrastructure.Data
{
    public interface IUnitOfWork: IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
