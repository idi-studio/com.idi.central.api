using System;

namespace IDI.Central.Models.SCM
{
    public class RoleTableRow
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = "";

        public string Descrition { get; set; } = "";

        public bool IsActive { get; set; }
    }
}
