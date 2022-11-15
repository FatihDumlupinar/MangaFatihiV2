using Microsoft.EntityFrameworkCore;

namespace MangaFatihi.Persistence.Context
{
    public class WriteDbContext : BaseDbContext
    {
        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {
        }
    }
}
