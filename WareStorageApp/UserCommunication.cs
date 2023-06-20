using Microsoft.Extensions.Primitives;
using System.Text;
using WareStorageApp.DataProvides;
using WareStorageApp.Entities;
using WareStorageApp.Repositories;

namespace WareStorageApp
{
    public class UserCommunication : IUserCommunication
    {
        private readonly IRepository<Bag> _bagsRepository;

        public UserCommunication(IRepository<Bag> bagsRepository)
        {
            _bagsRepository = bagsRepository;
            _bagsRepository.BagAdded += BagAddedEvent;
            _bagsRepository.BagRemoved += BagRemovedEvent;
        }

        public static void BagAddedEvent (object? sender, Bag e)
        {
            Console.WriteLine("Bag added successfully!");
        }

        public static void BagRemovedEvent(object? sender, Bag e)
        {
            Console.WriteLine("Bag removed successfully!");
        }

        public string BeginProgram()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Welcome to [BagApp]!");
            sb.AppendLine("List of available actions:");
            sb.AppendLine("(1) Add a new bag");
            sb.AppendLine("(2) Remove an existing bag");
            sb.AppendLine("(3) Show a bag using its ID");
            sb.AppendLine("(4) Read the current list of bags");
            sb.AppendLine("(Q) End the session");
            return sb.ToString();
        }

        public string UserChoose()
        {
            Console.WriteLine("Choose an action: 1, 2, 3, 4, or Q:");
            var userChoose = Console.ReadLine()?.Trim();
            Console.WriteLine();
            return userChoose;
        }

        public void AddNewBag()
        {
            var bag = new Bag();
            Console.Write("Name: ");
            var nameOfBag = Console.ReadLine();
            Console.Write("Brand: ");
            var brandOfBag = Console.ReadLine();
            Console.Write("Type: ");
            var typeOfBag = Console.ReadLine();
            Console.Write("Year of production: ");
            var yearOfBag = decimal.Parse(Console.ReadLine() ?? "0");
            Console.Write("Purchase cost: ");
            var costOfBag = decimal.Parse(Console.ReadLine() ?? "0");

            bag.Name = nameOfBag;
            bag.Brand = brandOfBag;
            bag.Type = typeOfBag;
            bag.Year = yearOfBag;
            bag.Cost = costOfBag;

            _bagsRepository.Add(bag);
        }

        public void RemoveBagById()
        {
            Console.Write("Enter the ID of the bag to remove:");
            var idToRemove = Console.ReadLine();
            if (int.TryParse(idToRemove, out var result))
            {
                var itemToRemove = _bagsRepository.GetById(result);
                if (itemToRemove != null)
                {
                    _bagsRepository.Remove(itemToRemove);
                }
                else
                {
                    Console.WriteLine("ID not found");
                }
            }
            else
            {
                Console.WriteLine("ID is not an integer!");
            }
        }

        public void GetAllBags()
        {
            var list = _bagsRepository.GetAll();
            if (list.Count() != 0)
            {
                foreach (var item in list)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            else
            {
                Console.WriteLine("The database is empty");
            }
        }

        public void GetBagById()
        {
            Console.WriteLine("Enter the ID of the bag:");
            var idToShow = Console.ReadLine();
            if (int.TryParse(idToShow, out var result))
            {
                var itemToShow = _bagsRepository.GetById(result);
                if (itemToShow != null)
                {
                    Console.WriteLine(itemToShow.ToString());
                }
                else
                {
                    Console.WriteLine("ID not found");
                }
            }
            else
            {
                Console.WriteLine("ID is not an integer!");
            }
        }
    }
}