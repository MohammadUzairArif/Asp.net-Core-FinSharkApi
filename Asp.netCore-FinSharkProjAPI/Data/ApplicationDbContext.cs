using Asp.netCore_FinSharkProjAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Asp.netCore_FinSharkProjAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }



        /* Kya Ho Raha Hai Yahan?
         OnModelCreating Method:
                 Ye method Entity Framework Core ko batata hai ke model(database tables) ko kaise build karna hai.
                 Is method ko override karne ka matlab hai ke aap apne custom configuration de rahe ho jab database ban raha hota hai.*/

        /* base.OnModelCreating(modelBuilder):
                   Ye call zaroori hai takay Identity Framework ki apni configurations bhi apply ho sakein.
                   Agar ye na likho, tu Identity ke default behaviors (like UserClaims, Tokens) kaam nahi karein ge.

 */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            

            modelBuilder.Entity<Portfolio>(x => x.HasKey(p => new
            {
                p.AppUserId, // Portfolio table ka AppUserId ko primary key set karna
                p.StockId   // Portfolio table ka StockId ko primary key set karna
            })); 

            modelBuilder.Entity<Portfolio>()
                .HasOne(p => p.AppUser) // Portfolio ka AppUser ke saath one-to-many relationship
                .WithMany(u => u.Portfolios) // Ek AppUser ke paas multiple Portfolios ho sakte hain
                .HasForeignKey(p => p.AppUserId); // Foreign key set karna

            modelBuilder.Entity<Portfolio>()
                .HasOne(p => p.Stock) // Portfolio ka Stock ke saath one-to-many relationship
                .WithMany(s => s.Portfolios) // Ek Stock ke paas multiple Portfolios ho sakte hain
                .HasForeignKey(p => p.StockId); // Foreign key set karna


            // Seed initial data for roles
            /* Yahan aap 2 roles bana rahe ho:

                 Admin
                 User
                 NormalizedName ko upper-case mein likhna zaroori hota hai (Identity internally compare karne ke liye).

 */
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },    
                new IdentityRole { Name = "User", NormalizedName = "USER" }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles); /* HasData() ka matlab hota hai "Seed Data".
                                                                  Ye Entity Framework ko bolta hai:
                                                                  "Jab migration run hogi to Admin aur User roles automatically database mein daal dena."*/
        } 
    }
}

/* End Result:
Jab aap Update-Database command chalayenge, to ye 2 roles:

Admin

User

AspNetRoles table mein insert ho jaayenge by default. */