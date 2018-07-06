using SqlHelpCore.Model.admin;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelpCore.Mapping.admin
{
    class AdminActionMap: EntityTypeConfiguration<AdminAction>
    {
        public AdminActionMap()
        {
            this.ToTable("admin_action");

            this.HasKey(s => s.Id)
                .Property(s => s.Id)
                .HasColumnName("actionid");

            this.Property(s => s.ActionKey)
                .HasColumnName("actionkey");

            this.Property(s => s.ActionName)
               .HasColumnName("actionname");

            this.Property(s => s.MenuId)
               .HasColumnName("menuid");

            this.Property(s => s.Sort)
               .HasColumnName("sort");
        }
    }
}
