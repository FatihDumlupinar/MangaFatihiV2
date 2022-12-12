﻿using FluentValidation;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Domain.Enms;
using MangaFatihi.Models.Base;
using Mediator;

namespace MangaFatihi.Models.Bindings.CQRS.Commands;

public class CreateSeriesCommand : ICommand<DataResult<object>>
{
    /// <summary>
    /// Serinin Başlığı
    /// </summary>
    public string Title { get; set; } = "";

    /// <summary>
    /// Serinin alternatif veya farklı dillerdeki başlıkları
    /// </summary>
    public string? TitleAlternative { get; set; }

    /// <summary>
    /// Serinin özet hikayesi
    /// </summary>
    public string? Story { get; set; }

    /// <summary>
    /// Serinin kapak fotoğrafı
    /// </summary>
    public string? ProfileImgUrl { get; set; }

    /// <summary>
    /// Seri ile ilgili not
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Serinin ilk Yayın Tarihi
    /// </summary>
    public DateTime? BroadcastStartDate { get; set; }

    /// <summary>
    /// Siteye eklenme tarihi
    /// </summary>
    public DateTime? StartDateOnPage { get; set; }

    /// <summary>
    /// Güncel mi
    /// </summary>
    public bool IsUpToDate { get; set; } = false;

    /// <summary>
    /// Anasayfadaki Slyder da olacak mı 
    /// </summary>
    public bool IsSlyder { get; set; } = false;

    /// <summary>
    /// Yeni mi
    /// </summary>
    public bool IsNew { get; set; } = false;

    /// <summary>
    /// Serinin durum bilgisi
    /// </summary>
    public StaticSeriesStatusEnm SeriesStatusId { get; set; }

    /// <summary>
    /// Serinin Türü
    /// </summary>
    public StaticSeriesTypeEnm SeriesTypesId { get; set; }

    /// <summary>
    /// Serinin Sanatçıları
    /// </summary>
    public List<Guid>? SeriesArtistIds { get; set; } = new();

    /// <summary>
    /// Serinin Yazarları
    /// </summary>
    public List<Guid>? SeriesAuthorIds { get; set; } = new();

    /// <summary>
    /// Serinin kategorileri
    /// </summary>
    public List<Guid>? SeriesCategoryIds { get; set; } = new();

    //bölümleri seri oluşturulduktan sonra farklı bir servis ile eklenecek
}

public class CreateSeriesCommandValidator : AbstractValidator<CreateSeriesCommand>
{
    public CreateSeriesCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "Title"))
            .WithErrorCode(ApplicationMessages.ErrorDefaultIsNull)
            .NotEmpty()
            .WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "Title"))
            .WithErrorCode(ApplicationMessages.ErrorDefaultIsNull);

        RuleFor(x => x.SeriesStatusId)
            .IsInEnum()
            .WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "SeriesStatusId"))
            .WithErrorCode(ApplicationMessages.ErrorDefaultTypeError)
            .NotNull()
            .WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesStatusId"))
            .WithErrorCode(ApplicationMessages.ErrorDefaultIsNull)
            .NotEmpty()
            .WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesStatusId"))
            .WithErrorCode(ApplicationMessages.ErrorDefaultIsNull);

        RuleFor(x => x.SeriesTypesId)
            .IsInEnum()
            .WithMessage(string.Format(ApplicationMessages.ErrorDefaultTypeError.GetMessage(), "SeriesTypesId"))
            .WithErrorCode(ApplicationMessages.ErrorDefaultTypeError)
            .NotNull()
            .WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesTypesId"))
            .WithErrorCode(ApplicationMessages.ErrorDefaultIsNull)
            .NotEmpty()
            .WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "SeriesTypesId"))
            .WithErrorCode(ApplicationMessages.ErrorDefaultIsNull);


    }

}
