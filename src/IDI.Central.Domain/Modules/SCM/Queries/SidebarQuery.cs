using System.Linq;
using IDI.Central.Domain.Modules.SCM.AggregateRoots;
using IDI.Central.Domain.Modules.SCM.Conditions;
using IDI.Central.Models.SCM;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.SCM.Queries
{
    public class SidebarQuery : Query<SidebarQueryCondition, Sidebar>
    {
        [Injection]
        public IQueryRepository<Menu> MenuRepository { get; set; }

        [Injection]
        public IQueryRepository<User> UserRepository { get; set; }

        [Injection]
        public IQueryRepository<RolePrivilege> RolePrivilegeRepository { get; set; }

        public override Result<Sidebar> Execute(SidebarQueryCondition condition)
        {
            var user = this.UserRepository.Find(e => e.UserName == condition.UserName, e => e.Profile, e => e.UserRoles);

            if (user == null)
                return Result.Fail<Sidebar>($"无效的用户名'{condition.UserName}'");

            var userRoles = user.UserRoles.Select(e => e.RoleId).ToList();

            var privileges = this.RolePrivilegeRepository.Get(e => userRoles.Contains(e.RoleId), e => e.Privilege).Select(e => e.Privilege).ToList();

            var menus = this.MenuRepository.Get(e => e.Module);
            menus = menus.Where(e => e.IsAuthorized(privileges)).ToList();

            var sidebar = new Sidebar
            {
                Profile = new Sidebar.UserProfile { Name = user.Profile.Name, Photo = user.Profile.Photo, Gender = user.Profile.Gender, Birthday = user.Profile.Birthday },
                Menus = menus.GroupBy(m => m.Module).Select(g =>
                {
                    var module = g.Key;

                    return new Sidebar.Menu
                    {
                        SN = module.SN,
                        Name = module.Name,
                        Icon = module.Icon,
                        Subs = g.Where(e => e.Display).Select(e => new Sidebar.SubMenu
                        {
                            SN = e.SN,
                            Name = e.Name,
                            Code = e.Code,
                            Controller = e.Controller,
                            Action = e.Action
                        }).OrderBy(e => e.SN).ToList()
                    };
                }).OrderBy(m => m.SN).ToList()
            };

            return Result.Success(sidebar);
        }
    }
}
