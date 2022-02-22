using EFExamples;
using EFExamples.Models;
using Microsoft.EntityFrameworkCore;

List<User> InitialUsers = new List<User>
{
    new User { Name = "Pesho" },
    new User { Name = "Gosho" }
};

List<Product> InitialProducts = new List<Product>
{
    new Product { Name = "Cucumber", Price = 69.666m,  Quantity = 10 },
    new Product { Name = "Tomato", Price = 5m, Quantity = 100 }
};

string connString = "Server=., 1433;User Id=sa;Password=AzftT4PUiBTVET;Database=GroceryShop";

var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseSqlServer(connString)
    .Options;

using var context = new ApplicationDbContext(contextOptions);

if (!context.Users.Any())
{
    await context.Users.AddRangeAsync(InitialUsers);
}

if (!context.Products.Any())
{
    await context.Products.AddRangeAsync(InitialProducts);
}

await context.SaveChangesAsync();

var expensiveProducts = context.Products.Where(p => p.Price > 50).ToList();

var expensiveProductsNames = expensiveProducts.Select(p => p.Name);

Console.WriteLine($"Expensive products (price above 50): {string.Join(", ", expensiveProductsNames)}");

var shoppingCarts = context.ShoppingCarts.ToList();

var firstUser = context.Users.First();

if (firstUser.ShoppingCart == null)
{
    firstUser.ShoppingCart = new ShoppingCart { Products = expensiveProducts.Select(p => new ProductShoppingCart { ProductId = p.Id, Quantity = 1 }).ToList() };
    context.Users.Update(firstUser);
    context.SaveChanges();
}