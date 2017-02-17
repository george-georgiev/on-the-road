using OnTheRoad.Data.Contracts;
using OnTheRoad.Domain.Contracts;

namespace OnTheRoad.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IOnTheRoadDbContext context;

        public UnitOfWork(IOnTheRoadDbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
