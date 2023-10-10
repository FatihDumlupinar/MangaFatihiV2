using FluentValidation;
using MangaFatihi.Shared.Models.API.Commons.SeriesEpisodePages;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Constants;
using Mediator;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Commands;

public class CreateOneNovelPageCommand : ICommand<DataResult<object>>
{
    /// <summary>
    /// Seri bölümün unique id'si
    /// </summary>
    public Guid SeriesEpisodeId { get; set; } = Guid.Empty;

    /// <summary>
    /// Sayfası
    /// </summary>
    public CreateNovelPageListModel NovelPage { get; set; } = new();
}

public class CreateOneNovelPageCommandValidator : AbstractValidator<CreateOneNovelPageCommand>
{
    public CreateOneNovelPageCommandValidator()
    {
        RuleFor(x => x.NovelPage)
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "NovelPage"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "NovelPage"));

    }
}
