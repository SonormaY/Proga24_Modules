using SuperDuperMenuLib;
namespace module2;

public class Program
{
    public static void Main()
    {
        Order order = new Order();
        order.OrderStatusChanged += OrderStatusChanged;

        var menu = new SuperDuperMenu();
        menu.AddEntry("Add product", () => {
            Console.WriteLine("Enter product name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter product price:");
            decimal price = decimal.Parse(Console.ReadLine());
            try
            {
                order.AddProduct(new Product(name, price));
            }
            catch (OrderException ex)
            {
                Console.WriteLine(ex.Message);
            }
        });

        menu.AddEntry("Change status", () => {
            Console.WriteLine("Enter new status:");
            string status = Console.ReadLine();
            order.ChangeStatus(status);
        });
        
        menu.AddEntry("Remove product", () => {
            Console.WriteLine("Enter product name:");
            string name = Console.ReadLine();
            try
            {
                order.RemoveProduct(name);
            }
            catch (OrderException ex)
            {
                Console.WriteLine(ex.Message);
            }
        });

        menu.AddEntry("Clear order", () => {
            order.ClearOrder();
        });

        menu.AddEntry("Print order", () => {
            order.PrintOrder();
        });

        menu.AddEntry("Get total price", () => {
            Console.WriteLine($"Total price: {order.GetTotalPrice()}");
        });

        menu.Run();
    }

    private static void OrderStatusChanged(string status)
    {
        Console.WriteLine($"Order status changed: {status}");
    }
}