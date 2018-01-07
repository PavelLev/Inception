using System;
using System.Collections.Generic;

namespace Inception.Testing
{
    public class SiteTestResult
    {
        public string Id { get; set; }

        public string DomainName { get; set; }

        public DateTime TestedOn { get; set; }

        public List<LinkTestResult> LinkTestResults { get; set; }
    }
}