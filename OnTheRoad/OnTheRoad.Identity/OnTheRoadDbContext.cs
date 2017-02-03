using Microsoft.AspNet.Identity.EntityFramework;

namespace OnTheRoad.Identity
{
    public class OnTheRoadDbContext : IdentityDbContext<ApplicationUser>
    {
        public OnTheRoadDbContext()
            : base("OnTheRoadDB", throwIfV1Schema: false)
        {
        }

        public static OnTheRoadDbContext Create()
        {
            return new OnTheRoadDbContext();
        }
    }
}
