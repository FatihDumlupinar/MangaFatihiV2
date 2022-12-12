using FluentValidation;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Base;
using Mediator;

namespace MangaFatihi.Models.Bindings.CQRS.Commands;

public class DeleteSeriesEpisodeCommand: ICommand<DataResult<object>>
{
    public string SeriesEpisodeId { get; set; } = "";
}

public class DeleteSeriesEpisodeCommandValidator : AbstractValidator<DeleteSeriesEpisodeCommand>
{
    public DeleteSeriesEpisodeCommandValidator()
    {
        RuleFor(x => x.SeriesEpisodeId)
            .Must(x => Guid.TryParse(x, out _)).WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "SeriesEpisodeId"))
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesEpisodeId"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesEpisodeId"));

    }
}
