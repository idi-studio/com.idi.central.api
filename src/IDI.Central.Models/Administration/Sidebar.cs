using System;
using System.Collections.Generic;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Queries;

namespace IDI.Central.Models.Administration
{
    public class Sidebar : IQueryResult
    {
        public UserProfile Profile { get; set; }

        public List<Menu> Menus { get; set; } = new List<Menu>();

        public class UserProfile
        {
            public string Name { get; set; }

            public string Photo { get; set; }

            public Gender Gender { get; set; }

            public DateTime Birthday { get; set; }
        }

        public class Menu
        {
            public int SN { get; set; }

            public string Name { get; set; }

            public string Icon { get; set; }

            public List<SubMenu> Subs { get; set; } = new List<SubMenu>();
        }

        public class SubMenu
        {
            public int SN { get; set; }

            public string Code { get; set; }

            public string Name { get; set; }

            public string Controller { get; set; }

            public string Action { get; set; }
        }
    }
}
