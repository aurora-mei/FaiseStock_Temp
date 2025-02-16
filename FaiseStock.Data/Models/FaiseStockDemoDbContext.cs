using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace FaiseStock.Data.Models;

public partial class FaiseStockDemoDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public FaiseStockDemoDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public FaiseStockDemoDbContext(DbContextOptions<FaiseStockDemoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DepositHistory> DepositHistories { get; set; }

    public virtual DbSet<TopUser> TopUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }
    public virtual DbSet<Contest> Contests { get; set; }
    public virtual DbSet<ContestParticipant> ContestParticipants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql(_configuration.GetConnectionString("FaiseStock_DB"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder
        .UseCollation("utf8mb4_0900_ai_ci")
        .HasCharSet("utf8mb4");

    modelBuilder.Entity<DepositHistory>(entity =>
    {
        entity.HasKey(e => e.depositId).HasName("PRIMARY");

        entity.ToTable("deposit_history");

        entity.HasIndex(e => e.userId, "deposit_history_user_id_foreign");

        entity.Property(e => e.depositId).HasColumnName("deposit_id");
        entity.Property(e => e.amount).HasColumnName("amount");
        entity.Property(e => e.userId).HasColumnName("user_id");
        entity.Property(e => e.createAt).HasColumnName("create_at");

        entity.HasOne(d => d.user).WithMany(p => p.depositHistories)
            .HasForeignKey(d => d.userId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("deposit_history_user_id_foreign");
    });

    modelBuilder.Entity<TopUser>(entity =>
    {
        entity.HasKey(e => new { e.userId, e.createAt })
            .HasName("PRIMARY");

        entity.ToTable("top_user");

        entity.Property(e => e.userId).HasColumnName("user_id");
        entity.Property(e => e.contestId).HasColumnName("contest_id");
        entity.Property(e => e.createAt).HasColumnName("create_at");
        entity.Property(e => e.increasedAmount).HasColumnName("increased_amount");
        entity.Property(e => e.rank).HasColumnName("rank");
        entity.Property(e => e.roic).HasColumnName("ROIC");

        entity.HasOne(d => d.contest)
            .WithMany(p => p.topUsers)
            .HasForeignKey(d => d.contestId)
            .OnDelete(DeleteBehavior.Cascade) 
            .HasConstraintName("top_user_contest_id_foreign");

        entity.HasOne(d => d.user)
            .WithMany(p => p.topUsers)
            .HasForeignKey(d => d.userId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("top_user_user_id_foreign");
    });

    modelBuilder.Entity<User>(entity =>
    {
        entity.HasKey(e => e.userId).HasName("PRIMARY");

        entity.ToTable("user");

        entity.Property(e => e.userId).HasColumnName("user_id");
        entity.Property(e => e.name)
            .HasMaxLength(255)
            .HasColumnName("name");
    });

    modelBuilder.Entity<Wallet>(entity =>
    {
        entity.HasKey(e => e.walletId).HasName("PRIMARY");

        entity.ToTable("wallet");

        entity.HasIndex(e => e.userId, "wallet_user_id_foreign");

        entity.Property(e => e.walletId).HasColumnName("wallet_id");
        entity.Property(e => e.balance).HasColumnName("balance");
        entity.Property(e => e.userId).HasColumnName("user_id");

        entity.HasOne(d => d.user).WithMany(p => p.wallets)
            .HasForeignKey(d => d.userId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("wallet_user_id_foreign");
    });

    modelBuilder.Entity<Contest>(entity =>
    {
        entity.HasKey(e => e.contestId).HasName("PRIMARY");

        entity.ToTable("contest");

        entity.Property(e => e.contestId).HasColumnName("contest_id");
        entity.Property(e => e.contestName).HasColumnName("contest_name");
        entity.Property(e => e.startDateTime).HasColumnName("start_date_time");
        entity.Property(e => e.endDateTime).HasColumnName("end_date_time");
    });

    modelBuilder.Entity<ContestParticipant>(entity =>
    {
        entity.HasKey(e => new { e.contestId, e.userId }).HasName("PRIMARY");

        entity.ToTable("contest_participants");

        entity.Property(e => e.contestId).HasColumnName("contest_id");
        entity.Property(e => e.userId).HasColumnName("user_id");
        entity.Property(e => e.initialBalance).HasColumnName("initial_balance");
        entity.Property(e => e.finalBalance).HasColumnName("final_balance");

        entity.HasOne(d => d.contest)
            .WithMany(p => p.contestParticipants)
            .HasForeignKey(d => d.contestId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("contest_participants_contest_id_foreign");

        entity.HasOne(d => d.user)
            .WithMany(p => p.contestParticipants)
            .HasForeignKey(d => d.userId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("contest_participants_user_id_foreign");
    });

    OnModelCreatingPartial(modelBuilder);
}


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
