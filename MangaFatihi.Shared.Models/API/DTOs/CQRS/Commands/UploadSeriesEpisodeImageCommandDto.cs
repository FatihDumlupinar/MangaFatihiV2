using MangaFatihi.Shared.Models.API.Commons.SeriesEpisodes;

namespace MangaFatihi.Shared.Models.API.DTOs.CQRS.Commands;

public class UploadSeriesEpisodeImageCommandDto
{
    public UploadSeriesEpisodeModel Model { get; set; } = new();
}
