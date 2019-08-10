namespace LoWaiLo.WebAPI.ViewModels.Reviews.ViewModels
{
    using LoWaiLo.WebAPI.ViewModels.Reviews.InputModels;
    using System.Collections.Generic;

    public class SiteReviewViewModel
    {
        public List<ReviewViewModel> Reviews { get; set; }

        public CreateReviewInputModel InnerModel { get; set; }
    }
}
