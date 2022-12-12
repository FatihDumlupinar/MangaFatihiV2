using FluentValidation;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Domain.Enms;
using MangaFatihi.Models.Base;
using Mediator;

namespace MangaFatihi.Models.Bindings.CQRS.Commands;

public class EditSeriesEpisodeCommand : ICommand<DataResult<object>>
{
    /// <summary>
    /// Bölümün Enique Id'si
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// Bölüm numarası
    /// </summary>
    public int EpisodeNo { get; set; } = 1;

    /// <summary>
    /// Bölüm başlığı
    /// </summary>
    public string? Title { get; set; } = "";

    /// <summary>
    /// Bölüm için not
    /// </summary>
    public string? Note { get; set; } = "";

    /// <summary>
    /// Yayında olacak mı
    /// </summary>
    public bool IsOnAir { get; set; } = true;

    /// <summary>
    /// Ekip
    /// </summary>
    public Guid? TeamId { get; set; }

    /// <summary>
    /// Editor
    /// </summary>
    public Guid? EditorUserId { get; set; }

    /// <summary>
    /// Çevirmen
    /// </summary>
    public Guid? TranslatorUserId { get; set; }

    /// <summary>
    /// Bölümün bağlı olduğu serinin Unique Id'si
    /// </summary>
    public Guid SeriesId { get; set; }

    /// <summary>
    /// Bölümün Türünün 
    /// </summary>
    public StaticSeriesEpisodeTypeEnm SeriesEpisodeType { get; set; } = StaticSeriesEpisodeTypeEnm.image;

}

public class EditSeriesEpisodeCommandValidator : AbstractValidator<EditSeriesEpisodeCommand>
{
    public EditSeriesEpisodeCommandValidator()
    {
        RuleFor(x => x.SeriesEpisodeType)
           .IsInEnum()
           .WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "SeriesEpisodeType"))
           .WithErrorCode(ApplicationMessages.ErrorDefaultTypeError)
           .NotNull()
           .WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesEpisodeType"))
           .WithErrorCode(ApplicationMessages.ErrorDefaultIsNull)
           .NotEmpty()
           .WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesEpisodeType"))
           .WithErrorCode(ApplicationMessages.ErrorDefaultIsNull);

        RuleFor(x => x.Id)
           .Must(x => Guid.TryParse(x, out _)).WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "SeriesEpisodeId"))
           .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesEpisodeId"))
           .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesEpisodeId"));

        RuleFor(x => x.EpisodeNo)
            .NotEqual(0).WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "EpisodeNo"))
            .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "EpisodeNo"))
            .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "EpisodeNo"));

        RuleFor(x => x.SeriesId)
           .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesId"))
           .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesId"));
    }
}
