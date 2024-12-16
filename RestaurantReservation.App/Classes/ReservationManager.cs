using RestaurantReservation.App.Interfaces;

namespace RestaurantReservation.App.Classes
{
    public class ReservationManager : IReservationManager
    {
        public List<Restaurant> restaurants;

        public ReservationManager()
        {
            restaurants = new List<Restaurant>();
        }

        // Add Restaurant 
        public void AddRestaurant(string restaurantName, int tablesCount)
        {
            try
            {
                if(string.IsNullOrEmpty(restaurantName)) throw new ArgumentException("Restaurant name can't be null or empty");
                if(tablesCount <= 0) throw new ArgumentException("Tables count must be a positive integer");

                // Creating a restaurant
                var restaurant = new Restaurant
                {
                    Name = restaurantName,
                    Tables = Enumerable.Range(0, tablesCount).Select(index => new Table(index + 1)).ToList()
                };

                restaurants.Add(restaurant);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void LoadRestaurantsFromFile(string restaurantsFileName)
        {
            try
            {
                if(string.IsNullOrEmpty(restaurantsFileName)) throw new ArgumentException("Restaurants file name name can't be null or empty");

                // Getting lines from file
                var lines = File.ReadAllLines(restaurantsFileName);
                foreach (string line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int tableCount))
                    {
                        AddRestaurant(parts[0], tableCount);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // Find all available tables in all restaurants
        public List<string> FindAllAvailableTables(DateTime bookedDate)
        {
            try
            {
                // Creating tables output
                List<string> availableTablesOutput = [];

                // Searching for available tables
                List<Table> availableTables = [];

                foreach (var restaurant in restaurants)
                {
                    availableTables = restaurant.Tables.Where(t => !t.IsBooked(bookedDate)).ToList() ?? throw new ArgumentException("There are no available tables");
                    foreach (var availableTable in availableTables)
                    {
                        // Creating table output
                        string availableTableOutput = $"Restaurant: {restaurant.Name} - Table: {availableTable.Number}";

                        // Adding table output
                        availableTablesOutput.Add(availableTableOutput);
                    }
                }
                return availableTablesOutput;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<string>();
            }
        }

        public bool BookTable(string restaurantName, DateTime bookedDate, int tableNumber)
        {
            try
            {
                if(string.IsNullOrEmpty(restaurantName)) throw new ArgumentException("Restaurant name can't be null or empty");
            
                // Searching for a restaurant by name
                var restaurant = restaurants.FirstOrDefault(r => r.Name == restaurantName) ?? throw new ArgumentException("Restaraunt not found");

                var bookedTable = restaurant.Tables.FirstOrDefault(t => t.Number == tableNumber) ?? throw new ArgumentException("Table not found");;

                return bookedTable.Book(bookedDate);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // Sort eestaurants by availability
        public void SortRestaurantsByAvailability(DateTime bookedDate)
        {
            try
            {
                restaurants = restaurants.OrderByDescending(r => CountAvailableTablesInRestaurant(r.Name, bookedDate)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // Count available tables in restaurant
        public int CountAvailableTablesInRestaurant(string restaurantName, DateTime bookedDate)
        {
            try
            {
                if(string.IsNullOrEmpty(restaurantName)) throw new ArgumentException("Restaurant name can't be null or empty");

                // Searching for a restaurant by name
                var restaurant = restaurants.FirstOrDefault(r => r.Name == restaurantName) ?? throw new ArgumentException("Restaraunt not found");

                // Сounting available tables
                return restaurant.Tables.Where(t => !t.IsBooked(bookedDate)).Count();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}