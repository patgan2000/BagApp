using System.Drawing;
using System.Text;
using System.Collections.Generic;

namespace WareStorageApp.Entities
{
    public class Bag : EntityBase
    {
        public string Name { get; set; }
        public string Brand { get; set; }

        public string Type { get; set; }    
        public decimal? Year { get; set; }

        public int? NameLength { get; set; }

        public decimal? Cost { get; set; }

        #region ToString override

        public override string ToString()
        {
            StringBuilder sb = new(1024);
            sb.AppendLine($"Category: {Type}");
            sb.AppendLine($"{Name}, ID: {Id}");
            sb.AppendLine($" Brand: {Brand}, Year of production: {Year}");
            if (NameLength.HasValue)
            {
                sb.AppendLine($" Name Length: {NameLength}");
            }
            if (Cost.HasValue)
            {
                sb.AppendLine($" Purchase cost: {Cost:c}");

            }
            return sb.ToString();
        }
        #endregion

        
    }
}
