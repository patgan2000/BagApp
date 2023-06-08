namespace WareStorageApp.Entities
{
    public class Other : EntityBase
    {
        public string? Name { get; set; }

        public override string ToString() => $"ID: {Id}, Name: {Name}";
    }
}
