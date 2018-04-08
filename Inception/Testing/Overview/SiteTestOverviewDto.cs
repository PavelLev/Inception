using System;
using System.Collections.Generic;

namespace Inception.Repository.Testing.Overview
{
    public class SiteTestOverviewDto
    {
        public int Id
        {
            get;
            set;
        }

        public string DomainName
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


        public List<LinkTestOverviewDto> LinkTestOverviews
        {
            get;
            set;
        }
    }
}
