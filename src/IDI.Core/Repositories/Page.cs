using System.Collections.Generic;

namespace IDI.Core.Repositories
{
    public class Page<T> : ICollection<T>
    {
        private IList<T> items;

        #region Public Properties
        public int TotalRecords { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public IEnumerable<T> Data
        {
            get { return items; }
        }
        #endregion

        public Page()
        {
            this.items = new List<T>();
        }

        public Page(int totalRecords, int totalPages, int pageSize, int pageNumber, IList<T> items)
        {
            this.TotalRecords = totalRecords;
            this.TotalPages = totalPages;
            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.items = items;
        }

        #region ICollection<T> Members

        public void Add(T item)
        {
            items.Add(item);
        }

        public void Clear()
        {
            items.Clear();
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return items.Remove(item);
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        #endregion
    }
}
