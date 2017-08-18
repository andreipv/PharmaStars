namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBModel : DbContext
    {
        public DBModel()
            : base("name=DBModel")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Manufacturer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Manufacturer>()
                .Property(e => e.Adress)
                .IsUnicode(false);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Manufacturer)
                .HasForeignKey(e => e.ID_MRF);

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Categories)
                .WithMany(e => e.Products)
                .Map(cs =>
                {
                    cs.MapLeftKey("ID_CATEG");
                    cs.MapRightKey("ID_PROD");
                    cs.ToTable("Prod_Categ_Assoc");
                });
        }
    }
}
