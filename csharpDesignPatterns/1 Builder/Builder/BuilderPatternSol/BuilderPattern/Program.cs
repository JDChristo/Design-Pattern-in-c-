using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderPattern
{
    public class HtmlElements
    {
        public string Name, Text;
        public List<HtmlElements> Elements = new List<HtmlElements>();

        private const int indentSize = 2;

        public HtmlElements() { }

        public HtmlElements(string name, string text)
        {
            Name = name;
            Text = text;
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            sb.AppendLine($"{i} <{Name}>");

            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.AppendLine(Text);
            }

            foreach(var e in Elements)
            {
                sb.Append(e.ToStringImpl(indent + 1));
            }
            sb.AppendLine($"{i}</{Name}>");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    public class HtmlBuilder
    {
        private readonly string rootName;
        HtmlElements root = new HtmlElements();

        public HtmlBuilder(string rootName)
        {
            this.rootName = rootName;
            root.Name = rootName;
        }

        public HtmlBuilder AddChild(string childName, string childText)
        {
            var e = new HtmlElements(childName, childText);
            root.Elements.Add(e);
            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            root = new HtmlElements { Name = rootName };
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var hello = "hello";
            var sb = new StringBuilder();

            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("<p>");

            Console.WriteLine(sb);

            var words = new[] { "hello", "world" };
            sb.Clear();
            sb.Append("<ul>");
            foreach(var word in words)
            {
                sb.AppendFormat("<li>{0}</li>", word);
            }
            sb.Append("</ul>");
            Console.WriteLine(sb);

            var buildedr = new HtmlBuilder("ul");
            buildedr.AddChild("li", hello).AddChild("li", "world");
            Console.WriteLine(buildedr.ToString());

        }
    }
}
