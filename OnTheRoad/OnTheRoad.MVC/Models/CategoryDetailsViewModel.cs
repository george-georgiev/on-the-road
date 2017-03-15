namespace OnTheRoad.MVC.Models
{
    public class CategoryDetailsViewModel : CategoryOverviewViewModel
    {
        public int Page { get; set; }

        public int Take { get; set; }

        public int Total { get; set; }
    }
}