namespace OnTheRoad.Identity.Interfaces
{
   public interface IRegisterService
    {
        void CreateUser(string email, string password);
    }
}
