using EventPlanning.DataAccess.Interfaces;
using EventPlanning.Domain.Models;

namespace EventPlanning.DataAccess.Repositories
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(DatabaseContext context)
            : base(context)
        {

        }
    }
}
