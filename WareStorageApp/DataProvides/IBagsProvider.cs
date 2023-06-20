using WareStorageApp.Entities;

namespace WareStorageApp.DataProvides
{
    public interface IBagsProvider
    {
        //select
        List<Bag> GetAllNames();

        decimal? GetMostExpensiveBag();

        //order by
        List<Bag> OrderByName();

        List<Bag> OrderByNameDesc();

        List<Bag> OrderByBrandAndName();

        //where
        List<Bag> WhereStartsWith(string prefix);

        List<Bag> WhereBrandIs(string brand);

        List<Bag> WhereYearIs(int year);

        List<Bag> WhereCostIsEmpty();

        //take

        List<Bag> Take5CheapestBags();

        List<Bag> Take5ExpensiveBags();


        //skip
        

        //chunk
        List<Bag[]> ChunkBags(int id);

    }
}
