using BagApp;
using BagApp.Components.CsvReader;
using BagApp.Components.CsvReader.Models;
using BagApp.Components.DataProvides;
using BagApp.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Bag>, ListRepository<Bag>>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IBagsProvider, BagsProvider>();
services.AddSingleton<ICsvReader, CsvReader>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();

//Console.WriteLine("*****************************************************************************");
//Console.WriteLine("Welcome to [WareStorageApp]!");
//Console.WriteLine("Using this application, you can enter the history of your fashion purchases.");
//Console.WriteLine("*****************************************************************************");
//Thread.Sleep(2000);
//Console.WriteLine();
//Console.WriteLine("List of available actions:");
//Console.WriteLine("(1) Add a new ware");
//Console.WriteLine("(2) Remove a new ware");
//Console.WriteLine("(3) Read the current list of wares");
//Console.WriteLine("(Q) End the session");

//var wareInFile = "wareFile.json";

//    if (!File.Exists(wareInFile))
//    {
//        using (FileStream fs = File.Create(wareInFile)) ;
//    }

//var wareRepository = new SqlRepository<Ware>(new WareStorageDbContext());
//var wareFile = File.ReadAllLines(wareInFile);

//if (wareFile.Length > 0)
//{
//    foreach (var line in wareFile)
//    {
//        Ware? ware = JsonSerializer.Deserialize<Ware?>(line);
//        wareRepository.Add(ware);
//        wareRepository.Save();
//    }
//}

//wareRepository.WareAdded += WareAddedHandler;
//wareRepository.WareRemoved += WareRemovedHandler;

//while (true)
//{
//    Console.WriteLine("Choose an action: 1,2,3 or Q:");
//    var inputAction = Console.ReadLine();


//    switch (inputAction)

//    {
//        case "1":
//            AddWare(wareRepository);
//            break;

//        case "2":
//            RemoveWare(wareRepository);
//            break;

//        case "3":
//            ReadWares(wareRepository);
//            break;

//        case "q":
//        case "Q":
//            SaveInfo(wareRepository, wareInFile);
//            return;

//        default:
//            Console.WriteLine("You have to type 1,2,3 or Q");
//            continue;


//    }  
//} 

//static void AddWare(IWriteRepository<Ware> wareRepository)
//{
//    Console.WriteLine("Enter the name of the ware (or Q to cancel):");
//    var name = Console.ReadLine();

//        if (name == "q" || name == "Q")
//        {
//            Console.WriteLine("Adding new ware cancelled.");
//            return;
//        }

//    var ware = new Ware { Name = name };
//    wareRepository.Add(ware);
//    wareRepository.Save();
//    Console.WriteLine("Ware added successfully.");

//}

//static void RemoveWare(IRepository<Ware> wareRepository)
//{
//    Console.WriteLine("Enter the name of the ware to remove:");
//    var name = Console.ReadLine();
//    var ware = wareRepository.GetAll().FirstOrDefault(w => w.Name == name);
//    if (ware != null)
//    {
//        wareRepository.Remove(ware);
//        wareRepository.Save();
//        Console.WriteLine("Ware removed successfully.");
//        return;
//    }
//    else
//    {
//        Console.WriteLine("Ware not found.");
//    }
//}


//static void ReadWares(IReadRepository<IEntity> repository)
//{
//    var wares = repository.GetAll();
//    foreach (var ware in wares)
//    {
//        Console.WriteLine(ware);
//    }
//}

//static void SaveInfo(IRepository<Ware> wareRepository, string wareInFile)
//{
//    var wares = wareRepository.GetAll();
//    var jsonList = new List<string>();

//    foreach (var ware in wares)
//    {
//        var json = JsonSerializer.Serialize(ware);
//        jsonList.Add(json);
//    }

//    File.WriteAllLines(wareInFile, jsonList);

//    Console.WriteLine("Session ended. Goodbye!");
//}

//static void WareAddedHandler(object? sender, Ware ware)
//{
//    var logEntry = $"{DateTime.Now}-Added ware: {ware.Name}";
//    WriteAuditLog(logEntry);
//}

//static void WareRemovedHandler(object? sender, Ware ware)
//{
//    var logEntry = $"{DateTime.Now}-Removed ware: {ware.Name}";
//    WriteAuditLog(logEntry);
//}

//static void WriteAuditLog(string logEntry)
//{
//    var logFileName = "audit_log.txt";
//    using (var writer = File.AppendText(logFileName))
//    {
//        writer.WriteLine(logEntry);
//    }
//}