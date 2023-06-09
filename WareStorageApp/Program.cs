﻿using WareStorageApp.Data;
using WareStorageApp.Entities;
using WareStorageApp.Repositories;
Console.WriteLine("*****************************************************************************");
Console.WriteLine("Welcome to [WareStorageApp]!");
Console.WriteLine("Using this application, you can enter the history of your fashion purchases.");
Console.WriteLine("*****************************************************************************");
Thread.Sleep(2000);
Console.WriteLine();
Console.WriteLine("List of available actions:");
Console.WriteLine("(1) Add a new ware");
Console.WriteLine("(2) Remove a new ware");
Console.WriteLine("(3) Read the current list of wares");
Console.WriteLine("(Q) End the session");

var wareRepository = new SqlRepository<Ware>(new WareStorageDbContext());
wareRepository.WareAdded += WareAddedHandler;
wareRepository.WareRemoved += WareRemovedHandler;

while (true)
{
    Console.WriteLine("Choose an action: 1,2,3 or Q:");
    var inputAction = Console.ReadLine();


    switch (inputAction)

    {
        case "1":
            AddWare(wareRepository);
            break;

        case "2":
            RemoveWare(wareRepository);
            break;

        case "3":
            ReadWares(wareRepository);
            break;

        case "q":
        case "Q":
            SaveInfo(wareRepository);
            return;

        default:
            Console.WriteLine("You have to type 1,2,3 or Q");
            continue;


    }  
} 

static void AddWare(IWriteRepository<Ware> wareRepository)
{
    Console.WriteLine("Enter the name of the ware (or Q to cancel):");
    var name = Console.ReadLine();

        if (name == "q" || name == "Q")
        {
            Console.WriteLine("Adding new ware cancelled.");
            return;
        }

    var ware = new Ware { Name = name };
    wareRepository.Add(ware);
    wareRepository.Save();
    Console.WriteLine("Ware added successfully.");

}

static void RemoveWare(IRepository<Ware> wareRepository)
{
    Console.WriteLine("Enter the name of the ware to remove:");
    var name = Console.ReadLine();
    var ware = wareRepository.GetAll().FirstOrDefault(w => w.Name == name);
    if (ware != null)
    {
        wareRepository.Remove(ware);
        wareRepository.Save();
        Console.WriteLine("Ware removed successfully.");
        return;
    }
    else
    {
        Console.WriteLine("Ware not found.");
    }
}


static void ReadWares(IReadRepository<IEntity> repository)
{
    var wares = repository.GetAll();
    foreach (var ware in wares)
    {
        Console.WriteLine(ware);
    }
}

static void SaveInfo(IRepository<Ware> wareRepository)
{
    Console.WriteLine("Session ended. Goodbye!");
}

static void WareAddedHandler(object? sender, Ware ware)
{
    var logEntry = $"{DateTime.Now}-{nameof(WareAddedHandler)}-Added ware: {ware.Name}";
    WriteAuditLog(logEntry);
}

static void WareRemovedHandler(object? sender, Ware ware)
{
    var logEntry = $"{DateTime.Now}-{nameof(WareRemovedHandler)}-Removed ware: {ware.Name}";
    WriteAuditLog(logEntry);
}

static void WriteAuditLog(string logEntry)
{
    var logFileName = "audit_log.txt";
    using (var writer = File.AppendText(logFileName))
    {
        writer.WriteLine(logEntry);
    }
}