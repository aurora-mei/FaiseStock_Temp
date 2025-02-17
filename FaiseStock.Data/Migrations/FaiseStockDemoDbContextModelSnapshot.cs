﻿// <auto-generated />
using System;
using FaiseStock.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FaiseStock.Data.Migrations
{
    [DbContext(typeof(FaiseStockDemoDbContext))]
    partial class FaiseStockDemoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");
            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("FaiseStock.Data.Models.Contest", b =>
                {
                    b.Property<string>("contestId")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("contest_id");

                    b.Property<string>("contestName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("contest_name");

                    b.Property<DateTime>("endDateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("end_date_time");

                    b.Property<DateTime>("startDateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("start_date_time");

                    b.HasKey("contestId")
                        .HasName("PRIMARY");

                    b.ToTable("contest", (string)null);
                });

            modelBuilder.Entity("FaiseStock.Data.Models.ContestParticipant", b =>
                {
                    b.Property<string>("contestId")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("contest_id");

                    b.Property<string>("userId")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("user_id");

                    b.Property<double?>("finalBalance")
                        .HasColumnType("double")
                        .HasColumnName("final_balance");

                    b.Property<double>("initialBalance")
                        .HasColumnType("double")
                        .HasColumnName("initial_balance");

                    b.HasKey("contestId", "userId")
                        .HasName("PRIMARY");

                    b.HasIndex("userId");

                    b.ToTable("contest_participants", (string)null);
                });

            modelBuilder.Entity("FaiseStock.Data.Models.DepositHistory", b =>
                {
                    b.Property<string>("depositId")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("deposit_id");

                    b.Property<double>("amount")
                        .HasColumnType("double")
                        .HasColumnName("amount");

                    b.Property<DateTime>("createAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_at");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("user_id");

                    b.HasKey("depositId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "userId" }, "deposit_history_user_id_foreign");

                    b.ToTable("deposit_history", (string)null);
                });

            modelBuilder.Entity("FaiseStock.Data.Models.TopUser", b =>
                {
                    b.Property<string>("userId")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("user_id");

                    b.Property<DateTime>("createAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_at");

                    b.Property<string>("contestId")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("contest_id");

                    b.Property<double>("increasedAmount")
                        .HasColumnType("double")
                        .HasColumnName("increased_amount");

                    b.Property<int>("rank")
                        .HasColumnType("int")
                        .HasColumnName("rank");

                    b.Property<double>("roic")
                        .HasColumnType("double")
                        .HasColumnName("ROIC");

                    b.HasKey("userId", "createAt")
                        .HasName("PRIMARY");

                    b.HasIndex("contestId");

                    b.ToTable("top_user", (string)null);
                });

            modelBuilder.Entity("FaiseStock.Data.Models.User", b =>
                {
                    b.Property<string>("userId")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("user_id");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.HasKey("userId")
                        .HasName("PRIMARY");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("FaiseStock.Data.Models.Wallet", b =>
                {
                    b.Property<string>("walletId")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("wallet_id");

                    b.Property<double>("balance")
                        .HasColumnType("double")
                        .HasColumnName("balance");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("user_id");

                    b.HasKey("walletId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "userId" }, "wallet_user_id_foreign");

                    b.ToTable("wallet", (string)null);
                });

            modelBuilder.Entity("FaiseStock.Data.Models.ContestParticipant", b =>
                {
                    b.HasOne("FaiseStock.Data.Models.Contest", "contest")
                        .WithMany("contestParticipants")
                        .HasForeignKey("contestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("contest_participants_contest_id_foreign");

                    b.HasOne("FaiseStock.Data.Models.User", "user")
                        .WithMany("contestParticipants")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("contest_participants_user_id_foreign");

                    b.Navigation("contest");

                    b.Navigation("user");
                });

            modelBuilder.Entity("FaiseStock.Data.Models.DepositHistory", b =>
                {
                    b.HasOne("FaiseStock.Data.Models.User", "user")
                        .WithMany("depositHistories")
                        .HasForeignKey("userId")
                        .IsRequired()
                        .HasConstraintName("deposit_history_user_id_foreign");

                    b.Navigation("user");
                });

            modelBuilder.Entity("FaiseStock.Data.Models.TopUser", b =>
                {
                    b.HasOne("FaiseStock.Data.Models.Contest", "contest")
                        .WithMany("topUsers")
                        .HasForeignKey("contestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("top_user_contest_id_foreign");

                    b.HasOne("FaiseStock.Data.Models.User", "user")
                        .WithMany("topUsers")
                        .HasForeignKey("userId")
                        .IsRequired()
                        .HasConstraintName("top_user_user_id_foreign");

                    b.Navigation("contest");

                    b.Navigation("user");
                });

            modelBuilder.Entity("FaiseStock.Data.Models.Wallet", b =>
                {
                    b.HasOne("FaiseStock.Data.Models.User", "user")
                        .WithMany("wallets")
                        .HasForeignKey("userId")
                        .IsRequired()
                        .HasConstraintName("wallet_user_id_foreign");

                    b.Navigation("user");
                });

            modelBuilder.Entity("FaiseStock.Data.Models.Contest", b =>
                {
                    b.Navigation("contestParticipants");

                    b.Navigation("topUsers");
                });

            modelBuilder.Entity("FaiseStock.Data.Models.User", b =>
                {
                    b.Navigation("contestParticipants");

                    b.Navigation("depositHistories");

                    b.Navigation("topUsers");

                    b.Navigation("wallets");
                });
#pragma warning restore 612, 618
        }
    }
}
