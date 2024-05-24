using System.Xml;
using System.Xml.Linq;

[Serializable]
public class Employee
{
    public string Name { get; set; }
    public string Position { get; set; }
    public DateTime HireDate { get; set; }

    public Employee(string name, string position, DateTime hireDate)
    {
        Name = name;
        Position = position;
        HireDate = hireDate;
    }

    public override string ToString()
    {
        return $"Name: {Name} Position: {Position} HireDate: {HireDate}";
    }

}

static class Program
{
    static void Main(string[] args)
    {
        Console.Clear();

        var employees = GetEmployees("employees.xml");
        Console.WriteLine("Employees:");
        PrintEmployees(employees);

        var sortedEmployees = employees.OrderBy(e => e.HireDate).ToList();
        Console.WriteLine("Sorted employees:");
        PrintEmployees(sortedEmployees);

        WriteToXML(sortedEmployees, "sorted_employees.xml");
        WriteToTXT(sortedEmployees, "employees.txt");
    }

    static List<Employee> GetEmployees(string path)
    {
        var employees = new List<Employee>();
        var xml = new XmlDocument();
        xml.Load(path);
        foreach (XmlNode node in xml.DocumentElement)
        {
            var name = node["Name"].InnerText;
            var position = node["Position"].InnerText;
            var hireDate = DateTime.Parse(node["HireDate"].InnerText);
            employees.Add(new Employee(name, position, hireDate));
        }
        return employees;
    }
    static void WriteToXML(List<Employee> employees, string path)
    {
        var xml = new XElement("Employees",
            employees.Select(employee => new XElement("Employee",
                new XElement("Name", employee.Name),
                new XElement("Position", employee.Position),
                new XElement("HireDate", employee.HireDate.ToString())
            ))
        );
        xml.Save(path);
    }
    static void WriteToTXT(List<Employee> employees, string path)
    {
        using (var writer = new StreamWriter(path))
        {
            foreach (var employee in employees)
            {
                writer.WriteLine(employee);
            }
        }
    }
    static void PrintEmployees(IEnumerable<Employee> employees)
    {
        foreach (var employee in employees)
        {
            Console.WriteLine(employee);
        }
    }
}