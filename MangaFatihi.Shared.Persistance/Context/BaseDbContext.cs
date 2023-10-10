using MangaFatihi.Domain.Entities;
using MangaFatihi.Shared.Domain.Common;
using MangaFatihi.Shared.Domain.Entities.SeriesArtists;
using MangaFatihi.Shared.Domain.Entities.SeriesAuthors;
using MangaFatihi.Shared.Domain.Entities.SeriesCategories;
using MangaFatihi.Shared.Domain.Entities.SeriesEnty;
using MangaFatihi.Shared.Domain.Entities.SeriesEpisodes;
using MangaFatihi.Shared.Domain.Entities.Statics;
using MangaFatihi.Shared.Domain.Entities.Teams;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MangaFatihi.Shared.Persistance.Context
{
    public class BaseDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BaseDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            Guid userId = Guid.Empty;
            var userIdClaim = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userIdClaim))
            {
                _ = Guid.TryParse(userIdClaim, out userId);
            }

            ChangeTracker.DetectChanges();
            var added = ChangeTracker.Entries()
            .Where(t => t.State == EntityState.Added)
            .Select(t => t.Entity)
            .ToArray();

            foreach (var entity in added)
            {
                if (entity is BaseEntity track)
                {
                    track.CreateDate = DateTime.Now;
                    track.IsActive = true;
                    if (!string.IsNullOrEmpty(userIdClaim))
                    {
                        track.CreateUserId = userId;
                    }
                }
            }

            var modified = ChangeTracker.Entries()
            .Where(t => t.State == EntityState.Modified)
            .Select(t => t.Entity)
            .ToArray();

            foreach (var entity in modified)
            {
                if (entity is BaseEntity track)
                {
                    track.UpdateDate = DateTime.Now;
                    if (!string.IsNullOrEmpty(userIdClaim))
                    {
                        track.UpdateUserId = userId;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Series> Series { get; set; }
        public DbSet<SeriesAndSeriesArtist> SeriesAndSeriesArtists { get; set; }
        public DbSet<SeriesAndSeriesAuthor> SeriesAndSeriesAuthors { get; set; }
        public DbSet<SeriesAndSeriesCategory> SeriesAndSeriesCategories { get; set; }
        public DbSet<SeriesArtist> SeriesArtists { get; set; }
        public DbSet<SeriesAuthor> SeriesAuthors { get; set; }
        public DbSet<SeriesCategory> SeriesCategories { get; set; }
        public DbSet<SeriesEpisode> SeriesEpisodes { get; set; }
        public DbSet<SeriesEpisodesPage> SeriesEpisodesPages { get; set; }
        public DbSet<StaticSeriesEpisodeType> StaticSeriesEpisodeTypes { get; set; }
        public DbSet<StaticSeriesStatus> StaticSeriesStatus { get; set; }
        public DbSet<StaticSeriesType> StaticSeriesTypes { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamAndAppUser> TeamsAndAppUsers { get; set; }



    }
}
