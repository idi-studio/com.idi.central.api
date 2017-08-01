using System;
using IDI.Core.Infrastructure;
using IDI.Core.Repositories;
using IDI.Core.Tests.Utils;
using IDI.Core.Tests.Utils.AggregateRoots;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Core.Tests.Repositories.EFCore
{
    [TestClass]
    public class EFCoreRepositoryUnitTests_CUD : EFCoreRepositoryUnitTest
    {
        [TestMethod, TestCategory("EFCore")]
        public void TestEFCoreRepository_Add_Success()
        {
            User user = new User
            {
                UserName = "user",
                Password = "123456",
            };

            var repository = ServiceLocator.GetService<IRepository<User>>();

            repository.Add(user);
            repository.Context.Commit();
            repository.Context.Dispose();

            int actual = DbHelper.ReadRecordCount(Contants.Tables.Users);

            Assert.AreEqual(expected: 1, actual: actual);
        }

        [TestMethod]
        public void TestEFCoreRepository_Update_Success()
        {
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertUser.CmdText);

            int actual = DbHelper.ReadRecordCount(Contants.Tables.Users);
            Assert.AreEqual(expected: 1, actual: actual);

            var repository = ServiceLocator.GetService<IRepository<User>>();

            User user = repository.Find(new Guid(Contants.DbOperations.InsertUser.PK));

            Assert.IsNotNull(user);

            user.Password = "654321";
            repository.Update(user);
            repository.Context.Commit();

            User user2 = repository.Find(new Guid(Contants.DbOperations.InsertUser.PK));

            repository.Context.Dispose();

            Assert.IsNotNull(user2);
            Assert.AreEqual(user.UserName, user2.UserName);
            Assert.AreEqual(user.Password, user2.Password);
        }

        [TestMethod]
        public void TestEFCoreRepository_Remove_Success()
        {
            DbHelper.ExecuteNonQuery(Contants.DbOperations.InsertUser.CmdText);

            int actual = DbHelper.ReadRecordCount(Contants.Tables.Users);
            Assert.AreEqual(expected: 1, actual: actual);

            var repository = ServiceLocator.GetService<IRepository<User>>();

            User user = repository.Find(new Guid(Contants.DbOperations.InsertUser.PK));

            Assert.IsNotNull(user);

            repository.Remove(user);
            repository.Context.Commit();
            repository.Context.Dispose();

            actual = DbHelper.ReadRecordCount(Contants.Tables.Users);
            Assert.AreEqual(expected: 0, actual: actual);
        }
    }
}
