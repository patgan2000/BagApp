namespace WareStorageApp.Entities
{
    public class Ware : EntityBase
    {
        public Ware() 
        {
        }

        public string? Name { get; set; }

        public override string ToString() => $"ID {Id}, Name {Name}";



    }
}
