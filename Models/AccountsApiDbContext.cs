using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AccountMS.Models
{
    public partial class AccountsApiDbContext : DbContext
    {
        public AccountsApiDbContext()
        {
        }

        public AccountsApiDbContext(DbContextOptions<AccountsApiDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountStatement> AccountStatements { get; set; }
        public virtual DbSet<AccountStatus> AccountStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:bank-app-team2.database.windows.net,1433;Initial Catalog=dbAccount;Persist Security Info=False;User ID=team2;Password=Kanini2021;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountType)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AccountStatement>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__AccountS__55433A6BE53439D3");

                entity.ToTable("AccountStatement");

                entity.Property(e => e.Descriptions)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionDate).HasColumnType("date");

                entity.Property(e => e.TransactionStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ValueDate).HasColumnType("date");
            });

            modelBuilder.Entity<AccountStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__AccountS__C8EE20636EBB6082");

                entity.ToTable("AccountStatus");

                entity.Property(e => e.AccountCreationStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
