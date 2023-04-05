using FluentValidation;
using MangaFatihi.Domain.Constants;
using Microsoft.AspNetCore.Http;

namespace MangaFatihi.Models.Validators
{
    public class ImageValidator : AbstractValidator<IFormFile>
    {
        private readonly List<string> ALLOWED_CONTENT_TYPES = new()
        {
            "image/jpeg",
            "image/jpg",
            "image/png",
            "image/gif",

        };

        public ImageValidator()
        {
            RuleFor(x => x.ContentType)
                .NotNull()
                .Must(x => ALLOWED_CONTENT_TYPES.Contains(x))
                .WithMessage(string.Format(ApplicationMessages.ErrorNotAllowedFileExtension.GetMessage(), string.Join(",", ALLOWED_CONTENT_TYPES)));

        }
    }
}
