using System;
using MySql.Data.MySqlClient;

class Program
{
    static string connectionString = "server=localhost;user=root;password=your_password;database=SampleDB";

    static void Main()
    {
        string continueChoice;

        do
        {
            Console.WriteLine("\n--- Student Management ---");
            Console.WriteLine("1. Insert Student");
            Console.WriteLine("2. View All Students");
            Console.WriteLine("3. Update Student");
            Console.WriteLine("4. Delete Student");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    InsertStudent();
                    break;
                case "2":
                    ViewStudents();
                    break;
                case "3":
                    UpdateStudent();
                    break;
                case "4":
                    DeleteStudent();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            Console.Write("\nDo you want to continue? (y/n): ");
            continueChoice = Console.ReadLine();

        } while (continueChoice.Equals("y", StringComparison.OrdinalIgnoreCase));
    }

    static void InsertStudent()
    {
        Console.Write("Enter name: ");
        string name = Console.ReadLine();

        Console.Write("Enter age: ");
        int age = int.Parse(Console.ReadLine());

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "INSERT INTO Students (Name, Age) VALUES (@name, @age)";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@age", age);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "Student added." : "Insert failed.");
        }
    }

    static void ViewStudents()
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM Students";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                Console.WriteLine("\nId\tName\t\tAge");
                Console.WriteLine("---------------------------------");

                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Id"]}\t{reader["Name"]}\t\t{reader["Age"]}");
                }
            }
        }
    }

    static void UpdateStudent()
    {
        Console.Write("Enter ID to update: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Enter new name: ");
        string name = Console.ReadLine();

        Console.Write("Enter new age: ");
        int age = int.Parse(Console.ReadLine());

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "UPDATE Students SET Name = @name, Age = @age WHERE Id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.Parameters.AddWithValue("@id", id);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "Student updated." : "Update failed.");
        }
    }

    static void DeleteStudent()
    {
        Console.Write("Enter ID to delete: ");
        int id = int.Parse(Console.ReadLine());

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "DELETE FROM Students WHERE Id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "Student deleted." : "Delete failed.");
        }
    }
}
