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
        //protected override TAggregateRoot DoFind(Guid key, params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths)
        //{
        //    var query = BuidIncludableQuery(navigationPropertyPaths);

        //    return query.FirstOrDefault(e => e.Id == key);
        //}
        protected override TAggregateRoot DoFind(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return efContext.Context.Set<TAggregateRoot>().FirstOrDefault(condition);
        }
        //protected override TAggregateRoot DoFind(Expression<Func<TAggregateRoot, bool>> condition, params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths)
        //{
        //    return BuidIncludableQuery(navigationPropertyPaths).FirstOrDefault(condition);
        //}
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
        //protected override List<TAggregateRoot> DoGet(params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths)
        //{
        //    return BuidIncludableQuery(navigationPropertyPaths).ToList();
        //}
        protected override List<TAggregateRoot> DoGet(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return efContext.Context.Set<TAggregateRoot>().Where(condition).ToList();
        }
        //protected override List<TAggregateRoot> DoGet(Expression<Func<TAggregateRoot, bool>> condition, params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths)
        //{
        //    return BuidIncludableQuery(navigationPropertyPaths).Where(condition).ToList();
        //}
        //protected override QueryableContext<TAggregateRoot> DoQuery()
        //{
        //    var queryable = efContext.Context.Set<TAggregateRoot>();

        //    return new QueryableContext<TAggregateRoot>(queryable);
        //}
        //protected override QueryableContext<TAggregateRoot> DoQuery(Expression<Func<TAggregateRoot, bool>> condition)
        //{
        //    var queryable = efContext.Context.Set<TAggregateRoot>().Where(condition);

        //    return new QueryableContext<TAggregateRoot>(queryable);
        //}
        //protected override QueryableContext<TAggregateRoot> DoQuery(Expression<Func<TAggregateRoot, bool>> condition, params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths)
        //{
        //    var queryable = BuidIncludableQuery(navigationPropertyPaths).Where(condition);

        //    return new QueryableContext<TAggregateRoot>(queryable);
        //}
        #endregion

        //private IIncludableQueryable<TAggregateRoot, dynamic> BuidIncludableQuery(params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths)
        //{
        //    var dbset = efContext.Context.Set<TAggregateRoot>();

        //    IIncludableQueryable<TAggregateRoot, dynamic> query = null;

        //    foreach (Expression<Func<TAggregateRoot, dynamic>> navigationPropertyPath in navigationPropertyPaths)
        //    {
        //        if (query == null)
        //            query = dbset.Include(navigationPropertyPath);
        //        else
        //            query = query.Include(navigationPropertyPath);
        //    }

        //    return query;
        //}
    }
}
