using Inception.Repository.Testing;
using Inception.Repository.Testing.Overview;
using Microsoft.EntityFrameworkCore;

namespace Inception.Repository
{
    public class InceptionDbContext : DbContext
    {
        public InceptionDbContext(DbContextOptions<InceptionDbContext> options)
            : base(options)
        {
        }



        public DbSet<LinkTestResult> LinkTestResults
        {
            get;
            set;
        }

        public DbSet<SiteTestResult> SiteTestResults
        {
            get;
            set;
        }

        public DbSet<LinkTestOverview> LinkTestOverviews
        {
            get;
            set;
        }

        public DbSet<SiteTestOverview> SiteTestOverviews
        {
            get;
            set;
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LinkTestResult>()
                .HasKey(linkTestResult => linkTestResult.Id);

            modelBuilder.Entity<SiteTestResult>()
                .HasKey(siteTestResult => siteTestResult.Id);
            
            modelBuilder.Entity<LinkTestResult>()
                .HasOne(linkTestResult => linkTestResult.SiteTestResult)
                .WithMany(siteTestResult => siteTestResult.LinkTestResults)
                .HasForeignKey(linkTestResult => linkTestResult.SiteTestResultId);


            modelBuilder.Entity<LinkTestOverview>()
                .HasKey(linkTestOverview => linkTestOverview.Id);

            modelBuilder.Entity<SiteTestOverview>()
                .HasKey(siteTestOverview => siteTestOverview.Id);

            modelBuilder.Entity<LinkTestOverview>()
                .HasOne(linkTestOverview => linkTestOverview.SiteTestOverview)
                .WithMany(siteTestOverview => siteTestOverview.LinkTestOverviews)
                .HasForeignKey(linkTestOverview => linkTestOverview.SiteTestOverviewId);
        }
    }
}
