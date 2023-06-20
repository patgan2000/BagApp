using System.Xml.Linq;
using WareStorageApp.DataProvides;
using WareStorageApp.Entities;
using WareStorageApp.Repositories;

namespace WareStorageApp
{
    public class App : IApp
    {
        private readonly IRepository<Bag> _bagsRepository;

        public IBagsProvider _bagsProvider { get; }

        public App(
            IRepository<Bag> bagRepository,
            IBagsProvider bagsProvider)
        {
            _bagsRepository = bagRepository;
            _bagsProvider = bagsProvider;

        }

        public void Run() 
        {
            Console.WriteLine("I'm here in Run() method");

            var bags = GenerateSampleBags();

            foreach (var bag in bags)
            {
                _bagsRepository.Add(bag);
            }
            _bagsRepository.Save();

            //var items = _bagsRepository.GetAll();

            //foreach (var item in items)
            //{
            //    Console.WriteLine(item);
            //}
            Console.WriteLine();
            Console.WriteLine("Top of 5 the cheapest bags in the collection:");
            foreach(var bag in _bagsProvider.Take5CheapestBags())
            {
                Console.Write(bag);
            }

            Console.WriteLine();
            Console.WriteLine("Top of 5 the most expensive bags in the collection:");
            foreach (var bag in _bagsProvider.Take5ExpensiveBags())
            {
                Console.Write(bag);
            }

            Console.WriteLine();
            Console.WriteLine("Bags without purchase price:");
            foreach (var bag in _bagsProvider.WhereCostIsEmpty())
            {
                Console.Write(bag);
            }
        }

        public static List<Bag> GenerateSampleBags()
        {
            return new List<Bag>
            {
                new Bag
                {
                    Id = 98,
                    Name = "Sylvie",
                    Brand = "Gucci",
                    Year = 2019,
                    Cost = 1400,
                    Type = "Shoulder bag"
                },

                new Bag
                {
                    Id = 101,
                    Name = "Dionysus",
                    Brand = "Gucci",
                    Year = 2020,
                    Cost = 2800,
                    Type = "Shoulder bag"
                },

                new Bag
                {
                    Id = 22,
                    Name = "Marmont",
                    Brand = "Gucci",
                    Year = 2023,
                    Cost = 2950,
                    Type = "Shoulder bag"
                },

                new Bag
                {
                    Id = 55,
                    Name = "Soho Disco",
                    Brand = "Gucci",
                    Year = 2022,
                    Cost = null,
                    Type = "Shoulder bag"
                },

                new Bag
                {
                    Id = 25,
                    Name = "Blooms",
                    Brand = "Gucci",
                    Year = 2016,
                    Cost = 1600,
                    Type = "Backpack"
                },

                new Bag
                {
                    Id = 1,
                    Name = "Neverfull",
                    Brand = "Louis Vuitton",
                    Year = 2000,
                    Cost = 2700,
                    Type = "Tote Bag"
                },

                new Bag
                {
                    Id = 64,
                    Name = "Pochette",
                    Brand = "Louis Vuitton",
                    Year = 2001,
                    Cost = 900,
                    Type = "Clutch"
                },

                new Bag
                {
                    Id = 27,
                    Name = "Speedy",
                    Brand = "Louis Vuitton",
                    Year = 1996,
                    Cost = 2600,
                    Type = "Shoulder bag"
                },

                new Bag
                {
                    Id = 64,
                    Name = "Capucines",
                    Brand = "Louis Vuitton",
                    Year = 2019,
                    Cost = 1400,
                    Type = "Tote bag"
                },

                new Bag
                {
                    Id = 97,
                    Name = "Neo",
                    Brand = "Louis Vuitton",
                    Year = 2017,
                    Cost = 1800,
                    Type = "Messenger bag"
                },

                new Bag
                {
                    Id = 10,
                    Name = "Classic",
                    Brand = "Chanel",
                    Year = 2008,
                    Cost = 4000,
                    Type = "Shoulder bag"
                },

                new Bag
                {
                    Id = 68,
                    Name = "Timeless",
                    Brand = "Chanel",
                    Year = 2022,
                    Cost = 2400,
                    Type = "Clutch"
                },

                new Bag
                {
                    Id = 6,
                    Name = "Boy",
                    Brand = "Chanel",
                    Year = 2002,
                    Cost = 4500,
                    Type = "Shoulder bag"
                },

                new Bag
                {
                    Id = 88,
                    Name = "Camellia",
                    Brand = "Louis Vuitton",
                    Year = 2012,
                    Cost = 1650,
                    Type = "Clutch"
                },

                new Bag
                {
                    Id = 51,
                    Name = "Caleido",
                    Brand = "Gucci",
                    Year = 2017,
                    Cost = 1200,
                    Type = "Backpack"
                },

                new Bag
                {
                    Id = 20,
                    Name = "Supreme",
                    Brand = "Gucci",
                    Year = 2019,
                    Cost = 750,
                    Type = "Messenger bag"
                },

                new Bag
                {
                    Id = 32,
                    Name = "Arli",
                    Brand = "Gucci",
                    Year = 2019,
                    Cost = 650,
                    Type = "Shoulder bag"
                },
            };
        }
    }
}
