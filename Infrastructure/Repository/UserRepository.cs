using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
namespace Infrastructure.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email, cancellationToken).ConfigureAwait(false);
        }

        public async Task<bool> IsEmailTakenAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().AnyAsync(u => u.Email == email, cancellationToken).ConfigureAwait(false);
        }
    }
}
