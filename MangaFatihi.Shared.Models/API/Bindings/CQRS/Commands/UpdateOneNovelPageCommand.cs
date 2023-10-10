using FluentValidation;
using MangaFatihi.Shared.Models.API.Commons.SeriesEpisodePages;
using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Models.DataResults;
using Mediator;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Commands;

public class UpdateOneNovelPageCommand : ICommand<DataResult<object>>
{
    /// <summary>
    /// Seri bölümün unique id'si
    /// </summary>
    public Guid SeriesEpisodeId { get; set; } = Guid.Empty;

    /// <summary>
    /// Sayfası
    /// </summary>
    public UpdateNovelPageListModel NovelPage { get; set; } = null!;
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
