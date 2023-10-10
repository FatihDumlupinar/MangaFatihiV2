using FluentValidation;
using MangaFatihi.Shared.Models.API.DTOs.CQRS.Commands;
using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Models.DataResults;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Commands;

public class UploadSeriesEpisodeImageCommand : ICommand<DataResult<UploadSeriesEpisodeImageCommandDto>>
{
    /// <summary>
    /// Seri bölümün unique id'si
    /// </summary>
    public Guid SeriesEpisodeId { get; set; } = Guid.Empty;

    /// <summary>
    /// resim dosyası
    /// </summary>
    public IFormFile File { get; set; } = null!;

    /// <summary>
    /// Sayfa Numarası
    /// </summary>
    public int PageNo { get; set; } = 0;

}

public class UploadSeriesEpisodeImageCommandValidator : AbstractValidator<UploadSeriesEpisodeImageCommand>
{
    public UploadSeriesEpisodeImageCommandValidator()
    {
        RuleFor(x => x.File)
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "File"));

        RuleFor(x => x.SeriesEpisodeId)
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesEpisodeId"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesEpisodeId"));

    }
}