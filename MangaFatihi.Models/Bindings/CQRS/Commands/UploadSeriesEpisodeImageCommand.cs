using FluentValidation;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.DTOs.CQRS.Commands;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace MangaFatihi.Models.Bindings.CQRS.Commands;

public class UploadSeriesEpisodeImageCommand : ICommand<DataResult<UploadSeriesEpisodeImageCommandDto>>
{
    /// <summary>
    /// Seri bölümün unique id'si
    /// </summary>
    public Guid SeriesEpisodeId { get; set; }

    /// <summary>
    /// resim dosyası
    /// </summary>
    public IFormFile File { get; set; }

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