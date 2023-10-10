using FluentValidation;
using MangaFatihi.Shared.Models.Constants;

namespace MangaFatihi.Shared.Models.API.Commons.SeriesEpisodePages
{
    public class UpdateNovelPageListModel
    {
        /// <summary>
        /// Sayfa Unique Id'si
        /// </summary>
        public Guid PageId { get; set; }

        /// <summary>
        /// Sayfa Numarası
        /// </summary>
        public int PageNo { get; set; } = 1;

        /// <summary>
        /// İçerik
        /// </summary>
        public string PageContent { get; set; } = "";
    }
    public class UpdateNovelPageListModelValidator : AbstractValidator<UpdateNovelPageListModel>
    {
        public UpdateNovelPageListModelValidator()
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
