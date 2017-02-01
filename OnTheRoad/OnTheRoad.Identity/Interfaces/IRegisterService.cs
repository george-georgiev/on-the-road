namespace OnTheRoad.Identity.Interfaces
{
   public interface IRegisterService
    {
        void CreateUser(string username, string email, string password);
    }
}
