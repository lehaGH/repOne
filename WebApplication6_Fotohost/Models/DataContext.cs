namespace WebApplication6_Fotohost.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public DbSet<DataImg> DataImgs { get; set; }
    }
}
