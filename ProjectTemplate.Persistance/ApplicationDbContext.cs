using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProjectTemplate.Core;
using ProjectTemplate.Core.Exceptions;
using ProjectTemplate.Core.Resources;
using ProjectTemplate.Domain.Models;

namespace ProjectTemplate.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string>
    {

        
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public void EnsureSeeding()
        {
            SeedRoles();
            SeedDefaultUser();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            var connectionString = AppSettings.Configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

            builder.Entity<User>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            

        }

        private void SeedRoles()
        {

            var separtor = Path.DirectorySeparatorChar;

            var rolesFilePath = $"Config{separtor}Seeds{separtor}roles.json";

            if (File.Exists(rolesFilePath))
            {
                var rolesData = File.ReadAllText(rolesFilePath);

                var roles = JsonConvert.DeserializeObject<List<Role>>(rolesData);

                foreach (var item in roles)
                {
                    var found = Roles.Find(item.Id);

                    if (found != null)
                    {
                        found = found.Update(item);
                        Roles.Update(found);
                    }
                    else
                        Roles.Add(item);
                }


                SaveChanges();
            }
        }

        private void SeedDefaultUser()
        {
            var id = "E428D8D6-4C5E-4D33-9437-569195B3B80A".ToLower();
            var email = "user@admin.com";
            var username = "Admin";
            var fullName = "مدير النظام";
            var password = "1122";

            var user = Users.FirstOrDefault(a => a.Id == id);

            if (user == null)
            {
                user = new User()
                {
                    Id = "E428D8D6-4C5E-4D33-9437-569195B3B80A".ToLower(),
                    UserName = username,
                    Email = email,
                    NormalizedEmail = email.ToUpper(),
                    NormalizedUserName = username.ToUpper(),
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    LockoutEnabled = true,
                    FullName = fullName
                };

                var hashedPassword = new PasswordHasher<User>().HashPassword(user, password);
                user.PasswordHash = hashedPassword;
                Users.Add(user);
            }
            else
            {
                var hashedPassword = new PasswordHasher<User>().HashPassword(user, password);

                user.UserName = username;
                user.Email = email;
                user.NormalizedEmail = email.ToUpper();
                user.NormalizedUserName = username.ToUpper();
                user.PasswordHash = hashedPassword;
                user.LockoutEnabled = true;
                user.FullName = fullName;
            }

            SaveChanges();

            var foundRole = UserRoles.FirstOrDefault(a => a.UserId == user.Id && a.RoleId == AppRoles.ADMIN);

            if (foundRole == null)
            {
                var role = new IdentityUserRole<string>();
                role.UserId = user.Id;
                role.RoleId = AppRoles.ADMIN;
                UserRoles.Add(role);
            }

            SaveChanges();
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex) when (ex.InnerException is SqlException)
            {
                var sqlException = ex.InnerException as SqlException;

                if (sqlException.Number == 547)
                    throw new BusinessException(MessageText.YouCannotDeleteTheItemBecauseItIsLinkedWithOtherItems);
            }

            return -1;
        }
    }
}