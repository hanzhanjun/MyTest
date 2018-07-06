using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace SqlHelpCore.Mapping.User
{
    class UserMap:EntityTypeConfiguration<Model.User.User>
    {
        public UserMap()
        {
            this.ToTable("Learn_User");

            this.HasKey(s => s.Id)
                .Property(s => s.Id)
                .HasColumnName("id");

            this.Property(s => s.UserName)
                .HasColumnName("username");

            this.Property(s => s.RealName)
                .HasColumnName("realname");

            this.Property(s => s.Moblie)
                .HasColumnName("moblie");

            this.Property(s => s.PassWard)
                .HasColumnName("passward");

            this.Property(s => s.Email)
                .HasColumnName("email");

            this.Property(s => s.CreateTime)
                .HasColumnName("createtime");

            this.Property(s => s.Birthday)
                .HasColumnName("birthday");

            this.Property(s => s.Qq)
                .HasColumnName("qq");

            this.Property(s => s.Sex)
                .HasColumnName("sex");

        }
    }
}
