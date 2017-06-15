using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IDI.Core.Domain;

namespace IDI.Core.Repositories
{
    public abstract class Repository<TAggregateRoot> : IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        private readonly IRepositoryContext context;

        public Repository(IRepositoryContext context)
        {
            this.context = context;
        }

        #region IRepository<TAggregateRoot> Members
        public IRepositoryContext Context
        {
            get { return this.context; }
        }
        public void Add(TAggregateRoot aggregateRoot)
        {
            this.DoAdd(aggregateRoot);
        }
        public void Remove(TAggregateRoot aggregateRoot)
        {
            this.DoRemove(aggregateRoot);
        }
        public void Update(TAggregateRoot aggregateRoot)
        {
            this.DoUpdate(aggregateRoot);
        }
        public bool Exist(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return this.DoExist(condition);
        }
        public TAggregateRoot Find(Guid key)
        {
            return this.DoFind(key);
        }
        public TAggregateRoot Find(Guid key, params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths)
        {
            return this.DoFind(key, navigationPropertyPaths);
        }
        public TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return this.DoFind(condition);
        }
        public TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> condition, params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths)
        {
            return this.DoFind(condition, navigationPropertyPaths);
        }
        public List<TAggregateRoot> Get()
        {
            return this.DoGet();
        }
        public List<TAggregateRoot> Get(params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths)
        {
            return this.DoGet(navigationPropertyPaths);
        }
        public List<TAggregateRoot> Get(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return this.DoGet(condition);
        }
        public List<TAggregateRoot> Get(Expression<Func<TAggregateRoot, bool>> condition, params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths)
        {
            return this.DoGet(condition, navigationPropertyPaths);
        }
        public QueryableContext<TAggregateRoot> Query()
        {
            return this.DoQuery();
        }
        public QueryableContext<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return this.DoQuery(condition);
        }
        public QueryableContext<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>> condition, params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths)
        {
            return this.DoQuery(condition, navigationPropertyPaths);
        }
        #endregion

        #region Protected Methods
        protected abstract void DoAdd(TAggregateRoot aggregateRoot);
        protected abstract void DoRemove(TAggregateRoot aggregateRoot);
        protected abstract void DoUpdate(TAggregateRoot aggregateRoot);
        protected abstract bool DoExist(Expression<Func<TAggregateRoot, bool>> condition);
        protected abstract TAggregateRoot DoFind(Guid key);
        protected abstract TAggregateRoot DoFind(Guid key, params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths);
        protected abstract TAggregateRoot DoFind(Expression<Func<TAggregateRoot, bool>> condition);
        protected abstract TAggregateRoot DoFind(Expression<Func<TAggregateRoot, bool>> condition, params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths);
        protected abstract List<TAggregateRoot> DoGet();
        protected abstract List<TAggregateRoot> DoGet(params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths);
        protected abstract List<TAggregateRoot> DoGet(Expression<Func<TAggregateRoot, bool>> condition);
        protected abstract List<TAggregateRoot> DoGet(Expression<Func<TAggregateRoot, bool>> condition, params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths);
        protected abstract QueryableContext<TAggregateRoot> DoQuery();
        protected abstract QueryableContext<TAggregateRoot> DoQuery(Expression<Func<TAggregateRoot, bool>> condition);
        protected abstract QueryableContext<TAggregateRoot> DoQuery(Expression<Func<TAggregateRoot, bool>> condition, params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths);
        #endregion
    }
}
