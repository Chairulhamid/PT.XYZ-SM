using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {


        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<UsersAccount> UsersAccounts { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One to one => Account to Employee
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(b => b.Employee)
                .HasForeignKey<Account>(b => b.NIK);

            modelBuilder.Entity<User>()
               .HasOne(a => a.UsersAccount)
               .WithOne(b => b.User)
               .HasForeignKey<UsersAccount>(b => b.Email);

            modelBuilder.Entity<AccountRole>()
                 .HasKey(bc => new { bc.NIK, bc.Role_Id });
            modelBuilder.Entity<AccountRole>()
                .HasOne(bc => bc.Account)
                .WithMany(b => b.AccountRoles)
                .HasForeignKey(bc => bc.NIK);
            modelBuilder.Entity<AccountRole>()
                .HasOne(bc => bc.Role)
                .WithMany(c => c.AccountRoles)
                .HasForeignKey(bc => bc.Role_Id);

            modelBuilder.Entity<UserRole>()
                .HasKey(bc => new { bc.Email, bc.Role_Id });
            modelBuilder.Entity<UserRole>()
                .HasOne(bc => bc.UsersAccount)
                .WithMany(b => b.UserRoles)
                .HasForeignKey(bc => bc.Email);
            modelBuilder.Entity<UserRole>()
                .HasOne(bc => bc.Role)
                .WithMany(c => c.UserRoles)
                .HasForeignKey(bc => bc.Role_Id);
             
            modelBuilder.Entity<User>()
                .HasMany(e => e.Projects)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.Email)
                .IsRequired(false);



        }
    }

}
