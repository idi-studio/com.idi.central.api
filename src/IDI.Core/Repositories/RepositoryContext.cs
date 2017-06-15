using System;
using System.Collections.Concurrent;
using IDI.Core.Domain;

namespace IDI.Core.Repositories
{
    public abstract class RepositoryContext : IRepositoryContext
    {
        #region Private Fields
        private readonly Guid id = Guid.NewGuid();
        private readonly ConcurrentDictionary<object, byte> newCollection = new ConcurrentDictionary<object, byte>();
        private ConcurrentDictionary<object, byte> modifiedCollection = new ConcurrentDictionary<object, byte>();
        private ConcurrentDictionary<object, byte> deletedCollection = new ConcurrentDictionary<object, byte>();
        private volatile bool committed = true;
        #endregion

        #region Protected Properties
        protected ConcurrentDictionary<object, byte> NewCollection
        {
            get { return newCollection; }
        }
        protected ConcurrentDictionary<object, byte> ModifiedCollection
        {
            get { return modifiedCollection; }
        }
        protected ConcurrentDictionary<object, byte> DeletedCollection
        {
            get { return deletedCollection; }
        }
        #endregion

        #region Protected Methods
        protected void ClearRegistrations()
        {
            newCollection.Clear();
            modifiedCollection.Clear();
            deletedCollection.Clear();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ClearRegistrations();
            }
        }
        #endregion

        #region IUnitOfWork Members
        public virtual bool Committed
        {
            get { return committed; }
            protected set { committed = value; }
        }
        public abstract void Commit();
        public abstract void Rollback();
        #endregion

        #region IRepositoryContext Members
        public Guid Id
        {
            get { return id; }
        }

        public virtual void RegisterNew<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot
        {
            newCollection.AddOrUpdate(aggregateRoot, byte.MinValue, (o, b) => byte.MinValue);
            Committed = false;
        }

        public virtual void RegisterModified<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot
        {
            if (deletedCollection.ContainsKey(aggregateRoot))
                throw new InvalidOperationException("The object cannot be registered as a modified object since it was marked as deleted.");
            if (!modifiedCollection.ContainsKey(aggregateRoot) && !(newCollection.ContainsKey(aggregateRoot)))
                modifiedCollection.AddOrUpdate(aggregateRoot, byte.MinValue, (o, b) => byte.MinValue);

            Committed = false;
        }

        public virtual void RegisterDeleted<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot
        {
            var @byte = byte.MinValue;
            if (newCollection.ContainsKey(aggregateRoot))
            {
                newCollection.TryRemove(aggregateRoot, out @byte);
                return;
            }
            var removedFromModified = modifiedCollection.TryRemove(aggregateRoot, out @byte);
            var addedToDeleted = false;
            if (!deletedCollection.ContainsKey(aggregateRoot))
            {
                deletedCollection.AddOrUpdate(aggregateRoot, byte.MinValue, (o, b) => byte.MinValue);
                addedToDeleted = true;
            }
            committed = !(removedFromModified || addedToDeleted);
        }

        #endregion

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~RepositoryContext()
        {
            this.Dispose(false);
        }
    }
}
