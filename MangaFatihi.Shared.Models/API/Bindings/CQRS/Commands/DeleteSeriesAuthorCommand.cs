using FluentValidation;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Constants;
using Mediator;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Commands;

public class DeleteSeriesAuthorCommand : ICommand<DataResult<object>>
{
    public string SeriesAuthorId { get; set; } = "";
}

public class DeleteSeriesAuthorCommandValidator : AbstractValidator<DeleteSeriesAuthorCommand>
{
    public DeleteSeriesAuthorCommandValidator()
    {
        RuleFor(x => x.SeriesAuthorId)
            .Must(x => Guid.TryParse(x, out _)).WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "SeriesArtistId"))
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesAuthorId"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesAuthorId"));

    }
}
