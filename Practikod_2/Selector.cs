using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practikod_2
{
    internal class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }

        public Selector Parent { get; set; }
        public Selector Child { get; set; }

        public Selector()
        {
            Classes = new List<string>();
        }

        public static Selector Parse(string selectorString)
        {
            // פירוק מחרוזת הסלקטור לחלקים לפי רווחים
            var parts = selectorString.Split(' ');

            // יצירת אובייקט שורש
            var root = new Selector();
            var current = root;

            foreach (var part in parts)
            {
                // פירוק חלק הסלקטור לפי # ו-.
                var subparts = part.Split('#', '.');

                // עדכון מאפייני הסלקטור הנוכחי
                if (subparts[0].Length > 0)
                {
                    current.TagName = subparts[0];
                }

                if (subparts.Length > 1)
                {
                    current.Id = subparts[1];
                }

                if (subparts.Length > 2)
                {
                    current.Classes.AddRange(subparts.Skip(2));
                }

                // יצירת אובייקט סלקטור חדש והוספתו כבן
                var child = new Selector();
                current.Child = child;
                current = child;
            }

            return root;
        }

    }
}
