using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Central.Domain.Modules.Administration.Queries;
using IDI.Central.Models.Administration;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure;
using Newtonsoft.Json;

namespace IDI.Central.Domain
{
    public class ApplicationAuthorization : Authorization
    {
        public ApplicationAuthorization() : base("IDI.Central") { }

        protected override Dictionary<string, List<IPermission>> GroupByRole(List<IPermission> permissions)
        {
            var dictionary = new Dictionary<string, List<IPermission>>();

            var result = Runtime.Querier.Execute<QueryRolesCondition, Set<RoleModel>>();

            if (result.Status != ResultStatus.Success)
                return dictionary;

            foreach (var role in result.Data)
            {
                if (dictionary.ContainsKey(role.Name) || role.Permissions.IsNull())
                    continue;

                var list = role.Permissions.To<Dictionary<string, List<string>>>().SelectMany(e => e.Value);

                dictionary.Add(role.Name, permissions.Where(p => list.Contains(p.Code)).ToList());
            }

            return dictionary;
        }
    }
}
