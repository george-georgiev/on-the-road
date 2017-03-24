using System.ComponentModel.DataAnnotations;

namespace OnTheRoad.MVC.Areas.User.Models
{
    public class ProfileViewModel
    {
        [UIHint("User")]
        public UserViewModel User { get; set; }

        public bool IsOwner { get; set; }

        public bool IsFollowing { get; set; }
    }
}