using System;
using System.Collections.Generic;

namespace Inception.Repository.Testing
{
    public class SiteTestResult
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

        public DateTime TestedOn
        {
            get;
            set;
        }


        public virtual List<LinkTestResult> LinkTestResults
        {
            get;
            set;
        }
    }
}
