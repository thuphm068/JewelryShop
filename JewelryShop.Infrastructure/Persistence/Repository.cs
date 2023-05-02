﻿using JewelryShop.Domain.Repository;
using JewelryShop.Infrastructure.Entity_Framework_Core;
using Microsoft.EntityFrameworkCore;

namespace JewelryShop.Infrastructure.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly JewelryShopDBContext _context;
        protected Repository(JewelryShopDBContext context)
        {
            _context = context;
        }

        public void Delete(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteById(Guid id)
        {
            try
            {
                var deleteRecord = await GetById(id);
                if (deleteRecord == null) return false;
                _context.Set<TEntity>().Remove(deleteRecord);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public async Task<List<TEntity>> GetAllList()
        {
            try
            {
                return await _context.Set<TEntity>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return _context.Set<TEntity>().AsQueryable<TEntity>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TEntity?> GetById(Guid id)
        {
            try
            {
                return await _context.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Insert(TEntity entity)
        {
            try
            {
                var result = await _context.Set<TEntity>().AddAsync(entity);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TEntity Update(TEntity entity)
        {
            try
            {
                var updateEntity = _context.Set<TEntity>().Update(entity);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
