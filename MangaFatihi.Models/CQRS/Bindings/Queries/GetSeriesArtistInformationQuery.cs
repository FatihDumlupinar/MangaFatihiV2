using FluentValidation;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.CQRS.DTOs.Queries;
using Mediator;

namespace MangaFatihi.Models.CQRS.Bindings.Queries
{
    public class GetSeriesArtistInformationQuery : IQuery<DataResult<GetSeriesArtistInformationQueryDto>>
    {
        /// <summary>
        /// Seri sanatçısının unique id'si
        /// </summary>
        public string SeriesArtistId { get; set; } = "";
    }

    public class GetSeriesArtistInformationQueryValidator : AbstractValidator<GetSeriesArtistInformationQuery>
    {
        public GetSeriesArtistInformationQueryValidator()
        {
            RuleFor(x => x.SeriesArtistId)
                .Must(x => Guid.TryParse(x, out _)).WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "SeriesArtistId"))
                .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesArtistId"))
                .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesArtistId"));

        }
    }
}
