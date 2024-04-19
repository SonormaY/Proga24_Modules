
namespace module2;
public delegate void OrderStatusChangedEventHandler(string status);
public interface IOrder
{
    void AddProduct(Product product);
    void RemoveProduct(string name);
    decimal GetTotalPrice();
}
public class Order : IOrder
{
    private readonly ProductList<Product> products = new ProductList<Product>();
    public event OrderStatusChangedEventHandler OrderStatusChanged;

    private string Status { get; set; }
    protected virtual void OnOrderStatusChanged(string status)
    {
        OrderStatusChanged?.Invoke(status);
    }
    public void ChangeStatus(string status)
    {
        Status = status;
        OnOrderStatusChanged(status);
    }
    public void AddProduct(Product product)
    {
        if (products.ContainsProduct(product.Name))
        {
            throw new OrderException("Product already exists in the order");
        }
        products.AddProduct(product);
    }
    public void RemoveProduct(string name)
    {
        products.RemoveProduct(name);
    }
    public decimal GetTotalPrice()
    {
        decimal totalPrice = 0;
        foreach (Product product in products)
        {
            totalPrice += product.Price;
        }
        return totalPrice;
    }
    public void PrintOrder()
    {
        foreach (Product product in products)
        {
            Console.WriteLine($"{product.Name} - {product.Price}");
        }
    }
    public void ClearOrder()
    {
        products.ClearProducts();
    }
    ~Order()
    {
        ClearOrder();
    }
}
public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}
public class FoodProduct : Product
{
    public DateTime ExpirationDate { get; set; }

    public FoodProduct(string name, decimal price, DateTime expirationDate) : base(name, price)
    {
        ExpirationDate = expirationDate;
    }
}
public class ElectronicProduct : Product
{
    public string Manufacturer { get; set; }

    public ElectronicProduct(string name, decimal price, string manufacturer) : base(name, price)
    {
        Manufacturer = manufacturer;
    }
}
public class ProductList<T> where T : Product
{
    private readonly List<T> products = new List<T>();

    public void AddProduct(T product)
    {
        products.Add(product);
    }
    public void RemoveProduct(string name)
    {
        T product = products.Find(p => p.Name == name);
        if (product == null)
        {
            throw new OrderException("Product not found in the order");
        }
        products.Remove(product);
    }
    public decimal GetTotalPrice()
    {
        decimal totalPrice = 0;
        foreach (T product in products)
        {
            totalPrice += product.Price;
        }
        return totalPrice;
    }
    public void PrintProducts()
    {
        foreach (T product in products)
        {
            Console.WriteLine($"{product.Name} - {product.Price}");
        }
    }
    public void ClearProducts()
    {
        products.Clear();
    }
    public bool ContainsProduct(string name)
    {
        if (products.Find(p => p.Name == name) != null)
        {
            return true;
        }
        return false;
    }
    public IEnumerator<T> GetEnumerator()
    {
        return products.GetEnumerator();
    }
    ~ProductList()
    {
        ClearProducts();
    }
}
public class OrderException : Exception
{
    public OrderException(string message) : base(message) { }
}


