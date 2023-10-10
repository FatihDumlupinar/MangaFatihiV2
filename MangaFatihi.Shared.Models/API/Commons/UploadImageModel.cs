using FluentValidation;
using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Models.Validators;
using Microsoft.AspNetCore.Http;

namespace MangaFatihi.Shared.Models.API.Commons
{
    public class UploadImageModel
    {
        public IFormFile File { get; set; } = null!;

    }

    public class UploadImageModelValidator : AbstractValidator<UploadImageModel>
    {
        public UploadImageModelValidator()
        {
            RuleFor(x => x.File)
                .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "File"))
                .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "File"))
                .SetValidator(new ImageValidator());

        }
    }
}
