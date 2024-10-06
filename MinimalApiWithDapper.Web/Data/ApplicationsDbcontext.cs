using Microsoft.EntityFrameworkCore;

namespace MinimalApiWithDapper.Web.Data
{
    public class ApplicationsDbcontext:DbContext
    {

        public ApplicationsDbcontext(DbContextOptions<ApplicationsDbcontext> options):base(options)
        {
            
        }



    }
}
