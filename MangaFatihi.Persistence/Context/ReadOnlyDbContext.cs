using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MangaFatihi.Persistence.Context
{
    public class ReadOnlyDbContext : BaseDbContext
    {
        public ReadOnlyDbContext(DbContextOptions<ReadOnlyDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options, httpContextAccessor)
        {
        }
    }
}
