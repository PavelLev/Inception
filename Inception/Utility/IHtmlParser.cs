using System.Collections.Generic;

namespace Inception.Utility
{
    public interface IHtmlParser
    {
        IEnumerable<string> GetLinks
            (
            string html
            );
    }
}
