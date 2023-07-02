using BagApp.Components.CsvReader;
using BagApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace BagApp.Services
{
    public class App : IApp
    {
        private readonly IUserCommunication _userCommunication;
        private readonly IDataSqlProvider _dataSqlProvider;
        private readonly IBagProvider _bagProvider;
        private readonly IEventHandelerService _eventHandeler;


        public App(IUserCommunication userComunnication,
            IBagProvider bagProvider,
            IDataSqlProvider dataSqlProvider,
            IEventHandelerService eventHandeler)
        {
            _userCommunication = userComunnication;
            _dataSqlProvider = dataSqlProvider;
            _bagProvider = bagProvider;
            _eventHandeler = eventHandeler;
        }

        public void Run()
        {
            Console.WriteLine("Welcome to [BagApp]!");
            Console.WriteLine("----------------------------------------------------------");

            _dataSqlProvider.InsertDataToSql();
            _bagProvider.AddBags();
            _eventHandeler.EventHandlerForList();
            _userCommunication.ChooseOption();
        }
    }
}