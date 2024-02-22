
using Practikod_2;
using System.Text.Json;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public sealed class HtmlHelper
{
    private static readonly Lazy<HtmlHelper> _instance = new Lazy<HtmlHelper>(() => new HtmlHelper());
    public  readonly string[] AllTags;
    public  readonly string[] VoidTags;

    private HtmlHelper()
    {
        using (StreamReader reader = new StreamReader("all_tags.json"))
        {
            AllTags = JsonSerializer.Deserialize<string[]>(reader.ReadToEnd());
        }

        using (StreamReader reader = new StreamReader("void_tags.json"))
        {
            VoidTags = JsonSerializer.Deserialize<string[]>(reader.ReadToEnd());
        }
    }

    public static HtmlHelper Instance
    {
        get { return _instance.Value; }
    }
    public HashSet<HtmlElement> UniqueElements { get; } = new HashSet<HtmlElement>();

    public IEnumerable<HtmlElement> GetElements(string html)
    {
        var regex = new Regex("<(.*?)>");
        var matches = regex.Matches(html);

        var rootElement = new HtmlElement();
        var currentElement = rootElement;

        foreach (Match match in matches)
        {
            var tag = match.Groups[1].Value;

            if (tag.StartsWith("html/"))
            {
                break;
            }

            if (tag.StartsWith("/"))
            {
                currentElement = currentElement.Parent;
                continue;
            }

            var element = new HtmlElement();
            element.Name = tag;

            var parts = tag.Split(' ');
            for (int i = 1; i < parts.Length; i++)
            {
                var attribute = parts[i];

                if (attribute.Contains("="))
                {
                    var keyValue = attribute.Split('=');
                    element.Attributes[keyValue[0]] = keyValue[1].Trim('"');

                    if (keyValue[0] == "id")
                    {
                        element.Id = keyValue[1];
                    }
                }
                else
                {
                    element.Classes.Add(attribute);
                }
            }

            element.Parent = currentElement;
            currentElement.Children.Add(element);

            if (VoidTags.Contains(element.Name))
            {
                continue;
            }
            if (!UniqueElements.Contains(element))
            {
                UniqueElements.Add(element);
                yield return element;
            }

            currentElement = element;
            element.InnerHtml = match.Value.Substring(tag.Length + 1);
        }

        yield return rootElement;
    }
}

