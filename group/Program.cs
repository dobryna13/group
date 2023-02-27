using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Підключення до бази даних
        using (var context = new MyDbContext())
        {
            // Отримуємо список продуктів з бази даних
            List<Product> products = context.Products.ToList();

            // Групуємо продукти за категорією
            var groupedProducts = products.GroupBy(p => p.Category);

            // Виводимо список груп продуктів та їхні елементи
            foreach (var group in groupedProducts)
            {
                Console.WriteLine("Category: {0}", group.Key);

                foreach (var product in group)
                {
                    Console.WriteLine("- Name: {0}, Price: {1}", product.Name, product.Price);
                }

                Console.WriteLine();
            }
        }
    }
}

// Клас продукту
class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}

// Клас контексту бази даних
class MyDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;");
    }
}
