using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MangaFatihi.Persistence.Context
{
    public class WriteDbContext : BaseDbContext
    {
        public WriteDbContext(DbContextOptions<WriteDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options, httpContextAccessor)
        {
        }
    }
}
