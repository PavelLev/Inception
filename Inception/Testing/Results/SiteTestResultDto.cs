using System;
using System.Collections.Generic;

namespace Inception.Testing.Results
{
    public class SiteTestResultDto
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

        public List<int> LinkTestResultIds
        {
            get;
            set;
        }
    }
}
