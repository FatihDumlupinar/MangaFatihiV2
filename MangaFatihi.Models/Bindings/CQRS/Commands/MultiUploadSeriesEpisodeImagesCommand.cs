using MangaFatihi.Models.Base;
using MangaFatihi.Models.DTOs.CQRS.Commands;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace MangaFatihi.Models.Bindings.CQRS.Commands;

public class MultiUploadSeriesEpisodeImagesCommand : ICommand<DataResult<MultiUploadSeriesEpisodeImagesCommandDto>>
{
    public List<IFormFile> Files { get; set; } = new();
    public Guid SeriesEpisodeId { get; set; }
}
