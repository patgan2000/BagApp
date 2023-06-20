namespace WareStorageApp
{
    public interface IUserCommunication
    {
        string BeginProgram();

        string UserChoose();

        void AddNewBag();

        void RemoveBagById();

        void GetAllBags();

        void GetBagById();

    }
}
