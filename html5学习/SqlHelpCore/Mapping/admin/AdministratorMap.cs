using SqlHelpCore.Model.admin;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelpCore.Mapping.admin
{
    class AdministratorMap: EntityTypeConfiguration<Administrator>
    {
        public AdministratorMap()
        {
            this.ToTable("admin_user");

            this.HasKey(s => s.Id)
                .Property(s => s.Id)
                .HasColumnName("userid");

            this.Property(s => s.UserName)
                .HasColumnName("username");

            this.Property(s => s.RealName)
                .HasColumnName("realname");

            this.Property(s => s.Status)
                .HasColumnName("userstate");

            this.Property(s => s.RoleId)
                .HasColumnName("roleid");

            this.Property(s => s.Password)
                .HasColumnName("userpassword");

            this.Property(s => s.CreateTime)
                .HasColumnName("createtime");
        }
    }
}
