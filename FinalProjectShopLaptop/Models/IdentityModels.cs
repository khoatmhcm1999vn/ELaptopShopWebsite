using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FinalProjectShopLaptop.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
       
        //[Required]
        //[MaxLength(50)]
        public string FullName { get; set; }
        public string Address { get; set; }
        public Nullable<bool> Gender { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }
        //public string PostalCode { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("name=QuanLyLaptop", throwIfV1Schema: false)
        {

        }

        public virtual DbSet<NhomSanPham> NhomSanPhams { get; set; }
        public virtual DbSet<Hang> Hangs { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<FavouriteSanPham> FavouriteSanPhams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NhomSanPham>()
                .HasKey(a => a.Id)
                .Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<NhomSanPham>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.NhomSanPham)
                .HasForeignKey(e => e.NhomSanPhamId);
            modelBuilder.Entity<NhomSanPham>()
                .Property(a => a.Ten)
                .IsRequired();

            modelBuilder.Entity<Hang>()
               .HasKey(a => a.Id)
               .Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Hang>()
              .HasMany(e => e.SanPhams)
              .WithRequired(e => e.Hang)
              .HasForeignKey(e => e.HangId);
            modelBuilder.Entity<Hang>()
                .Property(a => a.Ten)
                .IsRequired();

            modelBuilder.Entity<SanPham>()
               .HasKey(a => a.Id)
               .Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<SanPham>()
                .Property(a => a.Ten)
                .IsRequired();

            modelBuilder.Entity<SanPham>()
              .Property(e => e.Price)
              .HasPrecision(18, 0);

            modelBuilder.Entity<Order>()
               .Property(e => e.ShipMobile)
               .IsUnicode(false);

            modelBuilder.Entity<OrderDetail>()
              .Property(e => e.Price)
              .HasPrecision(18, 0);

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}