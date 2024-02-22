using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practikod_2
{
    internal class HtmlElementService: IHtmlElementService
    {
        public IEnumerable<HtmlElement> QuerySelector(IEnumerable<HtmlElement> elements, string selector)
        {
            var parts = selector.Split(' ');

            foreach (var part in parts)
            {
                // חיפוש לפי תגית
                if (part.StartsWith("."))
                {
                    elements = elements.Where(x => x.Classes.Contains(part.Substring(1)));
                }
                // חיפוש לפי id
                else if (part.StartsWith("#"))
                {
                    elements = elements.Where(x => x.Id == part.Substring(1));
                }
                // חיפוש לפי שם תגית
                else
                {
                    elements = elements.Where(x => x.Name == part);
                }

                // חיפוש בדורות הבאים (לעומק)
                if (part.Contains(" "))
                {
                    elements = elements.SelectMany(x => x.Children).Where(x => QuerySelector(new[] { x }, part).Any());
                }
            }

            return elements;
        }
    }
}
