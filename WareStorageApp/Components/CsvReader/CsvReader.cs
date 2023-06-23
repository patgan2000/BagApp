using BagApp.Components.CsvReader.Extensions;
using BagApp.Components.CsvReader.Models;

namespace BagApp.Components.CsvReader
{
    public class CsvReader : ICsvReader
    {

        public List<Bag> ProcessBags(string filePath)
        {
            if (File.Exists(filePath))
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
            if (File.Exists(filePath))
            {
                return new List<Shop>();
            }

            var shops = File.ReadAllLines(filePath)
                .Where(x => x.Length > 0)
                .Select(x =>
                {
                    var columns = x.Split(',');
                    return new Shop()
                    {
                        Brand = columns[0],
                        Site = columns[1],
                        Phone = double.Parse(columns[2]),
                    };
                });
            return shops.ToList();
        }


    }
}
