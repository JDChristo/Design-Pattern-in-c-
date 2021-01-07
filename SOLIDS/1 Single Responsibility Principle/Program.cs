using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace SingleResponsibilityPrinciple
{
    /* 
     * A typical class is responsible of one thing and has one reason to change
     * Here Journal Class is responsible for only to modify Journal i.e, Add, Remove, Or Print the content
     * Persistence Class is responsible to save file in the Dir
     * Job to Modify Journal and to Save the File is distributed to Journal Class and Persistence Class
     * Thus a single class is responsible for single thing
     */

    // A CLASS SHOULD HAVE INDEPENDENT TASK

    public class Journal
    {
        private readonly List<string> entries = new List<string>();
        private static int count = 0;
        public int AddEntry(string text)
        {
            entries.Add($"{++count} : {text}");
            return count;
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
    }
    public class Persistence
    {
        public void SaveToFile(Journal j, string filename, bool overwrite = false)
        {
            if(overwrite || !File.Exists(filename))
            {
                File.WriteAllText(filename, j.ToString());
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Journal journel = new Journal();
            journel.AddEntry("first entry");
            journel.AddEntry("Second Entry");
            Console.WriteLine(journel.ToString());

            Persistence p = new Persistence();
            var filename = @"c:\temp\journal.txt";
            p.SaveToFile(journel, filename, true);
            //Process.Start(filename);
        }
    }
}
