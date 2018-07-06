using SqlHelpCore.Model.admin;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelpCore.Mapping.admin
{
    class AdminMenuMap: EntityTypeConfiguration<AdminMenu>
    {
        public AdminMenuMap()
        {
            this.ToTable("admin_menu");

            this.HasKey(s => s.Id)
                .Property(s => s.Id)
                .HasColumnName("menuid");

            this.Property(s => s.ParentId)
                .HasColumnName("parentmenuid");

            this.Property(s => s.Name)
                .HasColumnName("menuname");

            this.Property(s => s.Url)
                .HasColumnName("url");

            this.Property(s => s.Sort)
                .HasColumnName("sort");

            this.Property(s => s.IsVisible)
                .HasColumnName("isvisible");
        }
    }
}
