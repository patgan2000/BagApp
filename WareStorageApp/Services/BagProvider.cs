using BagApp.Data.Entities;
using BagApp.Data.Repositories;
using BagApp.Data.Repositories.Extensions;

namespace BagApp.Services
{
    public class BagProvider : IBagProvider
    {
        private readonly IRepository<Bag> _bagRepository;

        public BagProvider(IRepository<Bag> bagRepository)
        {
            _bagRepository = bagRepository;
        }


        public void AddBags()
        {
            var bag = new Bag[]
            {
                new Bag{Name = "Arli", Brand = "Gucci", Year = 2017, Price = 1700},
                new Bag{Name = "Square monogram", Brand = "Gucci", Year = 2021, Price = 2700},
                new Bag{Name = "Neverfull", Brand = "Louis Vuitton", Year = 2008, Price = 1300},
                new Bag{Name = "Boy", Brand = "Chanel", Year = 2012, Price = 4700},
                new Bag{Name = "Star", Brand = "Cloe", Year = 2011, Price = 1000},
                new Bag{Name = "Pochette", Brand = "Louis Vuitton", Year = 2019, Price = 900},
                new Bag{Name = "Cappucines", Brand = "Louis Vuitton", Year = 2016, Price = 2200},
            };
            _bagRepository.AddBatch(bag);
            _bagRepository.Save();
        }
    }
}
