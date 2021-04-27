using System;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Devon4Net.WebAPI.Implementation.Domain.Database
{
    public partial class AlejandriaContext : DbContext
    {
        public AlejandriaContext()
        {
        }

        public AlejandriaContext(DbContextOptions<AlejandriaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<AuthorBook> AuthorBook { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=changeme");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<AuthorBook>(entity =>
            {
                entity.ToTable("Author_Book");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.AuthorId).HasColumnName("Author_Id");

                entity.Property(e => e.BookId).HasColumnName("Book_Id");

                entity.Property(e => e.PublishDate)
                    .HasColumnName("Publish_Date")
                    .HasColumnType("date");

                entity.Property(e => e.ValidityDate)
                    .HasColumnName("Validity_Date")
                    .HasColumnType("date");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.AuthorBook)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("author_book_fk");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.AuthorBook)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("author_book_fk_1");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Genere)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Summary)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Password)
                    .HasName("users_un1")
                    .IsUnique();

                entity.HasIndex(e => e.UserId)
                    .HasName("users_un")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.UserRole)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("users_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
