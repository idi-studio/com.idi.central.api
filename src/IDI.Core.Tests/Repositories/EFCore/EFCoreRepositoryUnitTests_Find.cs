using System;
using IDI.Core.Infrastructure;
using IDI.Core.Repositories;
using IDI.Core.Tests.TestUtils;
using IDI.Core.Tests.TestUtils.AggregateRoots;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Core.Tests.Repositories.EFCore
{
    [TestClass]
    public class EFCoreRepositoryUnitTests_Find : EFCoreRepositoryUnitTest
    {
        [TestMethod]
        public void TestEFCoreRepository_Find_ByKey()
        {
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertBlog.CmdText);
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertPost.CmdText1);
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertPost.CmdText2);

            Assert.AreEqual(expected: 1, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
            Assert.AreEqual(expected: 2, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));

            var repository = Runtime.GetService<IRepository<Blog>>();

            var blog = repository.Find(Contants.DbOperations.InsertBlog.PK.ToGuid());

            Assert.IsNotNull(blog);
            Assert.AreEqual(0, blog.Posts.Count);
        }

        [TestMethod]
        public void TestEFCoreRepository_Find_ByKey_Inculde_Navigation_Property()
        {
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertBlog.CmdText);
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertPost.CmdText1);
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertPost.CmdText2);

            Assert.AreEqual(expected: 1, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
            Assert.AreEqual(expected: 2, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));

            var repository = Runtime.GetService<IRepository<Blog>>();

            var blog = repository.Include(e => e.Posts).Find(Contants.DbOperations.InsertBlog.PK.ToGuid());

            Assert.IsNotNull(blog);
            Assert.AreEqual(2, blog.Posts.Count);
        }

        [TestMethod]
        public void TestEFCoreRepository_Find_ByCondition()
        {
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertBlog.CmdText);
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertPost.CmdText1);
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertPost.CmdText2);

            Assert.AreEqual(expected: 1, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
            Assert.AreEqual(expected: 2, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));

            var repository = Runtime.GetService<IRepository<Blog>>();

            var blog = repository.Find(b => b.Url == Contants.DbOperations.InsertBlog.URL);

            Assert.IsNotNull(blog);
            Assert.AreEqual(0, blog.Posts.Count);
        }

        [TestMethod]
        public void TestEFCoreRepository_Find_ByCondition_Inculde_Navigation_Property()
        {
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertBlog.CmdText);
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertPost.CmdText1);
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertPost.CmdText2);

            Assert.AreEqual(expected: 1, actual: DbHelper.ReadRecordCount(Contants.Tables.Blogs));
            Assert.AreEqual(expected: 2, actual: DbHelper.ReadRecordCount(Contants.Tables.Posts));

            var repository = Runtime.GetService<IRepository<Blog>>();

            var blog = repository.Include(e => e.Posts).Find(b => b.Url == Contants.DbOperations.InsertBlog.URL);

            Assert.IsNotNull(blog);
            Assert.AreEqual(2, blog.Posts.Count);
        }
    }
}
