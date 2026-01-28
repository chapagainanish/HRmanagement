using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Token == token, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<RefreshToken>> GetActiveTokensByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(r => r.UserId == userId && r.RevokedAt == null && r.ExpiresAt > DateTime.UtcNow)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task RevokeAllUserTokensAsync(int userId, CancellationToken cancellationToken = default)
        {
            var tokens = await _dbSet
                .Where(r => r.UserId == userId && r.RevokedAt == null)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            foreach (var token in tokens)
            {
                token.RevokedAt = DateTime.UtcNow;
            }
        }
    }
}