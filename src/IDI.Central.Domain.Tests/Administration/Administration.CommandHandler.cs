using System;
using System.Linq;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Domain.Modules.Administration.Commands;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure;
using IDI.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Central.Domain.Tests
{
    public partial class AdministrationUnitTests : IntegrationTests
    {
        [TestMethod]
        public void Administration_UserRegisterCommand()
        {
            var hanlder = new UserRegistrationCommandHandler();
            hanlder.Users = Runtime.GetService<IRepository<User>>();

            var result = hanlder.Execute(new UserRegistrationCommand("administrator", "123456", "123456"));

            Assert.AreEqual(ResultStatus.Success, result.Status);
            Assert.AreEqual("注册成功!", result.Message);

            TestData(context =>
            {
                Assert.AreEqual(1, context.Users.Count());
            });
        }

        //[TestMethod]
        //public void Administration_RoleAuthorizationCommand()
        //{
        //    var module = new Module { Name = "Administration", Code = "Administration" };
        //    var privilege1 = new Permission { Id = Utils.NewGuid(1), Name = "privilege1", Code = "action1", Type = PermissionType.Query, Module = module };
        //    var privilege2 = new Permission { Id = Utils.NewGuid(2), Name = "privilege2", Code = "action2", Type = PermissionType.Query, Module = module };
        //    var privilege3 = new Permission { Id = Utils.NewGuid(3), Name = "privilege3", Code = "action3", Type = PermissionType.Query, Module = module };
        //    var privilege4 = new Permission { Id = Utils.NewGuid(4), Name = "privilege4", Code = "action4", Type = PermissionType.Query, Module = module };

        //    var role = new Role { Name = "administrator" };
        //    role.RolePermissions.Add(new RolePermission { Role = role, Permission = privilege1 });
        //    role.RolePermissions.Add(new RolePermission { Role = role, Permission = privilege2 });
        //    role.RolePermissions.Add(new RolePermission { Role = role, Permission = privilege3 });

        //    TestData(context =>
        //    {
        //        context.Permissions.AddRange(privilege1, privilege2, privilege3, privilege4);
        //        context.Roles.Add(role);
        //        context.SaveChanges();
        //        context.Dispose();
        //    });

        //    var hanlder = new RoleAuthorizationCommandHandler();
        //    hanlder.Roles = Runtime.GetService<IRepository<Role>>();
        //    hanlder.Permissions = Runtime.GetService<IRepository<Permission>>();

        //    var result = hanlder.Execute(new RoleAuthorizationCommand(role.Name, new Guid[] { privilege2.Id, privilege3.Id, privilege4.Id }));

        //    Assert.AreEqual(ResultStatus.Success, result.Status);
        //    Assert.AreEqual("角色授权成功!", result.Message);

        //    TestData(context =>
        //    {
        //        Assert.AreEqual(1, context.Modules.Count());
        //        Assert.AreEqual(4, context.Permissions.Count());
        //        Assert.AreEqual(1, context.Roles.Count());
        //        Assert.AreEqual(3, context.RolePermissions.Count());
        //        Assert.IsTrue(context.RolePermissions.Any(e => e.PermissionId == privilege2.Id));
        //        Assert.IsTrue(context.RolePermissions.Any(e => e.PermissionId == privilege3.Id));
        //        Assert.IsTrue(context.RolePermissions.Any(e => e.PermissionId == privilege4.Id));
        //    });
        //}

        //[TestMethod]
        //public void Administration_UserAuthorizeCommand()
        //{
        //    var role1 = new Role { Id = Utils.NewGuid(1), Name = "role1" };
        //    var role2 = new Role { Id = Utils.NewGuid(2), Name = "role2" };
        //    var role3 = new Role { Id = Utils.NewGuid(3), Name = "role3" };
        //    var role4 = new Role { Id = Utils.NewGuid(4), Name = "role4" };
        //    var user = new User { UserName = "user", Password = "123456" };
        //    user.UserRoles.Add(new UserRole { User = user, Role = role1 });
        //    user.UserRoles.Add(new UserRole { User = user, Role = role2 });
        //    user.UserRoles.Add(new UserRole { User = user, Role = role3 });

        //    TestData(context =>
        //    {
        //        context.Roles.AddRange(role1, role2, role3, role4);
        //        context.Users.Add(user);
        //        context.SaveChanges();
        //        context.Dispose();
        //    });

        //    var hanlder = new UserAuthorizeCommandHandler();
        //    hanlder.Users = Runtime.GetService<IRepository<User>>();
        //    hanlder.Roles = Runtime.GetService<IRepository<Role>>();

        //    var result = hanlder.Execute(new UserAuthorizeCommand(user.UserName, new Guid[] { role2.Id, role3.Id, role4.Id }));

        //    Assert.AreEqual(ResultStatus.Success, result.Status);
        //    Assert.AreEqual("用户角色授权成功!", result.Message);

        //    TestData(context =>
        //    {
        //        Assert.AreEqual(1, context.Users.Count());
        //        Assert.AreEqual(4, context.Roles.Count());
        //        Assert.AreEqual(3, context.UserRoles.Count());
        //        Assert.IsTrue(context.UserRoles.Any(e => e.RoleId == role2.Id));
        //        Assert.IsTrue(context.UserRoles.Any(e => e.RoleId == role3.Id));
        //        Assert.IsTrue(context.UserRoles.Any(e => e.RoleId == role4.Id));
        //    });
        //}
    }
}
