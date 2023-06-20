using System.Xml.Linq;
using WareStorageApp.DataProvides;
using WareStorageApp.Entities;
using WareStorageApp.Repositories;

namespace WareStorageApp
{
    public class App : IApp
    {
        private readonly IRepository<Bag> _bagsRepository;
        private readonly IUserCommunication _userCommunication;
        private const string DataFilePath = "bags.csv";

        public IBagsProvider _bagsProvider { get; }

        public App(
            IRepository<Bag> bagRepository,
            IBagsProvider bagsProvider,
            IUserCommunication userCommunication)
        {
            _bagsRepository = bagRepository;
            _bagsProvider = bagsProvider;
            _userCommunication = userCommunication;
        }

        public void Run()
        {
            LoadDataFromCsv();

            Console.Write(_userCommunication.BeginProgram());
            while (true)
            {
                var userChoose = _userCommunication.UserChoose();
                if (userChoose == "q" || userChoose == "Q")
                {
                    break;
                }
                else if (userChoose == "1")
                {
                    _userCommunication.AddNewBag();
                }
                else if (userChoose == "2")
                {
                    _userCommunication.RemoveBagById();
                }
                else if (userChoose == "3")
                {
                    _userCommunication.GetBagById();
                }
                else if (userChoose == "4")
                {
                    _userCommunication.GetAllBags();
                }
                else
                {
                    Console.WriteLine("Wrong char, put 1,2,3,4 or Q.");
                }

                SaveDataToCsv();
            }
        }

        private void LoadDataFromCsv()
        {
            if (File.Exists(DataFilePath))
            {
                try
                {
                    var lines = File.ReadAllLines(DataFilePath);
                    foreach (var line in lines)
                    {
                        var values = line.Split(',');
                        if (values.Length >= 5)
                        {
                            var bag = new Bag
                            {
                                Name = values[0],
                                Brand = values[1],
                                Type = values[2],
                                Year = decimal.Parse(values[3]),
                                Cost = decimal.Parse(values[4])
                            };
                            _bagsRepository.Add(bag);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while loading data from CSV: {ex.Message}");
                }
            }
        }

        private void SaveDataToCsv()
        {
            try
            {
                var lines = new List<string>();
                var bags = _bagsRepository.GetAll();
                foreach (var bag in bags)
                {
                    var line = $"{bag.Name},{bag.Brand},{bag.Type},{bag.Year},{bag.Cost}";
                    lines.Add(line);
                }
                File.WriteAllLines(DataFilePath, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while saving data to CSV: {ex.Message}");
                // Obsłuż wyjątek w odpowiedni sposób, np. zapisz w logach lub zakończ działanie programu
            }
        }
    }
}
