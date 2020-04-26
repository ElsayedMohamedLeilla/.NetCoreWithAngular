using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.Models;

namespace Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.Data
{
    public class UsersContext : DbContext
    {
        public UsersContext (DbContextOptions<UsersContext> options)
            : base(options)
        {
        }

        public DbSet<Net_Elsayed_Mohammed_Elsayed_Ali_Leilla.Models.Table_Users> Table_Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table_Users>().HasData(new Table_Users
            {
                UserId = 1,
                Name = "Test Elsayed Mohammed",
                LastName = "TestLeilla",
                Email = "elsayed.mohammed70@gmail.com",
                IsEmailConfirmed = false,
                Password = "123",
                PhoneNumber = "01069879123",
                Username = "TestElsyed_Leilla",
                EmailConfirmationToken = DateTime.Now.AddDays(DateTime.Now.Millisecond).ToString().GetHashCode().ToString("x"),
                AddedDate = DateTime.Now,

            }, new Table_Users
            {
                UserId = 2,
                Name = "Test Elsayed Ali",
                LastName = "TestLeilla",
                Email = "elsayed.mohammed70@gmail.com",
                IsEmailConfirmed = false,
                Password = "1321",
                PhoneNumber = "01076566765",
                Username = "TestElsyed_Ali",
                EmailConfirmationToken = DateTime.Now.AddDays(DateTime.Now.Millisecond).ToString().GetHashCode().ToString("x"),
                AddedDate = DateTime.Now,
            });
        }
    }
}
