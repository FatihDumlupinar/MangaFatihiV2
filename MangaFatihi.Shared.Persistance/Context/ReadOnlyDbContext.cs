using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MangaFatihi.Shared.Persistance.Context
{
    public class ReadOnlyDbContext : BaseDbContext
    {
        public ReadOnlyDbContext(DbContextOptions<ReadOnlyDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options, httpContextAccessor)
        {
        }
    }
}
