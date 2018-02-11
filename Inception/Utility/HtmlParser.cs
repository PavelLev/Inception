using System;
using System.Collections.Generic;
using System.Linq;
using DryIoc;
using HtmlAgilityPack;

namespace Inception.Utility
{
    public class HtmlParser : IHtmlParser
    {
        private readonly Func<HtmlDocument> _htmlDocumentFactoryMethod;



        public HtmlParser(Func<HtmlDocument> htmlDocumentFactoryMethod)
        {
            _htmlDocumentFactoryMethod = htmlDocumentFactoryMethod;
        }



        public IEnumerable<string> GetLinks
            (
            string html
            )
        {
            var htmlDocument = _htmlDocumentFactoryMethod();

            htmlDocument.LoadHtml(html);

            var links = htmlDocument.DocumentNode.SelectNodes("//a")
                ?.Select
                    (
                    htmlNode =>
                    {
                        var link = htmlNode.GetAttributeValue
                            (
                            "href",
                            ""
                            );

                        return link;
                    })
                .Where
                    (
                    link => link != ""
                    );

            return links ?? Enumerable.Empty<string>();
        }
    }
}