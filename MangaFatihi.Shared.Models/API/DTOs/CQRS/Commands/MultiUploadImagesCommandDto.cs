using MangaFatihi.Shared.Models.API.Commons.SeriesEpisodes;

namespace MangaFatihi.Shared.Models.API.DTOs.CQRS.Commands;

public class MultiUploadSeriesEpisodeImagesCommandDto
{
    public List<UploadSeriesEpisodeModel> List { get; set; } = new();

}
