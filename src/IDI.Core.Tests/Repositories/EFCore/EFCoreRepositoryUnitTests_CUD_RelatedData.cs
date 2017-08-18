using IDI.Core.Common;
using IDI.Core.Infrastructure;
using IDI.Core.Repositories;
using IDI.Core.Tests.TestUtils;
using IDI.Core.Tests.TestUtils.AggregateRoots;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Core.Tests.Repositories.EFCore
{
    [TestClass]
    public class EFCoreRepositoryUnitTests_CUD_RelatedData : EFCoreRepositoryUnitTest
    {
        [TestMethod]
        public void TestEFCoreRepository_Add_Success()
        {
            var blog = new Blog { Url = "http://www.cnblogs.com/" };
            blog.Posts.Add(new Post { Title = "TestTitle1", Content = "TestContent1" });
            blog.Posts.Add(new Post { Title = "TestTitle2", Content = "TestContent2" });

            var repository = ServiceLocator.GetService<IRepository<Blog>>();

            repository.Add(blog);
            repository.Context.Commit();
            repository.Context.Dispose();

            Assert.AreEqual(expected: 1, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
            Assert.AreEqual(expected: 2, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));
        }

        [TestMethod]
        public void TestEFCoreRepository_Find_And_Addition_Navigation_Property_Success()
        {
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertBlog.CmdText);
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertPost.CmdText1);
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertPost.CmdText2);

            Assert.AreEqual(expected: 1, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
            Assert.AreEqual(expected: 2, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));

            var repository = ServiceLocator.GetService<IRepository<Blog>>();

            var blog = repository.Find(Contants.DbOperations.InsertBlog.PK.ToGuid(), b => b.Posts);

            Assert.IsNotNull(blog);
            Assert.AreEqual(2, blog.Posts.Count);

            blog.Posts.Add(new Post { Title = "TestTitle3", Content = "TestContent3" });

            repository.Update(blog);
            repository.Context.Commit();

            Assert.AreEqual(expected: 1, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
            Assert.AreEqual(expected: 3, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));
        }

        [TestMethod]
        public void TestEFCoreRepository_Find_And_Deletion_Navigation_Property_Success()
        {
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertBlog.CmdText);
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertPost.CmdText1);
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertPost.CmdText2);

            Assert.AreEqual(expected: 1, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
            Assert.AreEqual(expected: 2, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));

            var repository = ServiceLocator.GetService<IRepository<Blog>>();

            var blog = repository.Find(Contants.DbOperations.InsertBlog.PK.ToGuid(), b => b.Posts);

            Assert.IsNotNull(blog);
            Assert.AreEqual(2, blog.Posts.Count);

            blog.Posts.RemoveAt(1);

            repository.Update(blog);
            repository.Context.Commit();

            Assert.AreEqual(expected: 1, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
            Assert.AreEqual(expected: 1, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));
        }

        [TestMethod]
        public void TestEFCoreRepository_Find_And_Update_Navigation_Property_Success()
        {
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertBlog.CmdText);
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertPost.CmdText1);
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertPost.CmdText2);

            Assert.AreEqual(expected: 1, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
            Assert.AreEqual(expected: 2, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));

            var repository = ServiceLocator.GetService<IRepository<Blog>>();

            var blog = repository.Find(Contants.DbOperations.InsertBlog.PK.ToGuid(), b => b.Posts);

            Assert.IsNotNull(blog);
            Assert.AreEqual(2, blog.Posts.Count);

            var newURL = "https://github.com/";
            var newContent = "TestContent3";

            blog.Url = newURL;
            blog.Posts[0].Content = newContent;

            repository.Update(blog);
            repository.Context.Commit();

            Assert.AreEqual(expected: 1, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
            Assert.AreEqual(expected: 2, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));

            var newBlog = repository.Find(Contants.DbOperations.InsertBlog.PK.ToGuid(), b => b.Posts);

            Assert.IsNotNull(newBlog);
            Assert.AreEqual(newURL, newBlog.Url);
            Assert.AreEqual(2, newBlog.Posts.Count);
            Assert.AreEqual(newContent, newBlog.Posts[0].Content);
        }
    }
}
