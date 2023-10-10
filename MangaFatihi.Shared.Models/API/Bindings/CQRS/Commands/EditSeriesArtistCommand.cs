using FluentValidation;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Constants;
using Mediator;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Commands;

public class EditSeriesArtistCommand : ICommand<DataResult<object>>
{
    /// <summary>
    /// Seri sanatçısının unique id'si
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// Seri sanatçısının adı ve soyadı
    /// </summary>
    public string FullName { get; set; } = "";

    /// <summary>
    /// İlişkilendirilmek istenen serilerin unique id'si
    /// </summary>
    public List<Guid>? SeriesIds { get; set; } = new();
}

public class EditSeriesArtistCommandValidator : AbstractValidator<EditSeriesArtistCommand>
{
    public EditSeriesArtistCommandValidator()
    {
        RuleFor(x => x.Id)
           .Must(x => Guid.TryParse(x, out _)).WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "Id"))
           .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "Id"))
           .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "Id"));

        RuleFor(x => x.FullName)
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "FullName"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "FullName"));

    }
}
