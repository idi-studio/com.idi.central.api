using IDI.Core.Infrastructure;
using IDI.Core.Tests.Utils;
using IDI.Core.Tests.Utils.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Core.Tests.Repositories.EFCore
{
    [TestClass]
    public class EFCoreRepositoryUnitTest
    {
        [TestInitialize]
        public void Init()
        {
            DbHelper.Clear(Contants.Tables.Users, Contants.Tables.Blogs, Contants.Tables.Posts);

            ServiceLocator.AddDbContext<EFCoreContext>(options => options.UseSqlServer(Contants.ConnectionStrings.EFTestDb, o => o.UseRowNumberForPaging()));
        }

        [TestCleanup]
        public void ClearUp()
        {
            ServiceLocator.Clear();
        }
    }
}
