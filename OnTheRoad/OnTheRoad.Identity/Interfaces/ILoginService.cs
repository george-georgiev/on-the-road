namespace OnTheRoad.Identity.Interfaces
{
    public interface ILoginService
    {
        string LoginUser(string email, string password, bool rememberMe);
    }
}
