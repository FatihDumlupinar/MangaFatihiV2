using FluentValidation;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Constants;
using Mediator;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Commands;

public class DeleteSeriesCategoryCommand : ICommand<DataResult<object>>
{
    public string SeriesCategoryId { get; set; } = "";
}

public class DeleteSeriesCategoryCommandValidator : AbstractValidator<DeleteSeriesCategoryCommand>
{
    public DeleteSeriesCategoryCommandValidator()
    {
        RuleFor(x => x.SeriesCategoryId)
            .Must(x => Guid.TryParse(x, out _)).WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "SeriesCategoryId"))
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesCategoryId"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesCategoryId"));

    }
}
