using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
namespace Infrastructure.Repository
{
    /// <summary>
    /// Generic repository for basic CRUD operations
    /// </summary>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        /// <summary>
        /// Get all records
        /// </summary>
        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get all records (non-cancellable, interface implementation)
        /// </summary>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await GetAllAsync(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Get record by ID
        /// </summary>
        public virtual async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(new object[] { id }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get record by ID (non-cancellable, interface implementation)
        /// </summary>
        public async Task<T?> GetByIdAsync(int id)
        {
            return await GetByIdAsync(id, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Add new record
        /// </summary>
        public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Add new record (non-cancellable, interface implementation)
        /// </summary>
        public async Task AddAsync(T entity)
        {
            await AddAsync(entity, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Update existing record
        /// </summary>
        public virtual void Update(T entity) => _dbSet.Update(entity);

        /// <summary>
        /// Remove record
        /// </summary>
        public virtual void Remove(T entity) => _dbSet.Remove(entity);
    }
}
