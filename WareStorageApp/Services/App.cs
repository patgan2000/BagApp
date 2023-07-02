using BagApp.Components.CsvReader;
using BagApp.Data;
using BagApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.SqlServer;
using BagApp.Repositories;

namespace BagApp.Services
{
    public class App : IApp
    {
        private readonly IUserCommunication _userCommunication;
        private readonly IBagProvider _bagProvider;
        private readonly IEventHandelerService _eventHandeler;
        private readonly ICsvReader _csvReader;
        private readonly IRepository<Bag> _bagRepository;
        private readonly BagAppDbContext _bagDbContext;


        public App(IUserCommunication userComunnication,
            IBagProvider bagProvider,
            IEventHandelerService eventHandeler,
            ICsvReader csvReader,
            BagAppDbContext bagDbContext,
            IRepository<Bag> bagRepository)
        {
            _userCommunication = userComunnication;
            _bagProvider = bagProvider;
            _eventHandeler = eventHandeler;
            _csvReader = csvReader;
            _bagDbContext = bagDbContext;
            _bagRepository = bagRepository;
            _bagDbContext.Database.EnsureCreated();
        }

        public void Run()
        {
            Console.WriteLine("Welcome to [BagApp]!");
            Console.WriteLine("----------------------------------------------------------");

            CreateBagsTable();

            InsertDataToSql();
            _bagProvider.AddBags();
            _eventHandeler.EventHandlerForList();
            _userCommunication.ChooseOption();
        }

        private void CreateBagsTable()
        {
            var bag = new Bag
            {
                Name = "Sample Bag",
                Brand = "Sample Brand",
                Year = 2023,
                Price = 100
            };

            _bagRepository.Add(bag);
            _bagRepository.Save();
        }

        private void InsertDataToSql()
        {
            var bags = _csvReader.ProcessBags("Resources\\bag.csv");

            foreach (var bag in bags)
            {
                bool bagExists = _bagDbContext.Bags.Any(b => b.Name == bag.Name && b.Brand == bag.Brand);

                _bagDbContext.Add(new Bag()
                {
                    Name = bag.Name,
                    Brand = bag.Brand,
                    Year = bag.Year,
                    Price = bag.Price,
                });

                //if (!bagExists)
                //{
                //    _bagDbContext.Add(new Bag()
                //    {
                //        Name = bag.Name,
                //        Brand = bag.Brand,
                //        Year = bag.Year,
                //        Price = bag.Price,
                //    });
                //}
            }

            _bagDbContext.SaveChanges();
        }        
    }
}