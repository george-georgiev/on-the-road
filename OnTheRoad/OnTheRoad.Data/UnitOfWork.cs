using OnTheRoad.Domain.Contracts;

namespace OnTheRoad.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnTheRoadIdentityDbContext context;

        public UnitOfWork(OnTheRoadIdentityDbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
