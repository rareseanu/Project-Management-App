using Microsoft.AspNetCore.Http;
using ProjectManagementApp.Helpers;
using ProjectManagementApp.Models.Database.Entities;
using ProjectManagementApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectManagementApp.Services
{
    public class BaseService<T> where T : BaseEntity
    {
        protected BaseRepository<T> BaseRepository;
        protected ClaimsPrincipal CurrentUser;

        public BaseService(BaseRepository<T> baseRepository,
            IHttpContextAccessor contextAccessor)
        {
            BaseRepository = baseRepository;
            CurrentUser = contextAccessor.HttpContext?.User;
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            return await BaseRepository.Get(predicate);
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            return await BaseRepository.GetAll(predicate);
        }

        public async Task<int> CountAll(Expression<Func<T, bool>> predicate = null)
        {
            return await BaseRepository.CountAll(predicate);
        }

        public async Task Commit()
        {
            await BaseRepository.Commit();
        }

        public async Task<T> Create(T entity, bool commit = true)
        {
            if (CurrentUser != null && CurrentUser.GetUserId() != 0)
                entity.CreatedBy = entity.ModifiedBy = CurrentUser.GetUserId();

            return await BaseRepository.Create(entity, commit);
        }

        public async Task<T> Update(T entity, bool commit = true)
        {
            if (CurrentUser != null && CurrentUser.GetUserId() != 0)
            {
                entity.ModifiedBy = CurrentUser.GetUserId();
                entity.DateModified = DateTime.Now;
            }

            return await BaseRepository.Update(entity, commit);
        }

        public async Task<T> Delete(T entity, bool commit = true)
        {
            return await BaseRepository.Delete(entity, commit);
        }
    }
}
