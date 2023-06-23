using BagApp.Components.CsvReader.Models;

namespace BagApp.Components.CsvReader.Extensions
{
    public static class BagExtensions
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
                    Price = decimal.Parse(columns[3])
                };

            }
        }

    }
}
