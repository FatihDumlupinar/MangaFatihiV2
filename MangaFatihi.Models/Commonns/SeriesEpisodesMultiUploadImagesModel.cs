﻿using FluentValidation;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Validators;
using Microsoft.AspNetCore.Http;

namespace MangaFatihi.Models.Commonns
{
    /// <summary>
    /// Çoklu dosya(resim) yükleme modeli
    /// </summary>
    public class SeriesEpisodesMultiUploadImagesModel
    {
        /// <summary>
        /// resimler
        /// </summary>
        public List<IFormFile> Files { get; set; } = new();

    }

    public class SeriesEpisodesMultiUploadImagesModelValidator : AbstractValidator<SeriesEpisodesMultiUploadImagesModel>
    {
        public SeriesEpisodesMultiUploadImagesModelValidator()
        {
            RuleFor(x => x.Files)
                .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "Files"))
                .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "Files"))
                .Must(i => i == null || !i.Any()).WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "Files"));

           RuleForEach(x => x.Files).SetValidator(new ImageValidator());

        }
    }
}
