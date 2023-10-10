namespace MangaFatihi.Shared.Models.API.Commons.SeriesEpisodes
{
    public class UploadSeriesEpisodeModel
    {
        public Guid PageImageId { get; set; }
        public string PageImageUrl { get; set; } = "";
        public int PageNo { get; set; }

    }
}
