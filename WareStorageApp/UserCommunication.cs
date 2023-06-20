using Microsoft.Extensions.Primitives;
using System.Text;
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
        }
                
        public string BeginProgram()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Welcome to [BagApp]!");
            sb.AppendLine("List of available actions:");
            sb.AppendLine("(1) Add a new bag");
            sb.AppendLine("(2) Remove already exists bag");
            sb.AppendLine("(3) Read the current list of bags");
            sb.AppendLine("(Q) End the session");
            sb.AppendLine("Choose an action: 1,2,3 or Q:");
            return sb.ToString();
        }

        public string UserChoose()
        {
            var userChoose = Console.ReadLine();
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
            var yearOfBag = decimal.Parse(Console.ReadLine());
            Console.Write("Purchase cost: ");
            var costOfBag = decimal.Parse(Console.ReadLine());

            bag.Name = nameOfBag;
            bag.Brand = brandOfBag;
            bag.Type = typeOfBag;
            bag.Year = yearOfBag;
            bag.Cost = costOfBag;
        }
                
        public void RemoveBagById()
        {
            Console.Write("Enter the ID of the bag to remove:");
            var idToRemove = Console.ReadLine();
            if(int.TryParse(idToRemove, out var result))
            {
                var itemToRemove = _bagsRepository.GetById(result);
                if(itemToRemove != null)
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
                Console.WriteLine("ID is not integer!");
            }
        }

        public void GetAllBags()
        {
            var list = _bagsRepository.GetAll();
            if(list.Count() != 0)
            {
                foreach(var item in list)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            else
            {
                Console.WriteLine("The base is empty");
            }
        }

        public void GetBagById()
        {
            Console.WriteLine("Put Id of Bag:");
            var idToShow = Console.ReadLine();
            if(int.TryParse(idToShow, out var result))
            {
                var itemToShow = _bagsRepository.GetById(result);
                if(itemToShow != null)
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
                Console.WriteLine("ID is not integer!");
            }
        }
    }
}
