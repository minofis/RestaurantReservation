namespace RestaurantReservation.App.Interfaces
{
    public interface IReservationManager
    {
        void AddRestaurant(string restaurantName, int tablesCount);
        void LoadRestaurantsFromFile(string restaurantsFileName);
        List<string> FindAllAvailableTables(DateTime bookedDate);
        bool BookTable(string restaurantName, DateTime bookedDate, int tableNumber);
        void SortRestaurantsByAvailability(DateTime bookedDate);
        int CountAvailableTablesInRestaurant(string restaurantName, DateTime bookedDate);
    }
}