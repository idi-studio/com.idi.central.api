//using System;
//using System.Collections.Generic;
//using System.Linq;
//using IDI.Core.Common;
//using IDI.Core.Domain;

//namespace IDI.Core.Repositories
//{
//    public class QueryableContext<TAggregateRoot> where TAggregateRoot : AggregateRoot
//    {
//        private IQueryable<TAggregateRoot> queryable;
//        private List<SortPredicate<TAggregateRoot>> sortPredicates;
//        private IOrderedQueryable<TAggregateRoot> orderedQueryable;

//        public QueryableContext(IQueryable<TAggregateRoot> queryable)
//        {
//            this.queryable = queryable;
//            this.sortPredicates = new List<SortPredicate<TAggregateRoot>>();
//        }

//        public QueryableContext<TAggregateRoot> Sort(params SortPredicate<TAggregateRoot>[] sortPredicates)
//        {
//            this.sortPredicates = sortPredicates.ToList();
//            this.orderedQueryable = this.queryable.SortBy(this.sortPredicates);

//            return this;
//        }

//        public Page<TAggregateRoot> Page(int pageNumber, int pageSize)
//        {
//            if (pageNumber <= 0)
//                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "The pageNumber should be larger than zero.");

//            if (pageSize <= 0)
//                throw new ArgumentOutOfRangeException("pageSize", pageSize, "The pageSize should be larger than zero.");

//            int skip = (pageNumber - 1) * pageSize;
//            int take = pageSize;
//            int total = this.queryable.Count();

//            List<TAggregateRoot> items;

//            if (sortPredicates.Count > 0)
//            {
//                items = this.orderedQueryable.Skip(skip).Take(take).SortBy(sortPredicates).ToList();
//            }
//            else
//            {
//                items = this.orderedQueryable.Skip(skip).Take(take).ToList();
//            }

//            return new Page<TAggregateRoot>(totalRecords: total, totalPages: (total + pageSize - 1) / pageSize, pageSize: pageSize, pageNumber: pageNumber, items: items);
//        }
//    }
//}
