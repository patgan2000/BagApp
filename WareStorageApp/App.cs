using BagApp.Components.CsvReader;
using BagApp.Components.CsvReader.Models;
using BagApp.Components.DataProvides;
using BagApp.Data.Repositories;
using System.Xml.Linq;

namespace BagApp
{
    public class App : IApp
    {
        private readonly ICsvReader _csvReader;
        private readonly IRepository<Bag> _bagsRepository;
        private readonly IUserCommunication _userCommunication;
        private const string DataFilePath = "Resources\\bag.csv";

        public IBagsProvider _bagsProvider { get; }

        public App(
            IRepository<Bag> bagRepository,
            IBagsProvider bagsProvider,
            IUserCommunication userCommunication,
            ICsvReader csvReader)
        {
            _bagsRepository = bagRepository;
            _bagsProvider = bagsProvider;
            _userCommunication = userCommunication;
            _csvReader = csvReader;
        }

        public void Run()
        {
            CreateXml();
            QueryXml();

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

        private static void QueryXml()
        {
            var document = XDocument.Load("bag.xml");
            var names = document
                .Element("Bags")?
                .Elements("Bag")
                .Select(v => v.Attribute("Brand")?.Value);

            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }

        private void CreateXml()
        {
            var records = _csvReader.ProcessBags(DataFilePath);

            var document = new XDocument();
            var bags = new XElement("Bags", records
                .Select(x =>
                new XElement("Bag",
                    new XAttribute("Name", x.Name),
                    new XAttribute("Brand", x.Brand),
                    new XAttribute("Price", x.Price))

            ));
            document.Add(bags);
            document.Save("bag.xml");
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
                        if (values.Length >= 1)
                        {
                            var bag = new Bag
                            {
                                Name = values[0],
                                Brand = values[1],
                                Year = int.Parse(values[2]),
                                Price = decimal.Parse(values[3])
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
                    var line = $"{bag.Name},{bag.Brand},{bag.Year},{bag.Price}";
                    lines.Add(line);
                }
                File.WriteAllLines(DataFilePath, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while saving data to CSV: {ex.Message}");
            }
        }
    }
}
