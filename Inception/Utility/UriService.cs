using System;
using Inception.Utility.Extensions;

namespace Inception.Utility
{
    public class UriService : IUriService
    {
        public string ToAbsolute
            (
            string url,
            string link
            )
        {
            var uri = new Uri(link, UriKind.RelativeOrAbsolute);

            if (!uri.IsAbsoluteUri)
            {
                uri = new Uri(new Uri(url), uri);
            }

            var absoulteUrl = uri.AbsoluteUri;

            return absoulteUrl;
        }



        public bool AreFromSameHost
            (
            string firstUrl,
            string secondUrl
            )
        {
            var areFromSameHost = GetDomain(firstUrl) == GetDomain(secondUrl);

            return areFromSameHost;
        }



        public string GetDomain
            (
            string url
            )
        {
            var uri = new Uri
                (url);

            var domain = uri.Host;


            if (domain.StartsWith
                ("www."))
            {
                domain = domain.Substring
                    (
                    "www.".Length
                    );
            }

            return domain;
        }



        public string Normalize(string source)
        {
            var normalizedUrl = source.RemoveFromStart("http://")
                .RemoveFromStart("https://")
                .RemoveFromStart("www.")
                .Substring("?")
                .Substring("#")
                .Substring("/", -1);

            return normalizedUrl;
        }
    }
}