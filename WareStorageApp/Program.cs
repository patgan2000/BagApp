using WareStorageApp.Data;
using WareStorageApp.Entities;
using WareStorageApp.Repositories;

var wareRepository = new SqlRepository<Ware>(new WareStorageDbContext());
AddWares(wareRepository);
AddWears(wareRepository);
AddShoes(wareRepository);
WriteAllToConsole(wareRepository);

static void AddWares(IRepository<Ware> wareRepository)
{
    wareRepository.Add(new Ware { Name = "Kostium" });
    wareRepository.Add(new Ware { Name = "Czapka" });
    wareRepository.Save();
}

static void AddShoes(IWriteRepository<Shoe> shoeRepository)
{
    shoeRepository.Add(new Shoe { Name = "Nike mercurial" });
    shoeRepository.Add(new Shoe { Name = "Adidas zero" });
    shoeRepository.Save();
}

static void AddWears(IWriteRepository<Wear> wearRepository)
{
    wearRepository.Add(new Wear { Name = "Bluza" });
    wearRepository.Add(new Wear { Name = "Spodnie" });
    wearRepository.Save();
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var wares = repository.GetAll();
    foreach (var ware in wares)
    {
        Console.WriteLine(ware);
    }
}