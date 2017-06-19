﻿using System;
using System.Linq;
using IDI.Central.Domain.Common;
using IDI.Central.Domain.Modules.SCM.AggregateRoots;
using IDI.Central.Domain.Modules.SCM.Commands;
using IDI.Central.Domain.Modules.SCM.Handlers;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using IDI.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDI.Central.Domain.Tests
{
    public partial class SCMTests : IntegrationTests
    {
        [TestMethod]
        public void SCM_Command_Initialize()
        {
            var hanlder = new InitializeCommandHandler();
            hanlder.UserRepository = ServiceLocator.GetService<IRepository<User>>();
            hanlder.RoleRepository = ServiceLocator.GetService<IRepository<Role>>();
            hanlder.ModuleRepository = ServiceLocator.GetService<IRepository<Module>>();

            var result = hanlder.Execute(new InitializeCommand());

            Assert.AreEqual(ResultStatus.Success, result.Status);
            Assert.AreEqual("初始化成功!", result.Message);

            TestData(context =>
            {
                Assert.AreEqual(1, context.Users.Count());
                Assert.AreEqual(1, context.UserRoles.Count());
                Assert.AreEqual(1, context.Roles.Count());
                Assert.AreEqual(1, context.Modules.Count());
                Assert.AreEqual(5, context.Menus.Count());
                Assert.AreEqual(5, context.Privileges.Count());
            });
        }

        [TestMethod]
        public void SCM_Command_Register()
        {
            var hanlder = new RegisterCommandHandler();
            hanlder.UserRepository = ServiceLocator.GetService<IRepository<User>>();

            var result = hanlder.Execute(new RegisterCommand("administrator", "123456", "123456"));

            Assert.AreEqual(ResultStatus.Success, result.Status);
            Assert.AreEqual("注册成功!", result.Message);

            TestData(context =>
            {
                Assert.AreEqual(1, context.Users.Count());
            });
        }

        [TestMethod]
        public void SCM_Command_RoleAuthorize()
        {
            var module = new Module { Name = "SCM", Code = "SCM" };
            var privilege1 = new Privilege { Id = Utils.NewGuid(1), Name = "privilege1", Code = "action1", PrivilegeType = PrivilegeType.View, Module = module };
            var privilege2 = new Privilege { Id = Utils.NewGuid(2), Name = "privilege2", Code = "action2", PrivilegeType = PrivilegeType.View, Module = module };
            var privilege3 = new Privilege { Id = Utils.NewGuid(3), Name = "privilege3", Code = "action3", PrivilegeType = PrivilegeType.View, Module = module };
            var privilege4 = new Privilege { Id = Utils.NewGuid(4), Name = "privilege4", Code = "action4", PrivilegeType = PrivilegeType.View, Module = module };

            var role = new Role { Name = "administrator" };
            role.RolePrivileges.Add(new RolePrivilege { Role = role, Privilege = privilege1 });
            role.RolePrivileges.Add(new RolePrivilege { Role = role, Privilege = privilege2 });
            role.RolePrivileges.Add(new RolePrivilege { Role = role, Privilege = privilege3 });

            TestData(context =>
            {
                context.Privileges.AddRange(privilege1, privilege2, privilege3, privilege4);
                context.Roles.Add(role);
                context.SaveChanges();
                context.Dispose();
            });

            var hanlder = new RoleAuthorizeCommandHandler();
            hanlder.RoleRepository = ServiceLocator.GetService<IRepository<Role>>();
            hanlder.PrivilegeRepository = ServiceLocator.GetService<IRepository<Privilege>>();

            var result = hanlder.Execute(new RoleAuthorizeCommand(role.Name, new Guid[] { privilege2.Id, privilege3.Id, privilege4.Id }));

            Assert.AreEqual(ResultStatus.Success, result.Status);
            Assert.AreEqual("角色授权成功!", result.Message);

            TestData(context =>
            {
                Assert.AreEqual(1, context.Modules.Count());
                Assert.AreEqual(4, context.Privileges.Count());
                Assert.AreEqual(1, context.Roles.Count());
                Assert.AreEqual(3, context.RolePrivileges.Count());
                Assert.IsTrue(context.RolePrivileges.Any(e => e.PrivilegeId == privilege2.Id));
                Assert.IsTrue(context.RolePrivileges.Any(e => e.PrivilegeId == privilege3.Id));
                Assert.IsTrue(context.RolePrivileges.Any(e => e.PrivilegeId == privilege4.Id));
            });
        }

        [TestMethod]
        public void SCM_Command_UserAuthorize()
        {
            var role1 = new Role { Id = Utils.NewGuid(1), Name = "role1" };
            var role2 = new Role { Id = Utils.NewGuid(2), Name = "role2" };
            var role3 = new Role { Id = Utils.NewGuid(3), Name = "role3" };
            var role4 = new Role { Id = Utils.NewGuid(4), Name = "role4" };
            var user = new User { UserName = "user", Password = "123456" };
            user.UserRoles.Add(new UserRole { User = user, Role = role1 });
            user.UserRoles.Add(new UserRole { User = user, Role = role2 });
            user.UserRoles.Add(new UserRole { User = user, Role = role3 });

            TestData(context =>
            {
                context.Roles.AddRange(role1, role2, role3, role4);
                context.Users.Add(user);
                context.SaveChanges();
                context.Dispose();
            });

            var hanlder = new UserAuthorizeCommandHandler();
            hanlder.UserRepository = ServiceLocator.GetService<IRepository<User>>();
            hanlder.RoleRepository = ServiceLocator.GetService<IRepository<Role>>();

            var result = hanlder.Execute(new UserAuthorizeCommand(user.UserName, new Guid[] { role2.Id, role3.Id, role4.Id }));

            Assert.AreEqual(ResultStatus.Success, result.Status);
            Assert.AreEqual("用户角色授权成功!", result.Message);

            TestData(context =>
            {
                Assert.AreEqual(1, context.Users.Count());
                Assert.AreEqual(4, context.Roles.Count());
                Assert.AreEqual(3, context.UserRoles.Count());
                Assert.IsTrue(context.UserRoles.Any(e => e.RoleId == role2.Id));
                Assert.IsTrue(context.UserRoles.Any(e => e.RoleId == role3.Id));
                Assert.IsTrue(context.UserRoles.Any(e => e.RoleId == role4.Id));
            });
        }
    }
}
