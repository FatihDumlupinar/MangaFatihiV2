using FluentValidation;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Base;
using Mediator;

namespace MangaFatihi.Models.Bindings.CQRS.Commands;

public class DeleteSeriesCategoryCommand : ICommand<DataResult<object>>
{
    public string SeriesCategoryId { get; set; } = "";
}

public class DeleteSeriesCategoryCommandValidator : AbstractValidator<DeleteSeriesCategoryCommand>
{
    public DeleteSeriesCategoryCommandValidator()
    {
        RuleFor(x => x.SeriesCategoryId)
            .Must(x => Guid.TryParse(x, out _)).WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "SeriesArtistId"))
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesCategoryId"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesCategoryId"));

    }
}
