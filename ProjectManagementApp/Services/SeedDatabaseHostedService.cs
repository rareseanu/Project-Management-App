using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectManagementApp.Models.Database.Entities;
using ProjectManagementApp.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementApp.Services
{
    public class SeedDatabaseHostedService : IHostedService, IDisposable
    {
        private Timer timer;
        private IServiceScopeFactory Services { get; }

        public SeedDatabaseHostedService(IServiceScopeFactory services)
        {
            Services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(async _ => await SeedDatabase(),
                null, TimeSpan.Zero, TimeSpan.FromDays(7));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return null;
        }

        public void Dispose()
        {
        }

        private async Task SeedDatabase()
        {
            using var scope = Services.CreateScope();
            await SeedRoles(scope);
        }

        private async Task SeedRoles(IServiceScope scope)
        {
            using var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();
            var rolesList = Enum.GetNames<RolesEnum>().ToList();          

            foreach (var role in rolesList)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new RoleEntity
                    {
                        Name = role
                    });
                }
            }
        }
    }
}
