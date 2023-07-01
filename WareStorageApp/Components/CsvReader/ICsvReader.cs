using BagApp.Components.Models;

namespace BagApp.Components.CsvReader
{
    public interface ICsvReader
    {
        List<Bag> ProcessBags(string filePath);

        List<Shop> ProcessShops(string filePath);
    }
}
