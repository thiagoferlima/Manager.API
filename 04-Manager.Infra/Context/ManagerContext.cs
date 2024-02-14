using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.Domain.Entities;
using Manager.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Context
{
    public class ManagerContext : DbContext
    {
        public ManagerContext() { }

        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options) 
        { }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder Builder)
        {
            Builder.ApplyConfiguration(new UserMap());
        }

    }
}