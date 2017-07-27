using System.Linq;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Models.Administration;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Queries
{
    public class QuerySidebarCondition : Condition
    {
        [RequiredField("username")]
        public string UserName { get; set; }
    }

    public class QuerySidebar : Query<QuerySidebarCondition, Sidebar>
    {
        [Injection]
        public IQueryRepository<Menu> Menus { get; set; }

        [Injection]
        public IQueryRepository<User> Users { get; set; }

        [Injection]
        public IQueryRepository<RolePrivilege> RolePrivilegeRepository { get; set; }

        public override Result<Sidebar> Execute(QuerySidebarCondition condition)
        {
            var user = this.Users.Find(e => e.UserName == condition.UserName, e => e.Profile, e => e.UserRoles);

            if (user == null)
                return Result.Fail<Sidebar>($"无效的用户名'{condition.UserName}'");

            var userRoles = user.UserRoles.Select(e => e.RoleId).ToList();

            var privileges = this.RolePrivilegeRepository.Get(e => userRoles.Contains(e.RoleId), e => e.Privilege).Select(e => e.Privilege).ToList();

            var menus = this.Menus.Get(e => e.Module);
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
