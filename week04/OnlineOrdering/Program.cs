using System;
using System.Collections.Generic;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        // Create addresses
        Address addr1 = new Address("123 Main St", "Springfield", "IL", "USA");
        Address addr2 = new Address("456 Market Rd", "Toronto", "ON", "Canada");

        // Create customers
        Customer cust1 = new Customer("John Doe", addr1);
        Customer cust2 = new Customer("Maria Silva", addr2);

        // Create products for first order (customer in USA)
        Product p1 = new Product("Water Bottle", "WB-100", 10.50, 2);
        Product p2 = new Product("T-Shirt", "TS-200", 15.00, 1);

        Order order1 = new Order(cust1);
        order1.AddProduct(p1);
        order1.AddProduct(p2);

        // Create products for second order (customer outside USA)
        Product p3 = new Product("Mug", "MG-300", 8.75, 3);
        Product p4 = new Product("Notebook", "NB-400", 12.00, 2);

        Order order2 = new Order(cust2);
        order2.AddProduct(p3);
        order2.AddProduct(p4);

        // Optionally add a third order with 2-3 products
        Product p5 = new Product("Sticker Pack", "SP-500", 4.00, 5);
        Product p6 = new Product("Cap", "CP-600", 20.00, 1);
        Order order3 = new Order(cust1);
        order3.AddProduct(p5);
        order3.AddProduct(p6);

        List<Order> orders = new List<Order> { order1, order2, order3 };

        // Display each order's packing label, shipping label, and total price
        foreach (var order in orders)
        {
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine();
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine();
            double total = order.CalculateTotalPrice();
            Console.WriteLine($"Total Price: {total.ToString("C2", CultureInfo.CreateSpecificCulture("en-US"))}");
            Console.WriteLine(new string('-', 40));
        }
    }
}
