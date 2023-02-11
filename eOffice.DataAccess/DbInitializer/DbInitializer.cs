using eOffice.DataAccess.Data;
using eOffice.Models;
using eOffice.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOffice.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }

            if (!_roleManager.RoleExistsAsync(SD.SuperAdminRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.SuperAdminRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.AdminRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.CommandingOfficerRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.SecondInCommandRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.OfficerRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.ClerkRole)).GetAwaiter().GetResult();

                _roleManager.CreateAsync(new IdentityRole(SD.HeadClerkRole)).GetAwaiter().GetResult();

                //super admin
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "hariomahlawat@619",
                    Email = "hariomahlawat@619",
                    EmailConfirmed = true,
                    Rank = "Lt Col",
                    PersonalNumber = "333",
                    FirstName = "Hari Om",
                    LastName = "Ahlawat"
                }, "Admin123*").GetAwaiter().GetResult();
                //admin
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@161",
                    Email = "admin@161",
                    EmailConfirmed = true,
                    Rank = "Test",
                    PersonalNumber = "1",
                    FirstName = "Admin",
                    LastName = "161"
                }, "Admin123*").GetAwaiter().GetResult();
                //Cdr
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "cdr@161",
                    Email = "cdr@161",
                    EmailConfirmed = true,
                    Rank = "Brig",
                    PersonalNumber = "1",
                    FirstName = "Test",
                    LastName = "test"
                }, "Masha123*").GetAwaiter().GetResult();
                //Dy Cdr
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "dy_cdr@161",
                    Email = "dy_cdr@161",
                    EmailConfirmed = true,
                    Rank = "Colonel",
                    PersonalNumber = "1",
                    FirstName = "Test",
                    LastName = "test"
                }, "Masha123*").GetAwaiter().GetResult();
                //GSO1
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "gso1@161",
                    Email = "gso1@161",
                    EmailConfirmed = true,
                    Rank = "Lt Colonel",
                    PersonalNumber = "1",
                    FirstName = "Test",
                    LastName = "test"
                }, "Masha123*").GetAwaiter().GetResult();
                //GSO2
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "gso2@161",
                    Email = "gso2@161",
                    EmailConfirmed = true,
                    Rank = "Major",
                    PersonalNumber = "1",
                    FirstName = "Test",
                    LastName = "test"
                }, "Masha123*").GetAwaiter().GetResult();
                //AQ
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "aq@161",
                    Email = "aq@161",
                    EmailConfirmed = true,
                    Rank = "Lt Colonel",
                    PersonalNumber = "1",
                    FirstName = "Test",
                    LastName = "test"
                }, "Masha123*").GetAwaiter().GetResult();
                //AAG
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "aag@161",
                    Email = "aag@161",
                    EmailConfirmed = true,
                    Rank = "Lt Colonel",
                    PersonalNumber = "1",
                    FirstName = "Test",
                    LastName = "test"
                }, "Masha123*").GetAwaiter().GetResult();
                //Edn Officer
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "edn_offr@161",
                    Email = "edn_offr@161",
                    EmailConfirmed = true,
                    Rank = "Lt Colonel",
                    PersonalNumber = "1",
                    FirstName = "Test",
                    LastName = "test"
                }, "Masha123*").GetAwaiter().GetResult();
                //Adm Comdt
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "adm_comdt@161",
                    Email = "adm_comdt@161",
                    EmailConfirmed = true,
                    Rank = "Colonel",
                    PersonalNumber = "1",
                    FirstName = "Test",
                    LastName = "test"
                }, "Masha123*").GetAwaiter().GetResult();
                //SSO
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "sso@161",
                    Email = "sso@161",
                    EmailConfirmed = true,
                    Rank = "Colonel",
                    PersonalNumber = "1",
                    FirstName = "Test",
                    LastName = "test"
                }, "Masha123*").GetAwaiter().GetResult();


                //uploading clerk - gbranch
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "uploading_clerk@gbr",
                    Email = "uploading_clerk@gbr",
                    EmailConfirmed = true,
                    Rank = "Sep",
                    PersonalNumber = "1",
                    FirstName = "G",
                    LastName = "abc"
                }, "Masha123*").GetAwaiter().GetResult();
                //uploading clerk - abranch
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "uploading_clerk@abr",
                    Email = "uploading_clerk@abr",
                    EmailConfirmed = true,
                    Rank = "Sep",
                    PersonalNumber = "1",
                    FirstName = "A",
                    LastName = "abc"
                }, "Masha123*").GetAwaiter().GetResult();
                //uploading clerk - qbranch
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "uploading_clerk@qbr",
                    Email = "uploading_clerk@qbr",
                    EmailConfirmed = true,
                    Rank = "Sep",
                    PersonalNumber = "1",
                    FirstName = "Q",
                    LastName = "abc"
                }, "Masha123*").GetAwaiter().GetResult();


                ApplicationUser user = _db.ApplicationUser.FirstOrDefault(u => u.Email == "admin@161");
                _userManager.AddToRoleAsync(user, SD.AdminRole).GetAwaiter().GetResult();

                user = _db.ApplicationUser.FirstOrDefault(u => u.Email == "hariomahlawat@619");
                _userManager.AddToRoleAsync(user, SD.SuperAdminRole).GetAwaiter().GetResult();

                user = _db.ApplicationUser.FirstOrDefault(u => u.Email == "cdr@161");
                _userManager.AddToRoleAsync(user, SD.CommandingOfficerRole).GetAwaiter().GetResult();

                user = _db.ApplicationUser.FirstOrDefault(u => u.Email == "dy_cdr@161");
                _userManager.AddToRoleAsync(user, SD.SecondInCommandRole).GetAwaiter().GetResult();

                user = _db.ApplicationUser.FirstOrDefault(u => u.Email == "gso1@161");
                _userManager.AddToRoleAsync(user, SD.OfficerRole).GetAwaiter().GetResult();

                user = _db.ApplicationUser.FirstOrDefault(u => u.Email == "gso2@161");
                _userManager.AddToRoleAsync(user, SD.OfficerRole).GetAwaiter().GetResult();

                user = _db.ApplicationUser.FirstOrDefault(u => u.Email == "aq@161");
                _userManager.AddToRoleAsync(user, SD.OfficerRole).GetAwaiter().GetResult();

                user = _db.ApplicationUser.FirstOrDefault(u => u.Email == "aag@161");
                _userManager.AddToRoleAsync(user, SD.OfficerRole).GetAwaiter().GetResult();

                user = _db.ApplicationUser.FirstOrDefault(u => u.Email == "edn_offr@161");
                _userManager.AddToRoleAsync(user, SD.OfficerRole).GetAwaiter().GetResult();

                user = _db.ApplicationUser.FirstOrDefault(u => u.Email == "adm_comdt@161");
                _userManager.AddToRoleAsync(user, SD.OfficerRole).GetAwaiter().GetResult();

                user = _db.ApplicationUser.FirstOrDefault(u => u.Email == "sso@161");
                _userManager.AddToRoleAsync(user, SD.OfficerRole).GetAwaiter().GetResult();





                user = _db.ApplicationUser.FirstOrDefault(u => u.Email == "uploading_clerk@gbr");
                _userManager.AddToRoleAsync(user, SD.ClerkRole).GetAwaiter().GetResult();

                user = _db.ApplicationUser.FirstOrDefault(u => u.Email == "uploading_clerk@abr");
                _userManager.AddToRoleAsync(user, SD.ClerkRole).GetAwaiter().GetResult();

                user = _db.ApplicationUser.FirstOrDefault(u => u.Email == "uploading_clerk@qbr");
                _userManager.AddToRoleAsync(user, SD.ClerkRole).GetAwaiter().GetResult();



            }
            return;
        }
    }
}
