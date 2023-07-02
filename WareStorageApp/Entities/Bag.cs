using System.ComponentModel.DataAnnotations;

namespace BagApp.Entities
{
    public class Bag : EntityBase
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public int? Year { get; set; }
        public int? Price { get; set; }

        

        public override string ToString() => $"{Id},{Brand} {Name},\n" +
                                             $" \n   Year: {Year}" +
                                             $" \n   Price: {Price}";


    }
}
