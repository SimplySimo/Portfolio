﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MelbourneData.Areas.MoulaCodeChalleng.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected IDbSet<T> DbSet;

        public Repository(Models.IDbContext dataContext)
        {
            DbSet = dataContext.Set<T>();
        }

        public void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

    }
}