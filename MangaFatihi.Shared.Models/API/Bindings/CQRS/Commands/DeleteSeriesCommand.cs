using FluentValidation;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Constants;
using Mediator;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Commands;

public class DeleteSeriesCommand : ICommand<DataResult<object>>
{
    public string SeriesId { get; set; } = "";
}

public class DeleteSeriesCommandValidator : AbstractValidator<DeleteSeriesCommand>
{
    public DeleteSeriesCommandValidator()
    {
        RuleFor(x => x.SeriesId)
            .Must(x => Guid.TryParse(x, out _)).WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "SeriesId"))
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesId"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesId"));

    }
}

