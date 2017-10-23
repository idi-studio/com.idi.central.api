using System;
using System.Collections.Generic;
using IDI.Core.Authentication;
using IDI.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Central.Domain.Tests
{
    [TestClass]
    public abstract class IntegrationTests
    {
        private const string connectionString = @"Server=localhost; Database=com.idi.central;User Id = sa; Password = p@55w0rd;";

        [TestInitialize]
        public void Init()
        {
            Runtime.Initialize();
            Runtime.AddDbContext<CentralContext>(options => options.UseSqlServer(connectionString, o => o.UseRowNumberForPaging()));
            Runtime.UseAuthorization<FakeAuthorization>();

            using (var context = Runtime.GetService<CentralContext>())
            {
                context.Database.EnsureDeleted();
            }
        }

        protected void TestData(Action<CentralContext> action)
        {
            using (var context = Runtime.GetService<CentralContext>())
            {
                action(context);
            }
        }
    }

    public class FakeAuthorization : Authorization
    {
        public FakeAuthorization() : base("IDI.Central") { }

        protected override Dictionary<string, List<IPermission>> GroupByRole(List<IPermission> permissions)
        {
            return new Dictionary<string, List<IPermission>>();
        }
    }
}
