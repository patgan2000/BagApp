using BagApp.Components.Models;

namespace BagApp.Components.CsvReader.Extensions
{
    public static class ShopExtension
    {
        public static IEnumerable<Shop> ToShop(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');

                yield return new Shop
                {
                    Name = columns[0],
                    City = columns[1],
                };
            }
        }
    }
}
