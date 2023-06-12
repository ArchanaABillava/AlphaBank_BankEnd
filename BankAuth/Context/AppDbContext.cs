using BankAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankAuth.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<UserReg> UserRegs { get; set; }
        public DbSet<CustomerAccountInfo> AccInfo { get; set; }
        public DbSet<Interest> LoanInterest { get; set; }
        public DbSet<LoanDetails> LoanDetails { get; set; }

        public DbSet<Document> Documents { get; set; }
     
        

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<UserReg>()

                .ToTable("user_cred");

            modelBuilder.Entity<CustomerAccountInfo>()

                .ToTable("customer_accountinfo");
            
            modelBuilder.Entity<Interest>()

                .ToTable("loan_interest");


            modelBuilder.Entity<LoanDetails>().ToTable("loan_details");

            modelBuilder.Entity<Document>().ToTable("loan_documents");




        }


    }
}






