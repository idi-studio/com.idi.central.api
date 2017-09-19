namespace IDI.Core.Repositories
{
    public interface IUnitOfWork
    {
        bool Committed { get; }

        int Commit();

        void Rollback();
    }
}
