using Accord.IO;
using BagApp.Components.CsvReader;
using BagApp.Entities;
using BagApp.Repositories;

namespace BagApp.Services
{
    public class DataSqlProvider : IDataSqlProvider
    {
        private readonly ICsvReader _csvReader;
        private readonly IRepository<Bag> _bagRepository;

        public DataSqlProvider(ICsvReader csvReader, IRepository<Bag> bagRepository)
        {
            _csvReader = csvReader;
            _bagRepository = bagRepository;
        }

        public void InsertDataToSql()
        {
            var bags = _csvReader.ProcessBags("Resources\\bag.csv");

            foreach (var bag in bags)
            {
                bool bagExists = _bagRepository.GetAll().Any(b => b.Name == bag.Name && b.Brand == bag.Brand);

                if (!bagExists)
                {
                    _bagRepository.Add(new Bag()
                    {
                        Name = bag.Name,
                        Brand = bag.Brand,
                        Year = bag.Year,
                        Price = bag.Price,
                    });
                }
            }

            _bagRepository.Save();
        }
    }
}