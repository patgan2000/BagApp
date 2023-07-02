using BagApp.Components.CsvReader;
using BagApp.Components.XmlCreator;
using BagApp.Data;
using BagApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using BagApp.Data.Entities;
using BagApp.Data.Repositories;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IEventHandelerService, EventHandlerService>();
services.AddSingleton<IBagProvider, BagProvider>();
services.AddSingleton<IRepository<Bag>, SqlRepository<Bag>>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<IDataSqlProvider, DataSqlProvider>();
services.AddSingleton<IXmlCreator, XmlCreator>();
services.AddSingleton<DbContext, BagAppDbContext>();

services.AddDbContext<BagAppDbContext>(options =>
            options.UseSqlServer("Data Source=PATRYCJA\\SQLEXPRESS;Initial Catalog=BagAppDateBase;Integrated Security=True; Trusted_Connection=True;TrustServerCertificate=True;"));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();
app.Run();


