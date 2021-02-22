using hamituslukan.PaymentSystem.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hamituslukan.PaymentSystem.Data.Concrete
{
    public class PaymentSystemContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<SubscriberType> SubscriberTypes { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }

        public PaymentSystemContext(DbContextOptions<PaymentSystemContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity => entity.ToTable("User"));
            builder.Entity<ApplicationRole>(entity => entity.ToTable("Role"));
            builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable("UserRole"));
            builder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("UserClaims"); });
            builder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("UserLogins"); });
            builder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("UserTokens"); });
            builder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("RoleClaims"); });

            builder.Entity<Deposit>().Property(x => x.Amount).HasColumnType("decimal(18,2)");
            builder.Entity<Invoice>().Property(x => x.Amount).HasColumnType("decimal(18,2)");

            string adminRoleId = System.Guid.NewGuid().ToString();

            builder.Entity<ApplicationRole>().HasData(new ApplicationRole { Id = adminRoleId, Name = "Admin", NormalizedName = "Admin".ToUpper(new System.Globalization.CultureInfo("en-US")) });
            builder.Entity<ApplicationRole>().HasData(new ApplicationRole { Name = "User", NormalizedName = "User".ToUpper(new System.Globalization.CultureInfo("en-US")) });

            string adminUserId = System.Guid.NewGuid().ToString();
            var applicationUser = new ApplicationUser { Id = adminUserId, Email = "admin@admin.com", Name = "Administrator", UserName = "admin@admin.com" };
            applicationUser.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(applicationUser, "admin");
            applicationUser.NormalizedUserName = applicationUser.UserName.ToUpper(new System.Globalization.CultureInfo("en-US"));
            applicationUser.NormalizedEmail = applicationUser.Email.ToUpper(new System.Globalization.CultureInfo("en-US"));

            builder.Entity<ApplicationUser>().HasData(applicationUser);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { RoleId = adminRoleId, UserId = adminUserId });

            builder.Entity<SubscriberType>().HasData(new SubscriberType { Id = System.Guid.NewGuid(), Name = "Bireysel Müşteri", IdentityLength = 11 });
            builder.Entity<SubscriberType>().HasData(new SubscriberType { Id = System.Guid.NewGuid(), Name = "Tüzel Müşteri", IdentityLength = 10 });
        }
    }
}