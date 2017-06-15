namespace IDI.Core.Repositories
{
    public interface IUnitOfWork
    {
        bool Committed { get; }

        void Commit();

        void Rollback();
    }
}
