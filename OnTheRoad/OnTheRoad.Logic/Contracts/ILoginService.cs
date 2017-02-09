namespace OnTheRoad.Logic.Contracts
{
    public interface ILoginService
    {
        string LoginUser(string username, string password, bool rememberMe);
    }
}
