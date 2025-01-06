using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace FaiseStock.Data.Models;

public partial class FaiseStockDemoDbContext : DbContext
{
    public FaiseStockDemoDbContext()
    {
    }

    public FaiseStockDemoDbContext(DbContextOptions<FaiseStockDemoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DepositHistory> DepositHistories { get; set; }

    public virtual DbSet<TopUser> TopUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=FaiseStockDemo_DB;user id=root;password=mysql123", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<DepositHistory>(entity =>
        {
            entity.HasKey(e => e.DepositId).HasName("PRIMARY");

            entity.ToTable("deposit_history");

            entity.HasIndex(e => e.UserId, "deposit_history_user_id_foreign");

            entity.Property(e => e.DepositId).HasColumnName("deposit_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.DepositHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deposit_history_user_id_foreign");
        });

        modelBuilder.Entity<TopUser>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.CreateAt })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("top_user");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreateAt).HasColumnName("create_at");
            entity.Property(e => e.IncreasedAmount).HasColumnName("increased_amount");
            entity.Property(e => e.Rank).HasColumnName("rank");
            entity.Property(e => e.Roic).HasColumnName("ROIC");

            entity.HasOne(d => d.User).WithMany(p => p.TopUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("top_user_user_id_foreign");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasKey(e => e.WalletId).HasName("PRIMARY");

            entity.ToTable("wallet");

            entity.HasIndex(e => e.UserId, "wallet_user_id_foreign");

            entity.Property(e => e.WalletId).HasColumnName("wallet_id");
            entity.Property(e => e.Balance).HasColumnName("balance");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Wallets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("wallet_user_id_foreign");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
