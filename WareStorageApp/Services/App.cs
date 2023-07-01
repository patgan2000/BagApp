using BagApp.Components.CsvReader;
using BagApp.Data;
using BagApp.Entities;
using BagApp.Repositories;

namespace BagApp.Services
{
    public class App : IApp
    {
        private readonly IUserCommunication _userCommunication;
        private readonly IBagProvider _bagProvider;
        private readonly IEventHandelerService _eventHandeler;
        private readonly ICsvReader _csvReader;
        private readonly BagAppDbContext _bagDbContext;
        

        public App(IUserCommunication userComunnication,
            IBagProvider bagProvider,
            IEventHandelerService eventHandeler,
            ICsvReader csvReader,
            BagAppDbContext bagDbContext)
        {
            _userCommunication = userComunnication;
            _bagProvider = bagProvider;
            _eventHandeler = eventHandeler;
            _csvReader = csvReader;
            _bagDbContext = bagDbContext;
            _bagDbContext.Database.EnsureCreated();
        }

        public void Run()
        {
            Console.WriteLine("Welcome to [BagApp]!");
            Console.WriteLine("----------------------------------------------------------");

            InsertDataToSql();
            _bagProvider.AddBags();
            _eventHandeler.EventHandlerForList();
            _userCommunication.ChooseOption();
        }

        private void InsertDataToSql()
        {
            var bags = _csvReader.ProcessBags("Resources\\bag.csv");
            foreach (var bag in bags)
            {
                bool bagExists = _bagDbContext.Bags.Any(b => b.Name == bag.Name && b.Brand == bag.Brand);

                if (!bagExists)
                {
                    _bagDbContext.Add(new Bag
                    {
                        Name = bag.Name,
                        Brand = bag.Brand,
                    });
                }
            }

            _bagDbContext.SaveChanges();
        }
    }
}