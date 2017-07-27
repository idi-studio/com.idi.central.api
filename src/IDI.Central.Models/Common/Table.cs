using System.Collections.Generic;
using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Common
{
    public class Table<TRow> : IQueryResult
    {
        [JsonProperty("rows")]
        public List<TRow> Rows { get; set; } = new List<TRow>();
    }
}
