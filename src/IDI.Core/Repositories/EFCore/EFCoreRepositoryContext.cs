using Microsoft.EntityFrameworkCore;

namespace IDI.Core.Repositories.EFCore
{
    public class EFCoreRepositoryContext : RepositoryContext, IEFCoreRepositoryContext
    {
        private readonly DbContext efContext;
        private readonly object sync = new object();

        public EFCoreRepositoryContext(DbContext efContext)
        {
            this.efContext = efContext;

            //var provider = this.efContext.GetInfrastructure();
            //var loggerFactory = provider.GetService<ILoggerFactory>();
            //loggerFactory.AddProvider(new FileLoggerProvider());

            efContext.Database.EnsureCreated();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                efContext.Dispose();
            }
            base.Dispose(disposing);
        }

        #region IEntityFrameworkRepositoryContext Members
        public DbContext Context
        {
            get { return this.efContext; }
        }
        #endregion

        #region IRepositoryContext Members
        public override void RegisterNew<TAggregateRoot>(TAggregateRoot aggregateRoot)
        {
            efContext.Set<TAggregateRoot>().Add(aggregateRoot);
            Committed = false;
        }

        public override void RegisterModified<TAggregateRoot>(TAggregateRoot aggregateRoot)
        {
            efContext.Set<TAggregateRoot>().Update(aggregateRoot);
            Committed = false;
        }

        public override void RegisterDeleted<TAggregateRoot>(TAggregateRoot aggregateRoot)
        {
            efContext.Set<TAggregateRoot>().Remove(aggregateRoot);
            Committed = false;
        }
        #endregion

        #region IUnitOfWork Members
        public override void Commit()
        {
            if (!Committed)
            {
                lock (sync)
                {
                    efContext.SaveChanges();
                }
                Committed = true;
            }
        }
        public override void Rollback()
        {
            Committed = false;
        }
        #endregion
    }
}
