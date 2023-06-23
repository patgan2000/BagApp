using BagApp.Data.Entities;
using System.Text;

namespace BagApp.Components.CsvReader.Models
{
    public class Bag : EntityBase
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public decimal? Price { get; set; }

        public int? NameLength { get; set; }

        #region ToString override

        public override string ToString()
        {
            StringBuilder sb = new(1024);
            sb.AppendLine($"{Name}, ID: {Id}");
            sb.AppendLine($" Brand: {Brand}, Year of production: {Year}");
            if (NameLength.HasValue)
            {
                sb.AppendLine($" Name Length: {NameLength}");
            }
            if (Price.HasValue)
            {
                sb.AppendLine($" Purchase cost: {Price:c}");

            }
            return sb.ToString();
        }
        #endregion


    }
}
