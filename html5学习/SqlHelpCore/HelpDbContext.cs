using SqlHelpCore.Model.admin;
using SqlHelpCore.Model.User;
using System.Data.Entity;
using System.Reflection;

namespace SqlHelpCore
{
    public class HelpDbContext : DbContext
    {
        static HelpDbContext()
        {
            Database.SetInitializer<HelpDbContext>(null);
        }

        public HelpDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Administrator> Administrators { get; set; }

        public DbSet<AdminMenu> Menus { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<AdminAction> Actions { get; set; }

        public DbSet<RoleAction> RoleActions { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //当前代码所在程序集，加载所有的继承自EntityTypeConfiguration为模型配置类 
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
