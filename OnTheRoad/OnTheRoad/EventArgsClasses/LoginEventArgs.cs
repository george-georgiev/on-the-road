namespace OnTheRoad.EventArgsClasses
{
    public class LoginEventArgs
    {
        public string UserEmail { get; set; }

        public string UserPassword { get; set; }

        public bool RememberMe { get; set; }
    }
}