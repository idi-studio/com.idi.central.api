using System.Collections.Generic;
using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace IDI.Central.Domain
{
    public class CentralContext : DomainContext
    {
        //public override List<string> Modules => new List<string>
        //{
        //    "IDI.Central.Domain.Modules.Administration",
        //    "IDI.Central.Domain.Modules.BasicInfo",
        //    "IDI.Central.Domain.Modules.Inventory",
        //    "IDI.Central.Domain.Modules.Sales"
        //};

        public CentralContext(DbContextOptions options) : base(options) { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    new Modules.Administration.Mapping().Create(modelBuilder);
        //    new Modules.BasicInfo.Mapping().Create(modelBuilder);
        //    new Modules.Inventory.Mapping().Create(modelBuilder);
        //    new Modules.Sales.Mapping().Create(modelBuilder);

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
