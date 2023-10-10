using FluentValidation;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Constants;
using Mediator;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Commands;

public class DeleteOnePageCommand : ICommand<DataResult<object>>
{
    public string SeriesPageId { get; set; } = "";
}

public class DeleteOnePageCommandValidator : AbstractValidator<DeleteOnePageCommand>
{
    public DeleteOnePageCommandValidator()
    {
        RuleFor(x => x.SeriesPageId)
            .Must(x => Guid.TryParse(x, out _)).WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "SeriesPageId"))
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesPageId"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesPageId"));

    }
}
