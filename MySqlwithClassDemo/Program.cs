using System;
using MySql.Data.MySqlClient;


namespace MySqlwithClassDemo
{
    class Program
    {
        static string connectionString = "data source=localhost;database=sample;user=root;password=Lavs_2021";

        static void Main(string[] args)
        {
            string choice;
            do
            {
                Console.WriteLine("\n--- Employee CRUD Menu ---");
                Console.WriteLine("1. Insert Employee");
                Console.WriteLine("2. Fetch All Employees");
                Console.WriteLine("3. Update Employee Department");
                Console.WriteLine("4. Delete Employee");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Employee emp = new Employee();
                        Console.Write("Enter Employee Name: ");
                        emp.EmpName = Console.ReadLine();
                        Console.Write("Enter Department: ");
                        emp.Department = Console.ReadLine();
                        Insert(emp);
                        break;

                    case 2:
                        Fetch();
                        break;

                    case 3:
                        Employee empl = new Employee();
                        Console.Write("Enter Employee ID to update: ");
                       empl.EmpId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter New Department: ");
                        empl.Department = Console.ReadLine();
                        Update(empl);
                        break;

                    case 4:
                        Console.Write("Enter Employee ID to delete: ");
                        int deleteId = Convert.ToInt32(Console.ReadLine());
                        Delete(deleteId);
                        break;

                    case 5:
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid Option. Try again.");
                        break;
                }

                Console.Write("\nDo you want to continue? (y/n): ");
                choice = Console.ReadLine();

            } while (choice.ToLower() == "y");

            Console.WriteLine("Goodbye!");
        }

        public static void Insert(Employee emp)
        {
            using MySqlConnection mysqlcon = new MySqlConnection(connectionString);
            mysqlcon.Open();

            string query = "INSERT INTO Employee(empname, dept) VALUES(@nm, @dp)";
            MySqlCommand cmd = new MySqlCommand(query, mysqlcon);
            cmd.Parameters.AddWithValue("@nm", emp.EmpName);
            cmd.Parameters.AddWithValue("@dp", emp.Department);

            cmd.ExecuteNonQuery();
            Console.WriteLine("Inserted Successfully!");
        }

        public static void Fetch()
        {
            using MySqlConnection mysqlcon = new MySqlConnection(connectionString);
            mysqlcon.Open();

            string query = "SELECT * FROM Employee";
            MySqlCommand cmd = new MySqlCommand(query, mysqlcon);
            MySqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n--- Employee List ---");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["empid"]}, Name: {reader["empname"]}, Department: {reader["dept"]}");
            }
        }

        public static void Update(Employee em)
        {
            using MySqlConnection mysqlcon = new MySqlConnection(connectionString);
            mysqlcon.Open();

            string query = "UPDATE Employee SET dept=@dpt WHERE empid=@empid";
            MySqlCommand cmd = new MySqlCommand(query, mysqlcon);
            cmd.Parameters.AddWithValue("@dpt", em.Department);
            cmd.Parameters.AddWithValue("@empid", em.EmpId);

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine(rowsAffected > 0 ? "Updated Successfully!" : "Employee not found.");
        }

        public static void Delete(int id)
        {
            using MySqlConnection mysqlcon = new MySqlConnection(connectionString);
            mysqlcon.Open();

            string query = "DELETE FROM Employee WHERE empid=@empid";
            MySqlCommand cmd = new MySqlCommand(query, mysqlcon);
            cmd.Parameters.AddWithValue("@empid", id);

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine(rowsAffected > 0 ? "Deleted Successfully!" : "Employee not found.");
        }
    }
}

