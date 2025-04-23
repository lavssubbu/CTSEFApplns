using MySql.Data.MySqlClient;

internal class Program
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
                        Console.Write("Enter Employee Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter Department: ");
                        string dept = Console.ReadLine();
                        Insert(name, dept);
                        break;

                    case 2:
                        Fetch();
                        break;

                    case 3:
                        Console.Write("Enter Employee ID to update: ");
                        int updateId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter New Department: ");
                        string newDept = Console.ReadLine();
                        Update(updateId, newDept);
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

        public static void Insert(string name, string dept)
        {
            using MySqlConnection mysqlcon = new MySqlConnection(connectionString);
            mysqlcon.Open();

            string query = "INSERT INTO Employee(empname, dept) VALUES(@nm, @dp)";
            MySqlCommand cmd = new MySqlCommand(query, mysqlcon);
            cmd.Parameters.AddWithValue("@nm", name);
            cmd.Parameters.AddWithValue("@dp", dept);

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

        public static void Update(int id, string newDept)
        {
            using MySqlConnection mysqlcon = new MySqlConnection(connectionString);
            mysqlcon.Open();

            string query = "UPDATE Employee SET dept=@dpt WHERE empid=@empid";
            MySqlCommand cmd = new MySqlCommand(query, mysqlcon);
            cmd.Parameters.AddWithValue("@dpt", newDept);
            cmd.Parameters.AddWithValue("@empid", id);

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
