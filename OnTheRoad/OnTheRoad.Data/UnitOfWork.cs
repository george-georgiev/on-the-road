using OnTheRoad.Domain.Contracts;

namespace OnTheRoad.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnTheRoadDbContext context;

        public UnitOfWork(OnTheRoadDbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
