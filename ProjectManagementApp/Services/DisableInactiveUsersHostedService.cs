using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectManagementApp.Models.Database.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementApp.Services
{
    public class DisableInactiveUsersHostedService : IHostedService, IDisposable
    {
        private Timer timer;
        private IServiceScopeFactory Services { get; }

        public DisableInactiveUsersHostedService(IServiceScopeFactory services)
        {
            Services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(async _ => await UpdateUsers(),
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

        private async Task UpdateUsers()
        {
            using var scope = Services.CreateScope();
            await UpdateInactiveUsers(scope);
        }

        private async Task UpdateInactiveUsers(IServiceScope scope)
        {
            using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
            foreach(var user in userManager.Users)
            {
                if(user.RefreshTokenExpires.HasValue)
                {
                    var date = user.RefreshTokenExpires.Value;
                    if(DateTime.Now >= date.AddDays(10))
                    {
                        user.IsActive = false;
                        await userManager.UpdateAsync(user);
                    }
                }
            }

        }
    }
}
