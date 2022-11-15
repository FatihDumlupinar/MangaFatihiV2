using MangaFatihi.Domain.Entities;
using MangaFatihi.Domain.Entities.Identity;

namespace MangaFatihi.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<Series> Series { get; }
        public IGenericRepository<SeriesArtist> SeriesArtist { get; }
        public IGenericRepository<SeriesAuthor> SeriesAuthor { get; }
        public IGenericRepository<SeriesCategory> SeriesCategory { get; }
        public IGenericRepository<SeriesEpisode> SeriesEpisode { get; }
        public IGenericRepository<SeriesEpisodesPage> SeriesEpisodesPage { get; }
        public IGenericRepository<Team> Team { get; }
        public IGenericRepository<RefreshToken> RefreshToken { get; }

        Task<int> CommitAsync(CancellationToken cancellationToken = default);

    }
}
