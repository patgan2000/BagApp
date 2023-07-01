using BagApp.Entities;
using BagApp.Repositories;

namespace BagApp.Services
{
    public class EventHandlerService : IEventHandelerService
    {
        private readonly IRepository<Bag> _bagRepository;

        public EventHandlerService(IRepository<Bag> bagRepository)
        {
            _bagRepository = bagRepository;
        }

        public void EventHandlerForList()
        {
            _bagRepository.ItemAdded += EventHandlerItemAddedToFileTxt;
            _bagRepository.ItemRemoved += EventHandlerItemRemovedToFileTxt;
        }

        private void EventHandlerItemAddedToFileTxt(object? sender, Bag e)
        {
            SaveInfoAboutEventToFile("Bag Added", e);
        }

        private void EventHandlerItemRemovedToFileTxt(object? sender, Bag e)
        {
            SaveInfoAboutEventToFile("Bag Removed", e);
        }

        private void SaveInfoAboutEventToFile(string info, Bag e)
        {
            using (var writer = new StreamWriter("Historia.txt", true))
            {
                writer.WriteLine($"[{DateTime.Now}] - {info} - {e.Name} {e.Brand}");
            }
        }
    }
}
