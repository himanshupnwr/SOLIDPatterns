using System;
using System.Collections.Generic;
using System.IO;

namespace SOLIDPatterns
{
    public class Journal
    {
        private readonly List<string> entries = new List<string>();

        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
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
        //breaks single responsibility principle 
        public void Save(string filename, bool overwrite = false)
        {
            File.WriteAllText(filename, ToString());
        }
    }

    //handles the responsibility of persisting objects
    public class Persistence
    {
        public void SaveToFile(Journal journal, string filename, bool overwrite = false) {
            if(overwrite || !File.Exists(filename))
            {
                File.WriteAllText(filename, journal.ToString());
            }
        }
    }
    class SingleResponsibility
    {
        static void MainSP(string[] args)
        {
            var j = new Journal();
            j.AddEntry("I am happy");
            j.AddEntry("I am himanshu");
            j.AddEntry("I am Akshita");
            Console.WriteLine(j);

            var p = new Persistence();
            var filename = @"C:\temp\journal.txt";
            p.SaveToFile(j, filename);
            Console.WriteLine(filename);
        }
    }
}
