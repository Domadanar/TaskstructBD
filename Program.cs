using System;
using System.Threading.Tasks;
using Npgsql;

namespace Taskstruct
{

    class Program
    {
        static void Main()
        {  
            Todolist item = new Todolist("ggggg", DateTime.UtcNow, "dddd");

            PrintTask();
            Create(item);
        }
        public static void PrintTask()
        {
            var connString = "Host=127.0.0.1;Username=todotask_app;Password=1234;Database=todotask";

            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            using (var cmd = new NpgsqlCommand("SELECT id, done, title, due_date, description FROM task", conn))
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {

                    bool done = reader.GetBoolean(1);
                    char doneFlag = done ? 'x' : ' ';
                    string title = reader.GetString(2);
                    string description = reader.GetString(4);
                    int id = reader.GetInt32(0);
                    string date;
                    if (reader.IsDBNull(3))
                    {
                        date = "No date";
                    }
                    else
                    {
                        date = reader.GetDateTime(3).ToString();
                    }
                    Console.WriteLine($"{id}-\t[{doneFlag}] {title} {date} [{description}]");
                }
        }
        public static void Create(Todolist item)
        {
            var connString = "Host=127.0.0.1;Username=todotask_app;Password=1234;Database=todotask";

            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            using (var cmd = new NpgsqlCommand("INSERT INTO task (done, title, due_date, description) VALUES (false, @title, @due_date, @description)", conn))
            {
                cmd.Parameters.AddWithValue("title", item.Title);
                cmd.Parameters.AddWithValue("due_date", item.Date);
                cmd.Parameters.AddWithValue("description", item.Description);
                cmd.ExecuteNonQuery();
            }
        }
        public static void Update(int id)
        {
            var connString = "Host=127.0.0.1;Username=todotask_app;Password=1234;Database=todotask";

            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            using (var cmd = new NpgsqlCommand("UPDATE task set description = 123 where id =@id", conn))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public static void Delete(int id)
        {
            var connString = "Host=127.0.0.1;Username=todotask_app;Password=1234;Database=todotask";

            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            using (var cmd = new NpgsqlCommand("DELETE FROM task WHERE id =@id", conn))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
