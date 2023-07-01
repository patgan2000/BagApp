using Microsoft.EntityFrameworkCore;

namespace BagApp.Entities
{
    public class Bag : EntityBase
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public int? Year { get; set; }
        public int? Price { get; set; }

        public override string ToString() => $"{Id},{Brand} {Name},\n" +
                                             $" \n   Year: {Year}" +
                                             $" \n   Price: {Price}";


    }
}
