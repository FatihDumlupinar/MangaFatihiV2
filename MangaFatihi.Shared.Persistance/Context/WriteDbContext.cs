using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MangaFatihi.Shared.Persistance.Context
{
    public class WriteDbContext : BaseDbContext
    {
        public WriteDbContext(DbContextOptions<WriteDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options, httpContextAccessor)
        {
        }
    }
}
