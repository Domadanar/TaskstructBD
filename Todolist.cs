using System;
using System.Threading.Tasks;
using Npgsql;

namespace Taskstruct
{
    class Todolist
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public Todolist(string title, DateTime date, string description)
        {
            Title = title;
            Date = date;
            Description = description;
        }
    }
}