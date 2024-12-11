using ASAPTask.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Infrastructure.Seeding
{
    public class AppInitializer
    {
        private readonly ASAPTaskContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<AppInitializer> _logger;
        public AppInitializer(ASAPTaskContext context, UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager, ILogger<AppInitializer> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }
        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }
        public async Task TrySeedAsync()
        {
            if (!_roleManager.Roles.Any())
            {
                // role
                await _roleManager.CreateAsync(new ApplicationRole() { Name = "Admin" });
            }

            if (!_userManager.Users.Any())
            {
                //  user
                var defaultUser = new ApplicationUser
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com",
                    EmailConfirmed = true,
                };
                if (_userManager.Users.All(u => u.Id != defaultUser.Id))
                {
                    var user = await _userManager.FindByEmailAsync(defaultUser.Email);
                    if (user == null)
                    {
                        await _userManager.CreateAsync(defaultUser, "P@ssw0rd");
                        await _userManager.AddToRoleAsync(defaultUser, "Admin");
                    }
                }
            }
        }
    }
}
