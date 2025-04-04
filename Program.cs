using CtsDBFirstEF.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        
        CtsContext context = new CtsContext();
        string option;
        do
        {
            Console.WriteLine("Enter operation to be performed:\n 1.Create\n2.Read\n3.Update\n4.Delete");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Insert(context);
                    break;
                case 2:
                    Read(context);
                    break;
                case 3:
                    Update(context);
                    break;
                case 4:
                    Delete(context);
                    break;
                default:
                    Console.WriteLine("Enter the correct choice");
                    break;
            }
            Console.WriteLine("Do you want to continue(yes/no):");
            option = Console.ReadLine();
        } while (option.ToLower().Equals("yes"));      

    }
    public static void Read(CtsContext context)
    {
        foreach (Customer cus in context.Customers)
        {
            Console.WriteLine(cus.Custid + " " + cus.Custname + " " + cus.Custloc);
        }
    }
    public static void Insert(CtsContext context)
    {
        Console.WriteLine("Enter customer name,location and age:");
        string name = Console.ReadLine();
        string loc = Console.ReadLine();
        int age = Convert.ToInt32(Console.ReadLine());
        DateOnly orderdate = DateOnly.FromDateTime(DateTime.Now);
        Customer cust = new Customer() { Custname = name, Custloc = loc, Custage = age };
        context.Customers.Add(cust);
        Console.WriteLine("Customer Added");
        context.SaveChanges();//Changes will be made to database
    }

    public static void Update(CtsContext context)
    {
        Console.WriteLine("Enter the CustomerID to be updated:");
        int id = Convert.ToInt32(Console.ReadLine());
        Customer existingcus = context.Customers.FirstOrDefault(x => x.Custid == id);
        Console.WriteLine("Enter the Location to be updated");
        string newlocation = Console.ReadLine();
        existingcus.Custloc = newlocation;
        context.Customers.Update(existingcus);
        context.SaveChanges();
        Console.WriteLine("Updated");
    }
    public static void Delete(CtsContext context)
    {
        Console.WriteLine("Enter the CustomerID to be deleted:");
        int id = Convert.ToInt32(Console.ReadLine());
        Customer cus = context.Customers.FirstOrDefault(x => x.Custid == id);
        context.Customers.Remove(cus);
        context.SaveChanges();
        Console.WriteLine("Customer Deleted");

    }
}