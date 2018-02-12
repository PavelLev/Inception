namespace Inception.Repository.Testing
{
    public class LinkTestResult
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

        public int ResponseTime
        {
            get;
            set;
        }


        public int SiteTestResultId
        {
            get;
            set;
        }

        public virtual SiteTestResult SiteTestResult
        {
            get;
            set;
        }
    }
}
