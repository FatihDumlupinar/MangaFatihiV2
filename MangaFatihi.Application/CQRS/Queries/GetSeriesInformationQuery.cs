using FluentValidation;
using MangaFatihi.Application.Constants;
using MangaFatihi.Application.Models.Base;
using MangaFatihi.Application.Models.DTOs.Queries;
using MediatR;

namespace MangaFatihi.Application.CQRS.Queries
{
    public class GetSeriesInformationQuery : IRequest<DataResult<GetSeriesInformationQueryDto>>
    {
        /// <summary>
        /// Serinin unique id'si
        /// </summary>
        public string SeriesId { get; set; } = "";
    }

    public class GetSeriesInformationQueryValidator : AbstractValidator<GetSeriesInformationQuery>
    {
        public GetSeriesInformationQueryValidator()
        {
            RuleFor(x => x.SeriesId)
                .Must(x => Guid.TryParse(x, out _)).WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "SeriesId"))
                .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesId"))
                .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesId"));

        }
    }

}
