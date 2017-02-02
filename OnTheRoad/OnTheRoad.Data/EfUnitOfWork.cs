using OnTheRoad.Domain.Contracts;
using System.Data.Entity;

namespace OnTheRoad.Data
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private DbContext context;

        public EfUnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
