using Microsoft.EntityFrameworkCore;

namespace MoneyMe.Repository.Entities;

public partial class MoneyMeDbContext : DbContext
{
    //public MoneyMeDbContext()
    //{
    //}


    public MoneyMeDbContext(DbContextOptions<MoneyMeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerProfile> CustomerProfile { get; set; }

    public virtual DbSet<CustomerProfileLoanApplication> CustomerProfileLoanApplication { get; set; }

    public virtual DbSet<Customers> Customers { get; set; }

    public virtual DbSet<DomainBlacklist> DomainBlacklist { get; set; }

    public virtual DbSet<MobileBlacklist> MobileBlacklist { get; set; }

    public virtual DbSet<NamePrefixes> NamePrefixes { get; set; }

    public virtual DbSet<Products> Products { get; set; }


//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DREWCEL\\SQLEXPRESS;Database=MoneyMeDb;User ID=sa;Password=P@$$.1234;Min Pool Size=10;Max Pool Size=1000;Connection Timeout=30;TrustServerCertificate=True");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerProfile>(entity =>
        {
            entity.Property(e => e.AmountRequired).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateModified).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CustomerProfileLoanApplication>(entity =>
        {
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateModified)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Repayment).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalRepayment).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CustomerProfile).WithMany(p => p.CustomerProfileLoanApplication)
                .HasForeignKey(d => d.CustomerProfileId)
                .HasConstraintName("FK_CustomerProfileLoanApplication_CustomerProfile");

            entity.HasOne(d => d.Product).WithMany(p => p.CustomerProfileLoanApplication)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_CustomerProfileLoanApplication_Products");
        });

        modelBuilder.Entity<Customers>(entity =>
        {
            entity.Property(e => e.AmountRequired).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateModified).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Title).WithMany(p => p.Customers)
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customers_NamePrefixes");
        });

        modelBuilder.Entity<DomainBlacklist>(entity =>
        {
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateModified).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MobileBlacklist>(entity =>
        {
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateModified).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<NamePrefixes>(entity =>
        {
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateModified).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Products>(entity =>
        {
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateModified).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
