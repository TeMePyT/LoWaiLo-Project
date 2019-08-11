namespace LoWaiLo.WebAPI.ViewModels.Reviews.ViewModels
{
    using System.Collections.Generic;
    using LoWaiLo.WebAPI.ViewModels.Reviews.InputModels;

    public class SiteReviewViewModel
    {
        public IEnumerable<ReviewViewModel> Reviews { get; set; }

        public CreateReviewInputModel InnerModel { get; set; }

        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

    }
}
