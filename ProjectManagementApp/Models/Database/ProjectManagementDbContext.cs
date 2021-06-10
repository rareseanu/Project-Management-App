using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Database
{
    public class ProjectManagementDbContext : IdentityDbContext<UserEntity, RoleEntity, int, IdentityUserClaim<int>, UserRoleEntity,
        IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ProjectManagementDbContext(DbContextOptions<ProjectManagementDbContext> options) : base(options)
        {
        }
        public DbSet<ItemEntity> Items { get; set; }
        public DbSet<ItemListEntity> ItemLists { get; set; }
        public DbSet<BoardEntity> Boards { get; set; }
        public DbSet<BoardUserEntity> BoardUsers { get; set; }
        public DbSet<CheckItemEntity> CheckItems { get; set; }
        public DbSet<CheckListEntity> CheckLists { get; set; }
        public DbSet<CommentaryEntity> Commentaries { get; set; }
        public DbSet<LabelEntity> Labels { get; set; }
        public DbSet<ItemLabelEntity> ItemLabels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemEntity>().HasQueryFilter(p => p.IsDeleted == false);
            modelBuilder.Entity<ItemListEntity>().HasQueryFilter(p => p.IsDeleted == false);
            modelBuilder.Entity<BoardEntity>().HasQueryFilter(p => p.IsDeleted == false);
            modelBuilder.Entity<BoardUserEntity>().HasQueryFilter(p => p.BoardEntity.IsDeleted == false);
            modelBuilder.Entity<CheckItemEntity>().HasQueryFilter(p => p.IsDeleted == false);
            modelBuilder.Entity<CheckListEntity>().HasQueryFilter(p => p.IsDeleted == false);
            modelBuilder.Entity<CommentaryEntity>().HasQueryFilter(p => p.IsDeleted == false);
            modelBuilder.Entity<LabelEntity>().HasQueryFilter(p => p.IsDeleted == false);
            modelBuilder.Entity<ItemLabelEntity>().HasQueryFilter(p => p.LabelEntity.IsDeleted == false);
            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            modelBuilder.Entity<RoleEntity>()
                .HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        }
    }
}
