using FluentValidation;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Base;
using Mediator;

namespace MangaFatihi.Models.Bindings.CQRS.Commands;

public class EditSeriesCategoryCommand : ICommand<DataResult<object>>
{
    /// <summary>
    /// Seri kategorisinin unique id'si
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// Seri kategorisinin adı 
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// İlişkilendirilmek istenen serilerin unique id'si
    /// </summary>
    public List<Guid>? SeriesIds { get; set; } = new();
}

public class EditSeriesCategoryCommandValidator : AbstractValidator<EditSeriesCategoryCommand>
{
    public EditSeriesCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
           .Must(x => Guid.TryParse(x, out _)).WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "Id"))
           .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "Id"))
           .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "Id"));

        RuleFor(x => x.Name)
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "Name"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "Name"));

    }
}
