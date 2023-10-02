using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClientlyWorkerService.Data;

public partial class ClientyDbContext : DbContext
{
    public ClientyDbContext()
    {
    }

    public ClientyDbContext(DbContextOptions<ClientyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientsAddOn> ClientsAddOns { get; set; }

    public virtual DbSet<CustomSubscription> CustomSubscriptions { get; set; }

    public virtual DbSet<LaborVatMonthly> LaborVatMonthlies { get; set; }

    public virtual DbSet<LaborVatMonthlyMaster> LaborVatMonthlyMasters { get; set; }

    public virtual DbSet<LaborVatOption> LaborVatOptions { get; set; }

    public virtual DbSet<MasterPrimeTask> MasterPrimeTasks { get; set; }

    public virtual DbSet<PaymentGateway> PaymentGateways { get; set; }

    public virtual DbSet<PaymentHistory> PaymentHistories { get; set; }

    public virtual DbSet<Payroll> Payrolls { get; set; }

    public virtual DbSet<PayrollMaster> PayrollMasters { get; set; }

    public virtual DbSet<PrimeTask> PrimeTasks { get; set; }

    public virtual DbSet<PrimeTaskBook> PrimeTaskBooks { get; set; }

    public virtual DbSet<Subscriber> Subscribers { get; set; }

    public virtual DbSet<SubscriptionType> SubscriptionTypes { get; set; }

    public virtual DbSet<TaskBook> TaskBooks { get; set; }

    public virtual DbSet<TestResult> TestResults { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

    public virtual DbSet<VatLaborOption> VatLaborOptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=HRIDOYPC\\SQL2K19EXP;Initial Catalog=ClientyDb;User Id=sa;Password=1234;Connect Timeout=0;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Id).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.Property(e => e.RoleId).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Id).HasMaxLength(50);
            entity.Property(e => e.ConcurrencyStamp).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Discriminator).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EntryBy).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.ImageName).HasMaxLength(100);
            entity.Property(e => e.InstanceId).HasMaxLength(50);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(100);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.SecurityStamp).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.Instance).WithMany(p => p.AspNetUsers)
                .HasForeignKey(d => d.InstanceId)
                .HasConstraintName("FK_AspNetUsers_Subscribers");
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.Property(e => e.UserId).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.UserId).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserRole>(entity =>
        {
            entity.HasNoKey();

            entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

            entity.Property(e => e.RoleId).HasMaxLength(50);
            entity.Property(e => e.UserId).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany().HasForeignKey(d => d.RoleId);

            entity.HasOne(d => d.User).WithMany().HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.LoginProvider).HasMaxLength(450);
            entity.Property(e => e.Name).HasMaxLength(450);
            entity.Property(e => e.UserId).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany().HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK_Client");

            entity.Property(e => e.ClientId).HasMaxLength(50);
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.AnnualReportType).HasMaxLength(50);
            entity.Property(e => e.ClientName).HasMaxLength(100);
            entity.Property(e => e.ClientType).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Cvr).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.EntryBy).HasMaxLength(50);
            entity.Property(e => e.FinancialYear).HasMaxLength(50);
            entity.Property(e => e.FinancialYearStartDate).HasMaxLength(50);
            entity.Property(e => e.InstanceId).HasMaxLength(50);
            entity.Property(e => e.LaborVatType).HasMaxLength(50);
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.PayrollType).HasMaxLength(50);
            entity.Property(e => e.ReelOwner).HasMaxLength(100);
            entity.Property(e => e.TaxStatementType).HasMaxLength(50);
            entity.Property(e => e.Telephone).HasMaxLength(50);

            entity.HasOne(d => d.Instance).WithMany(p => p.Clients)
                .HasForeignKey(d => d.InstanceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Clients_Subscribers");
        });

        modelBuilder.Entity<ClientsAddOn>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<CustomSubscription>(entity =>
        {
            entity.HasKey(e => e.CustomSubId);

            entity.ToTable("CustomSubscription");

            entity.Property(e => e.CustomSubId).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.Frequency).HasMaxLength(20);
            entity.Property(e => e.InstanceId).HasMaxLength(50);
        });

        modelBuilder.Entity<LaborVatMonthly>(entity =>
        {
            entity.ToTable("LaborVatMonthly");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateDate).HasMaxLength(50);
            entity.Property(e => e.Deadline).HasMaxLength(50);
            entity.Property(e => e.FromDate).HasMaxLength(50);
            entity.Property(e => e.InstanceId).HasMaxLength(50);
            entity.Property(e => e.TaskType).HasMaxLength(50);
            entity.Property(e => e.TillDate).HasMaxLength(50);

            entity.HasOne(d => d.Instance).WithMany(p => p.LaborVatMonthlies)
                .HasForeignKey(d => d.InstanceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_LaborVatMonthly_Subscribers");
        });

        modelBuilder.Entity<LaborVatMonthlyMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("LaborVatMonthlyMaster");

            entity.Property(e => e.CreateDate).HasMaxLength(50);
            entity.Property(e => e.Deadline).HasMaxLength(50);
            entity.Property(e => e.FromDate).HasMaxLength(50);
            entity.Property(e => e.TaskType).HasMaxLength(50);
            entity.Property(e => e.TillDate).HasMaxLength(50);
        });

        modelBuilder.Entity<LaborVatOption>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.LaborId).HasMaxLength(50);
            entity.Property(e => e.OptionsDk).HasMaxLength(50);
            entity.Property(e => e.OptionsEn).HasMaxLength(50);
        });

        modelBuilder.Entity<MasterPrimeTask>(entity =>
        {
            entity.HasKey(e => e.PrimeTaskId).HasName("PK_PrimeTasksMaster");

            entity.Property(e => e.Deadline).HasMaxLength(50);
            entity.Property(e => e.PrimeTaskName).HasMaxLength(50);
            entity.Property(e => e.TaskCreationDate).HasMaxLength(50);
        });

        modelBuilder.Entity<PaymentGateway>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PaymentGateway");

            entity.Property(e => e.LiveCheckoutKey).HasMaxLength(50);
            entity.Property(e => e.LiveSecretKey).HasMaxLength(50);
            entity.Property(e => e.TestCheckoutKey).HasMaxLength(50);
            entity.Property(e => e.TestSecretKey).HasMaxLength(50);
        });

        modelBuilder.Entity<PaymentHistory>(entity =>
        {
            entity.ToTable("PaymentHistory");

            entity.Property(e => e.InstanceId).HasMaxLength(50);
            entity.Property(e => e.PackageName).HasMaxLength(50);
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentId).HasMaxLength(50);
            entity.Property(e => e.SubscriptionId).HasMaxLength(50);

            entity.HasOne(d => d.Instance).WithMany(p => p.PaymentHistories)
                .HasForeignKey(d => d.InstanceId)
                .HasConstraintName("FK_PaymentHistory_Subscribers");
        });

        modelBuilder.Entity<Payroll>(entity =>
        {
            entity.ToTable("Payroll");

            entity.Property(e => e.CreateDate).HasMaxLength(50);
            entity.Property(e => e.Deadline).HasMaxLength(50);
            entity.Property(e => e.InstanceId).HasMaxLength(50);
            entity.Property(e => e.TaskType).HasMaxLength(50);

            entity.HasOne(d => d.Instance).WithMany(p => p.Payrolls)
                .HasForeignKey(d => d.InstanceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Payroll_Subscribers");
        });

        modelBuilder.Entity<PayrollMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PayrollMaster");

            entity.Property(e => e.CreateDate).HasMaxLength(50);
            entity.Property(e => e.Deadline).HasMaxLength(50);
            entity.Property(e => e.TaskType).HasMaxLength(50);
        });

        modelBuilder.Entity<PrimeTask>(entity =>
        {
            entity.HasKey(e => e.PrimeTaskId).HasName("PK_PrimeTask");

            entity.Property(e => e.Deadline).HasMaxLength(50);
            entity.Property(e => e.InstanceId).HasMaxLength(50);
            entity.Property(e => e.PrimeTaskName).HasMaxLength(50);
            entity.Property(e => e.TaskCreationDate).HasMaxLength(50);
        });

        modelBuilder.Entity<PrimeTaskBook>(entity =>
        {
            entity.HasKey(e => e.TaskBookId);

            entity.ToTable("PrimeTaskBook");

            entity.Property(e => e.Assignee).HasMaxLength(50);
            entity.Property(e => e.AtchTitle).HasMaxLength(50);
            entity.Property(e => e.ClientId).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Deadline).HasColumnType("datetime");
            entity.Property(e => e.FileUrl).HasMaxLength(150);
            entity.Property(e => e.InstanceId).HasMaxLength(50);
            entity.Property(e => e.Notes).HasMaxLength(4000);
            entity.Property(e => e.TaskName).HasMaxLength(50);
            entity.Property(e => e.TaskStatus).HasMaxLength(50);
            entity.Property(e => e.TaskType).HasMaxLength(50);

            entity.HasOne(d => d.Client).WithMany(p => p.PrimeTaskBooks)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_PrimeTaskBook_Clients");
        });

        modelBuilder.Entity<Subscriber>(entity =>
        {
            entity.HasKey(e => e.InstanceId);

            entity.Property(e => e.InstanceId).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Cvr).HasMaxLength(50);
            entity.Property(e => e.ExpireDate).HasColumnType("datetime");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.SubscriptionId).HasMaxLength(50);
        });

        modelBuilder.Entity<SubscriptionType>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId)
                .HasName("PK_AdSubscription")
                .IsClustered(false);

            entity.Property(e => e.SubscriptionId).HasMaxLength(50);
            entity.Property(e => e.PackageName).HasMaxLength(50);
        });

        modelBuilder.Entity<TaskBook>(entity =>
        {
            entity.HasKey(e => e.TaskId)
                .HasName("PK_TaskBook")
                .IsClustered(false);

            entity.Property(e => e.AtchTitle).HasMaxLength(50);
            entity.Property(e => e.ClientId).HasMaxLength(50);
            entity.Property(e => e.DeadLine).HasColumnType("datetime");
            entity.Property(e => e.FileUrl).HasMaxLength(150);
            entity.Property(e => e.InstanceId).HasMaxLength(50);
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TaskName).HasMaxLength(50);
            entity.Property(e => e.UserId).HasMaxLength(50);
        });

        modelBuilder.Entity<TestResult>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TestResult");

            entity.Property(e => e.Result).HasMaxLength(4000);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ManagerId).HasMaxLength(50);
            entity.Property(e => e.UserAssignId).HasMaxLength(50);
            entity.Property(e => e.UserId).HasMaxLength(50);
        });

        modelBuilder.Entity<UserRefreshToken>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserRefreshToken");

            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.RefreshToken).HasMaxLength(500);
            entity.Property(e => e.UserId).HasMaxLength(50);
            entity.Property(e => e.UserRefreshTokenId).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserRefreshToken_AspNetUsers");
        });

        modelBuilder.Entity<VatLaborOption>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.OptionsDk).HasMaxLength(20);
            entity.Property(e => e.OptionsEn).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
