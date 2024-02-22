using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practikod_2
{
  

    public class HtmlElement
    {


        public string Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }

        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }

        public HtmlElement()
        {
            Attributes = new Dictionary<string, string>();
            Classes = new List<string>();
            Children = new List<HtmlElement>();
        }


        public override string ToString()
        {
            var attributesString = string.Join(" ", Attributes.Select(x => $"{x.Key}={x.Value}"));
            return $"<{Name} {attributesString}>";
        }
        public IEnumerable<HtmlElement> Descendants()
        {
            var queue = new Queue<HtmlElement>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var element = queue.Dequeue();
                yield return element;

                foreach (var child in element.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }
        public IEnumerable<HtmlElement> Ancestors()
        {
            var element = this.Parent;

            while (element != null)
            {
                yield return element;
                element = element.Parent;
            }
        }
        //public IEnumerable<HtmlElement> FindBySelector(Selector selector)
        //{
        //    return FindBySelector(selector, new List<HtmlElement>());

        //    // פונקציה ריקורסיבית
        //    IEnumerable<HtmlElement> FindBySelector(Selector currentSelector, List<HtmlElement> results)
        //    {
        //        // תנאי עצירה - הגענו לסוף הסלקטור
        //        if (currentSelector == null)
        //        {
        //            return results;
        //        }

        //        // מציאת כל הצאצאים שעונים לקריטריונים של הסלקטור הנוכחי
        //        var descendants = this.Descendants().Where(element => MatchesSelector(element, currentSelector));

        //        // חיפוש ברשימה המסוננת עם הסלקטור הבא
        //        foreach (var descendant in descendants)
        //        {
        //            results = FindBySelector(currentSelector.Child, results).ToList();
        //        }

        //        return results;
        //    }

        //    // פונקציה לבדיקת התאמה בין אלמנט לסלקטור
        //    bool MatchesSelector(HtmlElement element, Selector selector)
        //    {
        //        if (selector.TagName != null && element.Name != selector.TagName)
        //        {
        //            return false;
        //        }

        //        if (selector.Id != null && element.Id != selector.Id)
        //        {
        //            return false;
        //        }

        //        if (selector.Classes.Any() && !element.Classes.Any(x => selector.Classes.Contains(x)))
        //        {
        //            return false;
        //        }

        //        return true;
        //    }
        //}

    }
}



