namespace OnTheRoad.Logic.Contracts
{
    public interface ILoginService
    {
        string LoginUser(string email, string password, bool rememberMe);
    }
}
