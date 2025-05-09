using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GordumYedim.API.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=GordumYedimDb;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.CommentId).HasColumnName("commentId");
            entity.Property(e => e.Comment1)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.CommentResId).HasColumnName("commentResId");
            entity.Property(e => e.CommentTime)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("commentTime");
            entity.Property(e => e.CommentUserId).HasColumnName("commentUserId");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.CommentRes).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CommentResId)
                .HasConstraintName("FK_Comments_Restaurants");


            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.HasKey(e => e.ResId);

                entity.Property(e => e.ResId).HasColumnName("resId");
                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createdTime");
                entity.Property(e => e.Latitude)
                    .HasColumnType("decimal(9, 6)")
                    .HasColumnName("latitude");
                entity.Property(e => e.Longitude)
                    .HasColumnType("decimal(9, 6)")
                    .HasColumnName("longitude");
                entity.Property(e => e.PlaceId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("placeId");
                entity.Property(e => e.ResAddress)
                    .HasColumnType("nvarchar(max)")
                    .HasColumnName("resAddress");
                entity.Property(e => e.ResName)
                    .HasColumnType("nvarchar(max)")
                    .HasColumnName("resName");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("userId");
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");
                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");
                entity.Property(e => e.UserCity)
                    .HasMaxLength(50)
                    .HasColumnName("userCity");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        });
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
