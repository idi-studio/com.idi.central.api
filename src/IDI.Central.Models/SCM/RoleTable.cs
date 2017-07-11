using System.Collections.Generic;
using IDI.Core.Infrastructure.Queries;

namespace IDI.Central.Models.Identity
{
    public class RoleTable : IQueryResult
    {
        public List<RoleTableRow> Rows { get; set; } = new List<RoleTableRow>();
    }
}
