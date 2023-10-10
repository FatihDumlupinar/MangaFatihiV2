using FluentValidation;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Constants;
using Mediator;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Commands;

public class DeleteSeriesArtistCommand : ICommand<DataResult<object>>
{
    public string SeriesArtistId { get; set; } = "";
}

public class DeleteSeriesArtistCommandValidator : AbstractValidator<DeleteSeriesArtistCommand>
{
    public DeleteSeriesArtistCommandValidator()
    {
        RuleFor(x => x.SeriesArtistId)
            .Must(x => Guid.TryParse(x, out _)).WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "SeriesArtistId"))
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesArtistId"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesArtistId"));

    }
}
