using System.ComponentModel.DataAnnotations;

namespace BagApp.Components.Models
{
    public class Bag
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public int? Year { get; set; }
        public int? Price { get; set; }

        [Key]
        public int Id { get; set; }
    }
}
