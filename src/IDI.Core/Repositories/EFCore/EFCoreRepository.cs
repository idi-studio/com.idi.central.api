using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IDI.Core.Authentication;
using IDI.Core.Domain;

namespace IDI.Core.Repositories.EFCore
{
    public class EFCoreRepository<TAggregateRoot> : Repository<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        private readonly IEFCoreRepositoryContext efContext;
        private readonly IQueryable<TAggregateRoot> source;
        private readonly ICurrentUser currentUser;

        public EFCoreRepository(IRepositoryContext context, ICurrentUser user) : base(context)
        {
            if (context is IEFCoreRepositoryContext)
            {
                this.efContext = context as IEFCoreRepositoryContext;
                this.source = efContext.Context.Set<TAggregateRoot>();
            }

            this.currentUser = user;
        }

        #region Protected Methods
        protected override IQueryable<TAggregateRoot> DoSource()
        {
            return source;
        }
        protected override void DoAdd(TAggregateRoot aggregateRoot)
        {
            if (currentUser != null && currentUser.IsAuthenticated)
            {
                aggregateRoot.CreatedBy = currentUser.Name;
                aggregateRoot.LastUpdatedBy = currentUser.Name;
            }

            efContext.RegisterNew(aggregateRoot);
        }
        protected override void DoRemove(TAggregateRoot aggregateRoot)
        {
            efContext.RegisterDeleted(aggregateRoot);
        }
        protected override void DoUpdate(TAggregateRoot aggregateRoot)
        {
            if (currentUser != null && currentUser.IsAuthenticated)
            {
                aggregateRoot.LastUpdatedAt = DateTime.Now;
                aggregateRoot.LastUpdatedBy = currentUser.Name;
            }

            efContext.RegisterModified(aggregateRoot);
        }
        protected override TAggregateRoot DoFind(Guid key)
        {
            return efContext.Context.Set<TAggregateRoot>().FirstOrDefault(e => e.Id == key);
        }
        protected override TAggregateRoot DoFind(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return efContext.Context.Set<TAggregateRoot>().FirstOrDefault(condition);
        }
        protected override bool DoExist(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return efContext.Context.Set<TAggregateRoot>().Any(condition);
        }
        protected override int DoCount(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return efContext.Context.Set<TAggregateRoot>().Count(condition);
        }
        protected override List<TAggregateRoot> DoGet()
        {
            return efContext.Context.Set<TAggregateRoot>().ToList();
        }
        protected override List<TAggregateRoot> DoGet(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return efContext.Context.Set<TAggregateRoot>().Where(condition).ToList();
        }
        #endregion
    }
}
