using MangaFatihi.Domain.Entities;
using MangaFatihi.Domain.Entities.Identity;
using MangaFatihi.Domain.Interfaces;
using MangaFatihi.Persistence.Context;

namespace MangaFatihi.Application.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Ctor&Fields

        protected readonly WriteDbContext _writeDbContext;
        protected readonly ReadOnlyDbContext _readOnlyDbContext;

        private readonly IGenericRepository<Series> _series;
        private readonly IGenericRepository<SeriesArtist> _seriesArtist;
        private readonly IGenericRepository<SeriesAuthor> _seriesAuthor;
        private readonly IGenericRepository<SeriesCategory> _seriesCategory;
        private readonly IGenericRepository<SeriesEpisode> _seriesEpisode;
        private readonly IGenericRepository<SeriesEpisodesPage> _seriesEpisodesPage;
        private readonly IGenericRepository<Team> _team;
        private readonly IGenericRepository<RefreshToken> _refreshToken;

        public UnitOfWork(WriteDbContext writeDbContext, ReadOnlyDbContext readOnlyDbContext, IGenericRepository<Series> series, IGenericRepository<SeriesArtist> seriesArtist, IGenericRepository<SeriesAuthor> seriesAuthor, IGenericRepository<SeriesCategory> seriesCategory, IGenericRepository<SeriesEpisode> seriesEpisode, IGenericRepository<SeriesEpisodesPage> seriesEpisodesPage, IGenericRepository<Team> team, IGenericRepository<RefreshToken> refreshToken)
        {
            _writeDbContext = writeDbContext;
            _readOnlyDbContext = readOnlyDbContext;
            _series = series;
            _seriesArtist = seriesArtist;
            _seriesAuthor = seriesAuthor;
            _seriesCategory = seriesCategory;
            _seriesEpisode = seriesEpisode;
            _seriesEpisodesPage = seriesEpisodesPage;
            _team = team;
            _refreshToken = refreshToken;
        }


        #endregion

        #region Properties

        public IGenericRepository<Series> Series => _series;
        public IGenericRepository<SeriesArtist> SeriesArtist => _seriesArtist;
        public IGenericRepository<SeriesAuthor> SeriesAuthor => _seriesAuthor;
        public IGenericRepository<SeriesCategory> SeriesCategory => _seriesCategory;
        public IGenericRepository<SeriesEpisode> SeriesEpisode => _seriesEpisode;
        public IGenericRepository<SeriesEpisodesPage> SeriesEpisodesPage => _seriesEpisodesPage;
        public IGenericRepository<Team> Team => _team;
        public IGenericRepository<RefreshToken> RefreshToken => _refreshToken;



        #endregion

        /// <summary>
        /// Veritabanı için olan değişiklikleri veritabanına gönderir.
        /// </summary>
        /// <returns>Kaç verinin etkilendiğini geriye döndürür</returns>
        public Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return _writeDbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Veritabanı için oluşturulmuş DbContext'leri Dispose eder. (Dispose = kullanılan kaynağı serbest bırakır.)
        /// </summary>
        public void Dispose()
        {
            _writeDbContext.Dispose();
            _readOnlyDbContext.Dispose();
        }
    }
}
