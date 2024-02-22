
using Practikod_2;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;

public class Program
{
    static async Task Main(string[] args)
    {
        // var content = File.ReadAllText("void_tags.json.txt");
        var path1 = "void_tags.json.txt";
        var path2 = "all_tags.json.txt";
      
        var html = await Load("https://hebrewbooks.org/beis");
      File.WriteAllText(path2, html);
        var cleanHtml = Regex.Replace(html, @"\s+", ""); // הסרת רווחים
        var lines = Regex.Split(cleanHtml, @"<(.*?)>"); // פיצול לפי תגי HTML
       
       
        var elements = lines
            .Where(line => line.Length > 0) // הסרת שורות ריקות
            .Select(line => HtmlHelper.Instance.GetElements(line).First()) // יצירת אובייקטים של HtmlElement
            .ToList();
        for (int i = 0; i < lines.Length; i++)
        {
            File.WriteAllText(path1, lines[i]);
        }
        foreach (var element in elements)
        {
            Console.WriteLine(element);
        }

        var service = new HtmlElementService();

        var elements2 = HtmlHelper.Instance.GetElements(html);

        // חיפוש אלמנטים לפי סלקטור CSS
        var results = service.QuerySelector(elements2, "div#mydiv.class-name");

        foreach (var el in results)
        {
            Console.WriteLine(el);
        }
        // קבלת מחרוזת שאילתה מהמשתמש
        var selectorString = Console.ReadLine();

        // המרת מחרוזת שאילתה לאובייקט Selector
        var selector = Selector.Parse(selectorString);

        // בדיקת פונקציית Descendants
        var descendants = elements[0].Descendants();
        Console.WriteLine("Descendants:");
        foreach (var element in descendants)
        {
            Console.WriteLine(element);
        }

        // בדיקת פונקציית Ancestors
        var ancestors = elements[0].Ancestors();
        Console.WriteLine("Ancestors:");
        foreach (var element in ancestors)
        {
            Console.WriteLine(element);
        }
        Console.WriteLine("FindBySelector:");
        foreach (var element in results)
        {
            Console.WriteLine(element);
        }

    }

    static async Task<string> Load(string url)
    {
        HttpClient client = new HttpClient();
        var response = await client.GetAsync(url);
        var html = await response.Content.ReadAsStringAsync();
        return html;
    }

}

