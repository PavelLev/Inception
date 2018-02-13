namespace Inception.Utility
{
    public interface IUriService
    {
        string ToAbsolute
            (
            string url,
            string link
            );



        bool AreFromSameHost
            (
            string firstUrl,
            string secondUrl
            );



        string GetDomain
            (
            string url
            );



        string Normalize
            (
            string source
            );
    }
}
