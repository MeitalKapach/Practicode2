using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practikod_2
{
    public interface IHtmlElementService
    {
        IEnumerable<HtmlElement> QuerySelector(IEnumerable<HtmlElement> elements, string selector);
    }
}
