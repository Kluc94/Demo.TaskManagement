﻿using Demo.TaskManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demo.TaskManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Account>()
                .HasMany(a => a.Users)
                .WithOne(u => u.Account)
                .HasForeignKey(u => u.AccountId);
        }

        //public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
