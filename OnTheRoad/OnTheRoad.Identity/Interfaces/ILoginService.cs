namespace OnTheRoad.Identity.Interfaces
{
    public interface ILoginService
    {
        void LoginUser(string email, string password, bool rememberMe);
    }
}
