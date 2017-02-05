using Microsoft.AspNet.Identity.EntityFramework;

namespace OnTheRoad.Identity
{
    public class OnTheRoadIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public OnTheRoadIdentityDbContext()
            : base("OnTheRoadDB", throwIfV1Schema: false)
        {
        }

        public static OnTheRoadIdentityDbContext Create()
        {
            return new OnTheRoadIdentityDbContext();
        }
    }
}
