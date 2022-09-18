using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Models
{
    public partial class WalletAppContext : DbContext
    {
        public WalletAppContext()
        {
        }

        public WalletAppContext(DbContextOptions<WalletAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<Merchant> Merchant { get; set; }
        public virtual DbSet<MerchantServiceMapping> MerchantServiceMapping { get; set; }
        public virtual DbSet<MerchantServiceType> MerchantServiceType { get; set; }
        public virtual DbSet<MerchantTransaction> MerchantTransaction { get; set; }
        public virtual DbSet<Otp> Otp { get; set; }
        public virtual DbSet<Otppurpose> Otppurpose { get; set; }
        public virtual DbSet<PaymentType> PaymentType { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserCard> UserCard { get; set; }
        public virtual DbSet<UserTransaction> UserTransaction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("Connection");
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bank>(entity =>
            {
                entity.Property(e => e.BankId).ValueGeneratedOnAdd();

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Merchant>(entity =>
            {
                entity.HasIndex(e => e.EmailId)
                    .HasName("uk_Merchant_EmailId")
                    .IsUnique();

                entity.Property(e => e.CreatedTimestamp).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MerchantServiceMapping>(entity =>
            {
                entity.HasKey(e => e.MerchantServiceMappingId)
                    .HasName("pk_MerchantServicesMapping_MerchantServiceMMapping")
                    .IsClustered(false);

                entity.HasIndex(e => new { e.EmailId, e.ServiceId })
                    .HasName("uk_MerchantServicesMapping_EmailIdServicesId")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.CreatedTimestamp).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DiscountPercent)
                    .HasColumnType("decimal(5, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Email)
                    .WithMany(p => p.MerchantServiceMapping)
                    .HasPrincipalKey(p => p.EmailId)
                    .HasForeignKey(d => d.EmailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MerchantServicesMapping_EmailId");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.MerchantServiceMapping)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MerchantServicesMapping_ServiceId");
            });

            modelBuilder.Entity<MerchantServiceType>(entity =>
            {
                entity.HasKey(e => e.ServiceId)
                    .HasName("pk_MerchantServiceType_ServiceId");

                entity.HasIndex(e => e.ServiceType)
                    .HasName("uk_MerchantServiceType_ServiceType")
                    .IsUnique();

                entity.Property(e => e.ServiceId).ValueGeneratedOnAdd();

                entity.Property(e => e.ServiceType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MerchantTransaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("pk_MerchantTransaction_TransactionId");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Info)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Email)
                    .WithMany(p => p.MerchantTransaction)
                    .HasPrincipalKey(p => p.EmailId)
                    .HasForeignKey(d => d.EmailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MerchantTransaction_EmailId");

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.MerchantTransaction)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MerchantTransaction_PaymentTypeId");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.MerchantTransaction)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MerchantTransaction_Status");
            });

            modelBuilder.Entity<Otp>(entity =>
            {
                entity.ToTable("OTP");

                entity.HasIndex(e => new { e.Otpvalue, e.ReferenceId, e.ExpiryDateTime })
                    .HasName("pk_OTP_OTPReferenceIdExpiryDateTime")
                    .IsUnique();

                entity.Property(e => e.Otpid).HasColumnName("OTPId");

                entity.Property(e => e.ExpiryDateTime).HasColumnType("datetime");

                entity.Property(e => e.IsValid).HasDefaultValueSql("((1))");

                entity.Property(e => e.OtppurposeId).HasColumnName("OTPPurposeId");

                entity.Property(e => e.Otpvalue)
                    .IsRequired()
                    .HasColumnName("OTPValue")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Otppurpose)
                    .WithMany(p => p.Otp)
                    .HasForeignKey(d => d.OtppurposeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_OTP_OTPPurposeId");
            });

            modelBuilder.Entity<Otppurpose>(entity =>
            {
                entity.ToTable("OTPPurpose");

                entity.HasIndex(e => e.Otppurpose1)
                    .HasName("uk_OTPPurpose_OTPPurpose")
                    .IsUnique();

                entity.Property(e => e.OtppurposeId)
                    .HasColumnName("OTPPurposeId")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Otppurpose1)
                    .IsRequired()
                    .HasColumnName("OTPPurpose")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.Property(e => e.PaymentTypeId).ValueGeneratedOnAdd();

                entity.Property(e => e.PaymentFrom)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PaymentTo)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PaymentType1).HasColumnName("PaymentType");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.StatusId).ValueGeneratedOnAdd();

                entity.Property(e => e.StatusValue)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.EmailId)
                    .HasName("uk_User_EmailID")
                    .IsUnique();

                entity.HasIndex(e => e.MobileNumber)
                    .HasName("uk_User_MobileNumber")
                    .IsUnique();

                entity.Property(e => e.CreatedTimestamp).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasColumnName("EmailID")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("chk_User_Status");
            });

            modelBuilder.Entity<UserCard>(entity =>
            {
                entity.HasKey(e => e.UserCardMappingId)
                    .HasName("pk_UserCardMappingId");

                entity.HasIndex(e => new { e.EmailId, e.CardNumber })
                    .HasName("pk_UserCard_EmailId_CardNumber")
                    .IsUnique();

                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTimestamp).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiryDate).HasColumnType("smalldatetime");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.UserCard)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_UserCard_BankId");

                entity.HasOne(d => d.Email)
                    .WithMany(p => p.UserCard)
                    .HasPrincipalKey(p => p.EmailId)
                    .HasForeignKey(d => d.EmailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_UserCard_EmailId");
            });

            modelBuilder.Entity<UserTransaction>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Info)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PointsEarned).HasDefaultValueSql("((0))");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Email)
                    .WithMany(p => p.UserTransaction)
                    .HasPrincipalKey(p => p.EmailId)
                    .HasForeignKey(d => d.EmailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_UserTransaction_EmailId");

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.UserTransaction)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_UserTransaction_PaymentTypeId");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.UserTransaction)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_UserTransaction_StatusId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
