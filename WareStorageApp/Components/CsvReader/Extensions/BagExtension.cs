using BagApp.Components.Models;

namespace BagApp.Components.CsvReader.Extensions
{
    public static class BagExtension
    {
        public static IEnumerable<Bag> ToBag(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');

                yield return new Bag
                {
                    Name = columns[0],
                    Brand = columns[1],
                    Year = int.Parse(columns[2]),
                    Price = int.Parse(columns[3]),
                };
            }
        }
    }
}
