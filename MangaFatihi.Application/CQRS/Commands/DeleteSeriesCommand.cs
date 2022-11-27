using FluentValidation;
using MangaFatihi.Application.Constants;
using MangaFatihi.Application.Models.Base;
using MangaFatihi.Application.Models.DTOs.Commands;
using MediatR;

namespace MangaFatihi.Application.CQRS.Commands
{
    public class DeleteSeriesCommand : IRequest<DataResult<DeleteSeriesCommandDto>>
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
