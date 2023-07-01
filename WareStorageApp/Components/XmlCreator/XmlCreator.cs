using BagApp.Components.CsvReader;
using System.Xml.Linq;

namespace BagApp.Components.XmlCreator
{
    public class XmlCreator : IXmlCreator
    {
        private readonly ICsvReader _csvReader;
        public XmlCreator(ICsvReader csvReader)
        {
            _csvReader = csvReader;
        }

        public void CreateXmlFile()
        {
            var bags = _csvReader.ProcessBags("Resources\\bag.csv");
            var shops = _csvReader.ProcessShops("Resources\\shop.csv");

            var bagGroups = shops.GroupJoin(
                bags,
                x => x.Name,
                x => x.Name,
                (s, g) =>
                new
                {
                    Shops = s,
                    Bags = g
                });

            var document = new XDocument();

            var fileXml = new XElement("Shops", bagGroups
                .Select(x =>
                     new XElement("Shops",
                        new XAttribute("Name", x.Shops.Name),
                        new XAttribute("City", x.Shops.City),
                           new XElement("Bags"))));

            document.Add(fileXml);
            document.Save("Bag Catalog.xml");
        }
    }
}

