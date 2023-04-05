using FluentValidation;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.Commonns;
using Mediator;

namespace MangaFatihi.Models.Bindings.CQRS.Commands;

public class UpdateOneNovelPageCommand : ICommand<DataResult<object>>
{
    /// <summary>
    /// Seri bölümün unique id'si
    /// </summary>
    public Guid SeriesEpisodeId { get; set; }

    /// <summary>
    /// Sayfası
    /// </summary>
    public UpdateNovelPageListModel NovelPage { get; set; }
}
public class UpdateOneNovelPageCommandValidator : AbstractValidator<UpdateOneNovelPageCommand>
{
    public UpdateOneNovelPageCommandValidator()
    {
        RuleFor(x => x.NovelPage)
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "NovelPage"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "NovelPage"));

    }
}
