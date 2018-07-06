using SqlHelpCore.Model.admin;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelpCore.Mapping.admin
{
    class RoleMap: EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            this.ToTable("admin_role");

            this.HasKey(s => s.Id)
                .Property(s => s.Id)
                .HasColumnName("roleid");

            this.Property(s => s.RoleName)
                .HasColumnName("rolename");

            this.Property(s => s.Remark)
                .HasColumnName("remark");

            this.Property(s => s.Createon)
                .HasColumnName("createon");

            this.Property(s => s.CreateTime)
                .HasColumnName("createtime");

            this.Property(s => s.IsDelete)
                .HasColumnName("isdelete");
        }
    }
}
