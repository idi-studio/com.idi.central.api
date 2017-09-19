using System;
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

            using (var context = Runtime.GetService<CentralContext>())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
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
}
