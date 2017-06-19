using System.Linq;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using IDI.Core.Repositories;
using IDI.Core.Tests.Common;
using IDI.Core.Tests.Common.AggregateRoots;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Core.Tests.Repositories.EFCore
{
    [TestClass]
    public class EFCoreRepositoryUnitTests_Query : EFCoreRepositoryUnitTest
    {
        [TestMethod]
        public void TestEFCoreRepository_Query()
        {
            int blogCount = 20, postCount = 5, pageNumber = 1, pageSize = 5;

            DbHelper.ExecuteNonQuery(Contants.DbOperations.BatchInsertBlog.BuildCmdTexts(blogCount, postCount));

            Assert.AreEqual(expected: blogCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
            Assert.AreEqual(expected: blogCount * postCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));

            var repository = ServiceLocator.GetService<IRepository<Blog>>();

            var pagedResult = repository.Query().Sort(new AscBy<Blog>(b => b.Url)).Page(pageNumber: pageNumber, pageSize: pageSize);

            Assert.IsNotNull(pagedResult);
            Assert.AreEqual(pageNumber, pagedResult.PageNumber);
            Assert.AreEqual(pageSize, pagedResult.PageSize);
            Assert.AreEqual(pageSize * 1, pagedResult.Count);
            Assert.AreEqual(blogCount, pagedResult.TotalRecords);
            Assert.AreEqual(4, pagedResult.TotalPages);
            Assert.AreEqual("http://www.cnblogs.com/?id=1", pagedResult.Data.ElementAt(0).Url);
            Assert.AreEqual("http://www.cnblogs.com/?id=10", pagedResult.Data.ElementAt(1).Url);
            Assert.AreEqual("http://www.cnblogs.com/?id=11", pagedResult.Data.ElementAt(2).Url);
            Assert.AreEqual("http://www.cnblogs.com/?id=12", pagedResult.Data.ElementAt(3).Url);
            Assert.AreEqual("http://www.cnblogs.com/?id=13", pagedResult.Data.ElementAt(4).Url);
            Assert.AreEqual(0, pagedResult.SelectMany(b => b.Posts).Count());

            pagedResult = repository.Query().Sort(new DescBy<Blog>(b => b.Url)).Page(pageNumber: pageNumber, pageSize: pageSize);

            Assert.IsNotNull(pagedResult);
            Assert.AreEqual(pageNumber, pagedResult.PageNumber);
            Assert.AreEqual(pageSize, pagedResult.PageSize);
            Assert.AreEqual(pageSize * 1, pagedResult.Count);
            Assert.AreEqual(blogCount, pagedResult.TotalRecords);
            Assert.AreEqual(4, pagedResult.TotalPages);
            Assert.AreEqual("http://www.cnblogs.com/?id=9", pagedResult.Data.ElementAt(0).Url);
            Assert.AreEqual("http://www.cnblogs.com/?id=8", pagedResult.Data.ElementAt(1).Url);
            Assert.AreEqual("http://www.cnblogs.com/?id=7", pagedResult.Data.ElementAt(2).Url);
            Assert.AreEqual("http://www.cnblogs.com/?id=6", pagedResult.Data.ElementAt(3).Url);
            Assert.AreEqual("http://www.cnblogs.com/?id=5", pagedResult.Data.ElementAt(4).Url);
            Assert.AreEqual(0, pagedResult.SelectMany(b => b.Posts).Count());

            repository.Context.Dispose();
        }

        [TestMethod]
        public void TestEFCoreRepository_Query_WithCondition()
        {
            int blogCount = 20, postCount = 5, pageNumber = 1, pageSize = 5;

            DbHelper.ExecuteNonQuery(Contants.DbOperations.BatchInsertBlog.BuildCmdTexts(blogCount, postCount));

            Assert.AreEqual(expected: blogCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
            Assert.AreEqual(expected: blogCount * postCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));

            var repository = ServiceLocator.GetService<IRepository<Blog>>();

            var pagedResult = repository.Query(b => b.Url.Contains("2")).Sort(new AscBy<Blog>(b => b.Url)).Page(pageNumber: pageNumber, pageSize: pageSize);

            Assert.IsNotNull(pagedResult);
            Assert.AreEqual(pageNumber, pagedResult.PageNumber);
            Assert.AreEqual(pageSize, pagedResult.PageSize);
            Assert.AreEqual(3, pagedResult.Count);
            Assert.AreEqual(3, pagedResult.TotalRecords);
            Assert.AreEqual(1, pagedResult.TotalPages);
            Assert.AreEqual("http://www.cnblogs.com/?id=12", pagedResult.Data.ElementAt(0).Url);
            Assert.AreEqual("http://www.cnblogs.com/?id=2", pagedResult.Data.ElementAt(1).Url);
            Assert.AreEqual("http://www.cnblogs.com/?id=20", pagedResult.Data.ElementAt(2).Url);
            Assert.AreEqual(0, pagedResult.SelectMany(b => b.Posts).Count());

            repository.Context.Dispose();
        }

        [TestMethod]
        public void TestEFCoreRepository_Query_WithCondition_IncludableProperties()
        {
            int blogCount = 20, postCount = 5, pageNumber = 1, pageSize = 5;

            DbHelper.ExecuteNonQuery(Contants.DbOperations.BatchInsertBlog.BuildCmdTexts(blogCount, postCount));

            Assert.AreEqual(expected: blogCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
            Assert.AreEqual(expected: blogCount * postCount, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));

            var repository = ServiceLocator.GetService<IRepository<Blog>>();

            var pagedResult = repository.Query(b => b.Url.Contains("2"), b => b.Posts)
               .Sort(new AscBy<Blog>(b => b.Url))
               .Page(pageNumber: pageNumber, pageSize: pageSize);

            Assert.IsNotNull(pagedResult);
            Assert.AreEqual(pageNumber, pagedResult.PageNumber);
            Assert.AreEqual(pageSize, pagedResult.PageSize);
            Assert.AreEqual(3, pagedResult.Count);
            Assert.AreEqual(3, pagedResult.TotalRecords);
            Assert.AreEqual(1, pagedResult.TotalPages);
            Assert.AreEqual("http://www.cnblogs.com/?id=12", pagedResult.Data.ElementAt(0).Url);
            Assert.AreEqual("http://www.cnblogs.com/?id=2", pagedResult.Data.ElementAt(1).Url);
            Assert.AreEqual("http://www.cnblogs.com/?id=20", pagedResult.Data.ElementAt(2).Url);
            Assert.AreEqual(3 * postCount, pagedResult.SelectMany(b => b.Posts).Count());

            repository.Context.Dispose();
        }
    }
}
