using Microsoft.EntityFrameworkCore.Design;
using DryIoc;

namespace Inception.Repository
{
    internal class InceptionDesignTimeDbContextFactory : IDesignTimeDbContextFactory<InceptionDbContext>
    {
        public InceptionDbContext CreateDbContext(string[] args)
        {
            var inceptionDbContext = StaticContainer.Instance.Resolve<InceptionDbContext>();

            return inceptionDbContext;
        }
    }
}
