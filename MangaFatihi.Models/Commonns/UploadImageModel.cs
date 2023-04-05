using FluentValidation;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Validators;
using Microsoft.AspNetCore.Http;

namespace MangaFatihi.Models.Commonns
{
    public class UploadImageModel
    {
        public IFormFile File { get; set; }

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
