﻿using System.Linq;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Domain.Modules.Administration.Commands;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Handlers
{
    public class UserAuthorizeCommandHandler : ICommandHandler<UserAuthorizeCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<User> Users { get; set; }

        [Injection]
        public IRepository<Role> Roles { get; set; }

        public Result Execute(UserAuthorizeCommand command)
        {
            var user = this.Users.Find(e => e.UserName == command.UserName, e => e.UserRoles);

            if (user == null)
                return Result.Fail(Localization.Get( Resources.Key.INVALID_USER));

            var recent = command.Roles.ToList();
            var current = user.UserRoles.Select(e => e.RoleId).ToList();
            var deletion = current.Except(recent).ToList();
            var addition = recent.Except(current).ToList();

            user.UserRoles.RemoveAll(e => deletion.Contains(e.RoleId));

            var roles = this.Roles.Get(e => addition.Contains(e.Id));
            var additionRoles = roles.Select(role => new UserRole { UserId = user.Id, RoleId = role.Id }).ToList();
            user.UserRoles.AddRange(additionRoles);

            this.Users.Update(user);
            this.Users.Context.Commit();
            this.Users.Context.Dispose();

            return Result.Success(message: Localization.Get( Resources.Key.USER_AUTHORIZATION_SUCCESS));
        }
    }
}
