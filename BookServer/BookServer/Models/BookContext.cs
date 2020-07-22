using Microsoft.EntityFrameworkCore;

namespace BookServer.Models
{
    // Using EntityFramework
    public partial class BookContext : DbContext
    {
        public BookContext()
        {
        }

        // It reads the connection string from appsettings.json see ConfigureServices in Startup.cs.
        // PM> Scaffold-DbContext -Project ProjectName "Data Source=MORPHEUS\SQLEXPRESS;Initial Catalog=CatTracker;Integrated Security=true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
        // Connect to localdb using SSMS: (LocalDb)\MSSQLLocalDB
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.ISBN)
                    .HasColumnName("ISBN")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.ISBN);

                entity.Property(e => e.ISBN)
                    .HasColumnName("ISBN")
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Genre)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PublishDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });
        }
    }
}
