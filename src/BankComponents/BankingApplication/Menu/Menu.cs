using System;
using System.Collections.Generic;

namespace BankingApplication.Menu
{
    public class Menu
    {
        private List<IMenuEntry> _entries = new List<IMenuEntry>();

        public void Print()
        {
            _entries.ForEach(x=>Console.WriteLine(x.Title));
        }

        public void Execute(string command)
        {
            _entries.Find(x=>x.CanHandle(command)).Command.Execute();
        }

        public void AddEntry(IMenuEntry entry)
        {
            _entries.Add(entry);
        }
    }
}