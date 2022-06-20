using MySql.Data.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AprelKonsaltingAPP.DataBase
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public partial class EfModel : DbContext
    {
        private static EfModel Instance;
        public static EfModel Init()
        {
            if (Instance == null)
                Instance = new EfModel();
            return Instance;
        }
        public EfModel()
            : base($"server={Properties.Settings.Default.Host};port={Properties.Settings.Default.Port};userid={Properties.Settings.Default.UserName};password={Properties.Settings.Default.Password};database={Properties.Settings.Default.DataBase}")
        {
        }

        public virtual DbSet<client> clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<client>()
                .Property(e => e.surname)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.firstname)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.middlename)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.gender)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.email)
                .IsUnicode(false);
        }
    }
}
