using FluentValidation;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.Commonns;
using Mediator;

namespace MangaFatihi.Models.Bindings.CQRS.Commands;

public class CreateMultiNovelPageCommand : ICommand<DataResult<object>>
{
    /// <summary>
    /// Seri bölümün unique id'si
    /// </summary>
    public Guid SeriesEpisodeId { get; set; }

    /// <summary>
    /// Sayfalar
    /// </summary>
    public List<CreateNovelPageListModel> NovelPages { get; set; } = new();

}
public class CreateMultiNovelPageCommandValidator : AbstractValidator<CreateMultiNovelPageCommand>
{
    public CreateMultiNovelPageCommandValidator()
    {
        RuleFor(x => x.NovelPages)
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "NovelPages"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "NovelPages"))
            .Must(i => i == null || !i.Any()).WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "NovelPages"));

    }
}
