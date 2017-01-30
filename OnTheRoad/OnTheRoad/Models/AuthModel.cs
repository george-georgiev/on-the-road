namespace OnTheRoad.Models
{
    public class AuthModel
    {
        public string Username { get; set; }

        public string UserEmail { get; set; }

        public bool HasSucceeded { get; set; }

        public string ErrorMsg { get; set; }
    }
}