using System.Collections.Generic;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Administration
{
    public class RoleTable : IQueryResult
    {
        [JsonProperty("rows")]
        public List<RoleRow> Rows { get; set; } = new List<RoleRow>();
    }
}
