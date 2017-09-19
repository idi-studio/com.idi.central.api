using System.Linq;
using IDI.Core.Infrastructure;
using IDI.Core.Repositories;
using IDI.Core.Tests.TestUtils;
using IDI.Core.Tests.TestUtils.AggregateRoots;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Core.Tests.Repositories.EFCore
{
    [TestClass]
    public class EFCoreRepositoryUnitTests_Get : EFCoreRepositoryUnitTest
    {
        [TestMethod]
        public void TestEFCoreRepository_Get_All()
        {
            int blogCount = 5, postCount = 5;

            DbHelper.ExecuteNonQuery(Contants.DbOperations.BatchInsertBlog.BuildCmdTexts(blogCount, postCount));

            Assert.AreEqual(expected: blogCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
            Assert.AreEqual(expected: blogCount * postCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));

            var repository = Runtime.GetService<IRepository<Blog>>();

            var blogs = repository.Get();

            Assert.IsNotNull(blogs);
            Assert.AreEqual(blogCount, blogs.Count);
            Assert.AreEqual(0, blogs.SelectMany(b => b.Posts).Count());
        }

        [TestMethod]
        public void TestEFCoreRepository_Get_All_InculdeNavigationProperty()
        {
            int blogCount = 5, postCount = 5;

            DbHelper.ExecuteNonQuery(Contants.DbOperations.BatchInsertBlog.BuildCmdTexts(blogCount, postCount));

            Assert.AreEqual(expected: blogCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
            Assert.AreEqual(expected: blogCount * postCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));

            var repository = Runtime.GetService<IRepository<Blog>>();

            var blogs = repository.Include(b => b.Posts).Get();

            Assert.IsNotNull(blogs);
            Assert.AreEqual(blogCount, blogs.Count);
            Assert.AreEqual(blogCount * postCount, blogs.SelectMany(b => b.Posts).Count());
        }

        [TestMethod]
        public void TestEFCoreRepository_Get_ByCondition()
        {
            int blogCount = 5, postCount = 5;

            DbHelper.ExecuteNonQuery(Contants.DbOperations.BatchInsertBlog.BuildCmdTexts(blogCount, postCount));

            Assert.AreEqual(expected: blogCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
            Assert.AreEqual(expected: blogCount * postCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));

            var repository = Runtime.GetService<IRepository<Blog>>();

            var blogs = repository.Get(b => b.Url == "http://www.cnblogs.com/?id=1");

            Assert.IsNotNull(blogs);
            Assert.AreEqual(1, blogs.Count);
            Assert.AreEqual(0, blogs.SelectMany(b => b.Posts).Count());
        }

        [TestMethod]
        public void TestEFCoreRepository_Get_ByCondition_InculdeNavigationProperty()
        {
            int blogCount = 5, postCount = 5;

            DbHelper.ExecuteNonQuery(Contants.DbOperations.BatchInsertBlog.BuildCmdTexts(blogCount, postCount));

            Assert.AreEqual(expected: blogCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
            Assert.AreEqual(expected: blogCount * postCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));

            var repository = Runtime.GetService<IRepository<Blog>>();

            var blogs = repository.Include(e => e.Posts).Get(b => b.Url == "http://www.cnblogs.com/?id=1");

            Assert.IsNotNull(blogs);
            Assert.AreEqual(1, blogs.Count);
            Assert.AreEqual(postCount, blogs.SelectMany(b => b.Posts).Count());
        }

        //[TestMethod]
        //public void TestEFCoreRepository_Get_Paged()
        //{
        //    int blogCount = 20, postCount = 5, pageNumber = 1, pageSize = 5;

        //    DbHelper.ExecuteNonQuery(Contants.DbOperations.BatchInsertBlog.BuildCmdTexts(blogCount, postCount));

        //    Assert.AreEqual(expected: blogCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
        //    Assert.AreEqual(expected: blogCount * postCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));

        //    var repository = ServiceLocator.GetService<IRepository<Blog>>();

        //    var pagedResult = repository.Get(b => b.Url, SortDirection.Ascending, pageNumber, pageSize);

        //    Assert.IsNotNull(pagedResult);
        //    Assert.AreEqual(pageNumber, pagedResult.PageNumber);
        //    Assert.AreEqual(pageSize, pagedResult.PageSize);
        //    Assert.AreEqual(pageSize * 1, pagedResult.Count);
        //    Assert.AreEqual(blogCount, pagedResult.TotalRecords);
        //    Assert.AreEqual(4, pagedResult.TotalPages);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=1", pagedResult.Data.ElementAt(0).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=10", pagedResult.Data.ElementAt(1).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=11", pagedResult.Data.ElementAt(2).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=12", pagedResult.Data.ElementAt(3).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=13", pagedResult.Data.ElementAt(4).Url);
        //    Assert.AreEqual(0, pagedResult.SelectMany(b => b.Posts).Count());

        //    pagedResult = repository.Get(b => b.Url, SortDirection.Descending, pageNumber, pageSize);

        //    Assert.IsNotNull(pagedResult);
        //    Assert.AreEqual(pageNumber, pagedResult.PageNumber);
        //    Assert.AreEqual(pageSize, pagedResult.PageSize);
        //    Assert.AreEqual(pageSize * 1, pagedResult.Count);
        //    Assert.AreEqual(blogCount, pagedResult.TotalRecords);
        //    Assert.AreEqual(4, pagedResult.TotalPages);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=9", pagedResult.Data.ElementAt(0).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=8", pagedResult.Data.ElementAt(1).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=7", pagedResult.Data.ElementAt(2).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=6", pagedResult.Data.ElementAt(3).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=5", pagedResult.Data.ElementAt(4).Url);
        //    Assert.AreEqual(0, pagedResult.SelectMany(b => b.Posts).Count());
        //}

        //[TestMethod]
        //public void TestEFCoreRepository_Get_Paged_ByCondtion()
        //{
        //    int blogCount = 20, postCount = 5, pageNumber = 1, pageSize = 5;

        //    DbHelper.ExecuteNonQuery(Contants.DbOperations.BatchInsertBlog.BuildCmdTexts(blogCount, postCount));

        //    Assert.AreEqual(expected: blogCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
        //    Assert.AreEqual(expected: blogCount * postCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));

        //    var repository = ServiceLocator.GetService<IRepository<Blog>>();

        //    var pagedResult = repository.Get(b => b.Url.Contains("2"), b => b.Url, SortDirection.Ascending, pageNumber, pageSize);

        //    Assert.IsNotNull(pagedResult);
        //    Assert.AreEqual(pageNumber, pagedResult.PageNumber);
        //    Assert.AreEqual(pageSize, pagedResult.PageSize);
        //    Assert.AreEqual(3, pagedResult.Count);
        //    Assert.AreEqual(3, pagedResult.TotalRecords);
        //    Assert.AreEqual(1, pagedResult.TotalPages);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=12", pagedResult.Data.ElementAt(0).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=2", pagedResult.Data.ElementAt(1).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=20", pagedResult.Data.ElementAt(2).Url);
        //    Assert.AreEqual(0, pagedResult.SelectMany(b => b.Posts).Count());

        //    pagedResult = repository.Get(b => b.Url.Contains("2"), b => b.Url, SortDirection.Descending, pageNumber, pageSize);

        //    Assert.IsNotNull(pagedResult);
        //    Assert.AreEqual(pageNumber, pagedResult.PageNumber);
        //    Assert.AreEqual(pageSize, pagedResult.PageSize);
        //    Assert.AreEqual(3, pagedResult.Count);
        //    Assert.AreEqual(3, pagedResult.TotalRecords);
        //    Assert.AreEqual(1, pagedResult.TotalPages);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=20", pagedResult.Data.ElementAt(0).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=2", pagedResult.Data.ElementAt(1).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=12", pagedResult.Data.ElementAt(2).Url);
        //    Assert.AreEqual(0, pagedResult.SelectMany(b => b.Posts).Count());
        //}

        //[TestMethod]
        //public void TestEFCoreRepository_Get_Paged_InculdeNavigationProperty()
        //{
        //    int blogCount = 20, postCount = 5, pageNumber = 1, pageSize = 5;

        //    DbHelper.ExecuteNonQuery(Contants.DbOperations.BatchInsertBlog.BuildCmdTexts(blogCount, postCount));

        //    Assert.AreEqual(expected: blogCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
        //    Assert.AreEqual(expected: blogCount * postCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));

        //    var repository = ServiceLocator.GetService<IRepository<Blog>>();

        //    var pagedResult = repository.Get(b => b.Url, SortDirection.Ascending, pageNumber, pageSize, b => b.Posts);

        //    Assert.IsNotNull(pagedResult);
        //    Assert.AreEqual(pageNumber, pagedResult.PageNumber);
        //    Assert.AreEqual(pageSize, pagedResult.PageSize);
        //    Assert.AreEqual(pageSize * 1, pagedResult.Count);
        //    Assert.AreEqual(blogCount, pagedResult.TotalRecords);
        //    Assert.AreEqual(4, pagedResult.TotalPages);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=1", pagedResult.Data.ElementAt(0).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=10", pagedResult.Data.ElementAt(1).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=11", pagedResult.Data.ElementAt(2).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=12", pagedResult.Data.ElementAt(3).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=13", pagedResult.Data.ElementAt(4).Url);
        //    Assert.AreEqual(1 * pageSize * postCount, pagedResult.SelectMany(b => b.Posts).Count());

        //    pagedResult = repository.Get(b => b.Url, SortDirection.Descending, pageNumber, pageSize, b => b.Posts);

        //    Assert.IsNotNull(pagedResult);
        //    Assert.AreEqual(pageNumber, pagedResult.PageNumber);
        //    Assert.AreEqual(pageSize, pagedResult.PageSize);
        //    Assert.AreEqual(pageSize * 1, pagedResult.Count);
        //    Assert.AreEqual(blogCount, pagedResult.TotalRecords);
        //    Assert.AreEqual(4, pagedResult.TotalPages);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=9", pagedResult.Data.ElementAt(0).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=8", pagedResult.Data.ElementAt(1).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=7", pagedResult.Data.ElementAt(2).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=6", pagedResult.Data.ElementAt(3).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=5", pagedResult.Data.ElementAt(4).Url);
        //    Assert.AreEqual(1 * pageSize * postCount, pagedResult.SelectMany(b => b.Posts).Count());
        //}

        //[TestMethod]
        //public void TestEFCoreRepository_Get_Paged_ByCondtion_InculdeNavigationProperty()
        //{
        //    int blogCount = 20, postCount = 5, pageNumber = 1, pageSize = 5;

        //    DbHelper.ExecuteNonQuery(Contants.DbOperations.BatchInsertBlog.BuildCmdTexts(blogCount, postCount));

        //    Assert.AreEqual(expected: blogCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
        //    Assert.AreEqual(expected: blogCount * postCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));

        //    var repository = ServiceLocator.GetService<IRepository<Blog>>();

        //    var pagedResult = repository.Get(b => b.Url.Contains("2"), b => b.Url, SortDirection.Ascending, pageNumber, pageSize, b => b.Posts);

        //    Assert.IsNotNull(pagedResult);
        //    Assert.AreEqual(pageNumber, pagedResult.PageNumber);
        //    Assert.AreEqual(pageSize, pagedResult.PageSize);
        //    Assert.AreEqual(3, pagedResult.Count);
        //    Assert.AreEqual(3, pagedResult.TotalRecords);
        //    Assert.AreEqual(1, pagedResult.TotalPages);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=12", pagedResult.Data.ElementAt(0).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=2", pagedResult.Data.ElementAt(1).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=20", pagedResult.Data.ElementAt(2).Url);
        //    Assert.AreEqual(3 * postCount, pagedResult.SelectMany(b => b.Posts).Count());

        //    pagedResult = repository.Get(b => b.Url.Contains("2"), b => b.Url, SortDirection.Descending, pageNumber, pageSize, b => b.Posts);

        //    Assert.IsNotNull(pagedResult);
        //    Assert.AreEqual(pageNumber, pagedResult.PageNumber);
        //    Assert.AreEqual(pageSize, pagedResult.PageSize);
        //    Assert.AreEqual(3, pagedResult.Count);
        //    Assert.AreEqual(3, pagedResult.TotalRecords);
        //    Assert.AreEqual(1, pagedResult.TotalPages);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=20", pagedResult.Data.ElementAt(0).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=2", pagedResult.Data.ElementAt(1).Url);
        //    Assert.AreEqual("http://www.cnblogs.com/?id=12", pagedResult.Data.ElementAt(2).Url);
        //    Assert.AreEqual(3 * postCount, pagedResult.SelectMany(b => b.Posts).Count());
        //}
    }
}
