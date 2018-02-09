using System;

namespace Inception.Repository.Testing.Overview
{
    public class LinkTestOverview
    {
        public int Id
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        public int MinimumResponseTime
        {
            get;
            set;
        }

        public int MaximumResponseTime
        {
            get;
            set;
        }


        public int SiteTestOverviewId
        {
            get;
            set;
        }

        public SiteTestOverview SiteTestOverview
        {
            get;
            set;
        }
    }
}
