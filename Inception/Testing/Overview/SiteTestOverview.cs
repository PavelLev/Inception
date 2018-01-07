using System;
using System.Collections.Generic;

namespace Inception.Testing.Overview
{
    public class SiteTestOverview
    {
        public List<LinkTestOverview> LinkTestOverviews { get; set; }
        public DateTime FirstTestedOn { get; set; }
        public DateTime LastTestedOn { get; set; }
    }
}