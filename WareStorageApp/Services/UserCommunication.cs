﻿using BagApp.Data;
using BagApp.Data.Entities;
using BagApp.Data.Repositories;

namespace BagApp.Services
{
    public class UserCommunication : IUserCommunication
    {
        private readonly IRepository<Bag> _bagRepository;
        private readonly BagAppDbContext _bagDbContext;

        public UserCommunication(
            IRepository<Bag> bagRepository,
            BagAppDbContext bagDbContext)
        {
            _bagRepository = bagRepository;
            _bagDbContext = bagDbContext;
        }

        public void ChooseOption()
        {
            while (true)
            {
                Console.WriteLine("\n1. Add new bag");
                Console.WriteLine("2. Remove bag");
                Console.WriteLine("3. Get all bags");
                Console.WriteLine("4. Get bag by ID");
                Console.WriteLine("5. Edit bag");
                Console.WriteLine("6. Search bag");
                Console.WriteLine("Q. Quit the program\n");
                Console.Write("Your choice: ");
                var choice = Console.ReadLine();
                Console.WriteLine(" ");
                switch (choice)
                {
                    case "1":
                        AddNewBag(_bagRepository); break;
                    case "2":
                        RemoveBag(_bagRepository); break;
                    case "3":
                        WriteAllToConsole(_bagRepository); break;
                    case "4":
                        GetByID(_bagRepository); break;
                    case "5":
                        EditBag(_bagRepository); break;
                    case "6":
                        SearchBag(_bagRepository); break;
                    case "q":
                    case "Q":
                        return;
                    default:
                        Console.WriteLine("Wrong char. Try again");
                        break;
                }
            }
        }

        static void AddNewBag(IRepository<Bag> newBag)  //1
        {
            Console.Write("Name: ");
            var name = Console.ReadLine();
            Console.Write("Brand: ");
            var brand = Console.ReadLine();
            Console.Write("Year of production: ");
            int year;
            while (!int.TryParse(Console.ReadLine(), out year))
            {
                Console.WriteLine("Invalid input. Please enter a valid year.");
                Console.Write("Year of production: ");
            }
            Console.Write("Purchase cost: ");
            int price;
            while (!int.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("Invalid input. Please enter a valid price.");
                Console.Write("Purchase cost: ");
            }

            var bag = new Bag
            {
                Name = name,
                Brand = brand,
                Year = year,
                Price = price
            };

            newBag.Add(bag);
            newBag.Save();
        }

        static void RemoveBag(IRepository<Bag> removeBag)   //2
        {
            removeBag.Read();
            Console.Write("Put an ID of bag to remove: ");
            var id = int.Parse(Console.ReadLine());
            var bag = removeBag.GetById(id);
            removeBag.Remove(bag);
            removeBag.Save();
        }

        static void WriteAllToConsole<T>(IRepository<T> bagRepository) where T : class, IEntity //3
        {
            var bags = bagRepository.Read();

            foreach (var bag in bags)
            {
                Console.WriteLine(bag);
                Console.WriteLine();
            }
        }

        static void GetByID(IRepository<Bag> showBag)   //4
        {
            showBag.Read();
            Console.Write("Put an ID of bag to show: ");
            var id = int.Parse(Console.ReadLine());
            var bag = showBag.GetById(id);
            Console.WriteLine($"{bag.Name},{bag.Brand},{bag.Year},{bag.Price}");
        }

        static void EditBag(IRepository<Bag> bagRepository)     //5
        {
            Console.WriteLine("Enter the ID of bag to edit:");
            var idToEdit = Console.ReadLine();
            if (int.TryParse(idToEdit, out var result))
            {
                var itemToShow = bagRepository.GetById(result);
                if (itemToShow != null)
                {
                    Console.WriteLine($"Bag to edit: {itemToShow.ToString()}");
                    Console.WriteLine("What would you like edit?");
                    Console.WriteLine("N - name; B - brand; Y - year; P - price;");
                    var editChoose = Console.ReadLine();

                    if (editChoose == "n" || editChoose == "N")
                    {
                        Console.Write("Write new name of bag: ");
                        var editedName = Console.ReadLine();
                        itemToShow.Name = editedName;
                    }
                    else if (editChoose == "b" || editChoose == "B")
                    {
                        Console.Write("Write new brand of bag: ");
                        var editedBrand = Console.ReadLine();
                        itemToShow.Brand = editedBrand;
                    }
                    else if (editChoose == "y" || editChoose == "Y")
                    {
                        Console.Write("Write new year of bag: ");
                        var editedYear = Console.ReadLine();
                        if (int.TryParse(editedYear, out var edY))
                        {
                            itemToShow.Year = edY;
                        }
                        else
                        {
                            throw new Exception("This is not integer!");
                        }

                    }
                    else if (editChoose == "p" || editChoose == "P")
                    {
                        Console.Write("Write new year of bag: ");
                        var editedPrice = Console.ReadLine();
                        if (int.TryParse(editedPrice, out var edP))
                        {
                            itemToShow.Price = edP;
                        }
                        else
                        {
                            throw new Exception("This is not integer!");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("ID not found");
                }
            }
            else
            {
                Console.WriteLine("ID not found");
            }
        }

        static void SearchBag(IRepository<Bag> bagRepository)       //6
        {
            Console.WriteLine("Search bag by:");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Brand");
            Console.WriteLine("3. Year");
            Console.Write("Your choice: ");
            var searchChoice = Console.ReadLine();

            string searchTerm;
            switch (searchChoice)
            {
                case "1":
                    Console.Write("Enter the name to search: ");
                    searchTerm = Console.ReadLine();
                    var bagsByName = bagRepository.GetAll().Where(b => b.Name.Contains(searchTerm));
                    WriteBagsToConsole(bagsByName);
                    break;
                case "2":
                    Console.Write("Enter the brand to search: ");
                    searchTerm = Console.ReadLine();
                    var bagsByBrand = bagRepository.GetAll().Where(b => b.Brand.Contains(searchTerm));
                    WriteBagsToConsole(bagsByBrand);
                    break;
                case "3":
                    Console.Write("Enter the year to search: ");
                    if (int.TryParse(Console.ReadLine(), out var searchYear))
                    {
                        var bagsByYear = bagRepository.GetAll().Where(b => b.Year == searchYear);
                        WriteBagsToConsole(bagsByYear);
                    }
                    else
                    {
                        Console.WriteLine("Invalid year format.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }


            static void WriteBagsToConsole(IEnumerable<Bag> bags)
            {
                if (bags.Any())
                {
                    Console.WriteLine("Search results:");
                    foreach (var bag in bags)
                    {
                        Console.WriteLine($"{bag.Id}, {bag.Name}, {bag.Brand}, {bag.Year}, {bag.Price}");
                    }
                }
                else
                {
                    Console.WriteLine("No bags found.");
                }
            }
        }
    }
}