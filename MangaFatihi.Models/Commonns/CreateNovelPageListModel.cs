using FluentValidation;
using MangaFatihi.Domain.Constants;

namespace MangaFatihi.Models.Commonns
{
    public class CreateNovelPageListModel
    {
        /// <summary>
        /// Sayfa Numarası
        /// </summary>
        public int PageNo { get; set; } = 0;

        /// <summary>
        /// İçerik
        /// </summary>
        public string PageContent { get; set; } = "";

    }
    public class CreateNovelPageListModelValidator : AbstractValidator<CreateNovelPageListModel>
    {
        public CreateNovelPageListModelValidator()
        {
            RuleFor(x => x.PageContent)
                .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "PageContent"))
                .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "PageContent"));

            RuleFor(x => x.PageNo)
                .NotNull().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "PageNo"))
                .NotEmpty().WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "PageNo"))
                .LessThanOrEqualTo(1).WithMessage(string.Format(ApplicationMessages.ErrorDefaultIsNull.GetMessage(), "PageNo"));

        }
    }
}
