using IDI.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace IDI.Central.Domain.Tests
{
    [TestClass]
    public abstract class IntegrationTests
    {
        private const string connectionString = @"Server=localhost; Database=com.idi.central;User Id = sa; Password = p@55w0rd;";

        [TestInitialize]
        public void Init()
        {
            ServiceLocator.AddDbContext<CentralContext>(options => options.UseSqlServer(connectionString, o => o.UseRowNumberForPaging()));

            using (var context = ServiceLocator.GetService<CentralContext>())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        protected void TestData(Action<CentralContext> action)
        {
            using (var context = ServiceLocator.GetService<CentralContext>())
            {
                action(context);
            }
        }
    }
}
