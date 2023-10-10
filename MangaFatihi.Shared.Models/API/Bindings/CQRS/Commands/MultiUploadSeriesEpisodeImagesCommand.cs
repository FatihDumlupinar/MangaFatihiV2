using MangaFatihi.Shared.Models.API.DTOs.CQRS.Commands;
using MangaFatihi.Shared.Models.DataResults;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Commands;

public class MultiUploadSeriesEpisodeImagesCommand : ICommand<DataResult<MultiUploadSeriesEpisodeImagesCommandDto>>
{
    public List<IFormFile> Files { get; set; } = new();
    public Guid SeriesEpisodeId { get; set; }
}
