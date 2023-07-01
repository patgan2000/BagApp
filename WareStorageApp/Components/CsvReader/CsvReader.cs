using BagApp.Components.CsvReader.Extensions;
using BagApp.Components.Models;

namespace BagApp.Components.CsvReader
{
    public class CsvReader : ICsvReader
    {
        public List<Bag> ProcessBags(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Bag>();
            }

            var bags = File.ReadAllLines(filePath)
                .Where(x => x.Length > 0)
                .ToBag();

            return bags.ToList();
        }

        public List<Shop> ProcessShops(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Shop>();
            }

            var shops = File.ReadAllLines(filePath)
                .Where(x => x.Length > 1)
                .ToShop();

            return shops.ToList();
        }
    }
}
