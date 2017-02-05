using OnTheRoad.Domain.Contracts;

namespace OnTheRoad.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private OnTheRoadDbContext context;

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
