using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    private static List<Company> _companies;
    private static User _user;

    static async Task Main(string[] args)
    {
        _user = new User("John Doe", "12345", 1000.00m); // Example user

        _companies = new List<Company>
        {
            new Google(),
            new Facebook()
        };

        Console.WriteLine("Stock Alert System Started. Type 'check' to check stock prices or 'exit' to quit:");

        while (true)
        {
            string input = Console.ReadLine();
            if (input.Trim().ToLower() == "check")
            {
                await CheckStockPrices();
            }
            else if (input.Trim().ToLower() == "exit")
            {
                break;
            }
            else
            {
                Console.WriteLine("Unknown command. Type 'check' to check prices or 'exit' to quit.");
            }
        }
    }

    private static async Task CheckStockPrices()
    {
        foreach (var company in _companies)
        {
            decimal currentPrice = company.GetCurrentStockPrice();
            Console.WriteLine($"{company.Name} stock: {currentPrice}");

            if (currentPrice < 15)
            {
                BuyStock(company, currentPrice);
            }
            else if (currentPrice > 20)
            {
                SellStock(company, currentPrice);
            }
        }
    }

    private static void BuyStock(Company company, decimal price)
    {
        if (_user.Balance >= price)
        {
            _user.Balance -= price;
            Console.WriteLine($"Bought 1 share of {company.Name} at {price}. New balance: {_user.Balance}");
        }
        else
        {
            Console.WriteLine($"Insufficient balance to buy {company.Name} at {price}.");
        }
    }

    private static void SellStock(Company company, decimal price)
    {
        Console.WriteLine($"Sold 1 share of {company.Name} at {price}. New balance: {_user.Balance + price}");
        _user.Balance += price; // Add the price back to balance
    }
}

abstract class Company
{
    public string Name { get; set; }
    private static Random _random = new Random();

    public abstract decimal GetCurrentStockPrice();

    protected decimal GenerateRandomPrice()
    {
        return _random.Next(10, 30); // Random price between 10 and 30
    }
}

class Google : Company
{
    public Google()
    {
        Name = "Google";
    }

    public override decimal GetCurrentStockPrice()
    {
        return GenerateRandomPrice();
    }
}

class Facebook : Company
{
    public Facebook()
    {
        Name = "Facebook";
    }

    public override decimal GetCurrentStockPrice()
    {
        return GenerateRandomPrice();
    }
}

class User
{
    public string Name { get; }
    public string ID { get; }
    public decimal Balance { get; set; }

    public User(string name, string id, decimal balance)
    {
        Name = name;
        ID = id;
        Balance = balance;
    }
}
