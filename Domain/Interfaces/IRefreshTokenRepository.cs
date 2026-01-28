using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
        Task<IEnumerable<RefreshToken>> GetActiveTokensByUserIdAsync(int userId, CancellationToken cancellationToken = default);
        Task RevokeAllUserTokensAsync(int userId, CancellationToken cancellationToken = default);
    }
}