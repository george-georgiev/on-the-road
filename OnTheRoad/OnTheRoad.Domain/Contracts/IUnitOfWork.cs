namespace OnTheRoad.Domain.Contracts
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
