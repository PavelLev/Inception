using System;
using System.Collections.Generic;

namespace Inception.Repository.Testing.Overview
{
    public class SiteTestOverview
    {
        public int Id
        {
            get;
            set;
        }

        public DateTime FirstTestedOn
        {
            get;
            set;
        }

        public DateTime LastTestedOn
        {
            get;
            set;
        }


        public List<LinkTestOverview> LinkTestOverviews
        {
            get;
            set;
        }
    }
}
