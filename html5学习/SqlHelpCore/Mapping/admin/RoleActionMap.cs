using SqlHelpCore.Model.admin;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelpCore.Mapping.admin
{
    class RoleActionMap: EntityTypeConfiguration<RoleAction>
    {
        public RoleActionMap()
        {
            this.ToTable("admin_roleaction");

            this.HasKey(s => new { s.RoleId, s.ActionId });

            this.Property(s => s.RoleId)
               .HasColumnName("roleid");

            this.Property(s => s.ActionId)
                .HasColumnName("actionid");
        }
    }
}
