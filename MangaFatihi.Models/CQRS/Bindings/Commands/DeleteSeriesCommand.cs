using FluentValidation;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Base;
using Mediator;

namespace MangaFatihi.Models.CQRS.Bindings.Commands
{
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

}
