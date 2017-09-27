//using System.Linq;
//using IDI.Central.Domain.Localization;
//using IDI.Central.Domain.Modules.Administration.AggregateRoots;
//using IDI.Central.Models.Administration;
//using IDI.Core.Common;
//using IDI.Core.Infrastructure.DependencyInjection;
//using IDI.Core.Infrastructure.Queries;
//using IDI.Core.Infrastructure.Verification.Attributes;
//using IDI.Core.Repositories;

//namespace IDI.Central.Domain.Modules.Administration.Queries
//{
//    public class QuerySidebarCondition : Condition
//    {
//        [RequiredField]
//        public string UserName { get; set; }
//    }

//    public class QuerySidebar : Query<QuerySidebarCondition, Sidebar>
//    {
//        [Injection]
//        public IQueryableRepository<Menu> Menus { get; set; }

//        [Injection]
//        public IQueryableRepository<User> Users { get; set; }

//        [Injection]
//        public IQueryableRepository<RolePermission> RolePermissions { get; set; }

//        public override Result<Sidebar> Execute(QuerySidebarCondition condition)
//        {
//            var user = this.Users.Include(e => e.Profile).Include(e => e.UserRoles).Find(e => e.UserName == condition.UserName);

//            if (user == null)
//                return Result.Fail<Sidebar>(Localization.Get(Resources.Key.Command.InvalidUser));

//            var userRoles = user.UserRoles.Select(e => e.RoleId).ToList();

//            var permissions = this.RolePermissions.Include(e=>e.Permission).Get(e => userRoles.Contains(e.RoleId)).Select(e => e.Permission).ToList();

//            var menus = this.Menus.Include(e=>e.Module).Get();
//            menus = menus.Where(e => e.IsAuthorized(permissions)).ToList();

//            var sidebar = new Sidebar
//            {
//                Profile = new Sidebar.UserProfile { Name = user.Profile.Name, Photo = user.Profile.Photo, Gender = user.Profile.Gender, Birthday = user.Profile.Birthday },
//                Menus = menus.GroupBy(m => m.Module).Select(g =>
//                {
//                    var module = g.Key;

//                    return new Sidebar.Menu
//                    {
//                        SN = module.SN,
//                        Name = module.Name,
//                        Icon = module.Icon,
//                        Subs = g.Where(e => e.Display).Select(e => new Sidebar.SubMenu
//                        {
//                            SN = e.SN,
//                            Name = e.Name,
//                            Code = e.Code,
//                            Controller = e.Controller,
//                            Action = e.Action
//                        }).OrderBy(e => e.SN).ToList()
//                    };
//                }).OrderBy(m => m.SN).ToList()
//            };

//            return Result.Success(sidebar);
//        }
//    }
//}
