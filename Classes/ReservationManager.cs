namespace RestaurantReservation.Classes
{
    public class ReservationManager
    {
        public List<Restaurant> restaurants;

        public ReservationManager()
        {
            restaurants = new List<Restaurant>();
        }

        // Add Restaurant Method
        public void AddRestaurant(string restaurantName, int tableNumber)
        {
            try
            {
                Restaurant restaurant = new Restaurant();
                restaurant.Name = restaurantName;
                restaurant.Tables = new Table[tableNumber];
                for (int i = 0; i < tableNumber; i++)
                {
                    restaurant.Tables[i] = new Table();
                }
                restaurants.Add(restaurant);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
            }
        }

        private void LoadRestaurantsFromFile(string restaurantsDataFile)
        {
            try
            {
                string[] lines = File.ReadAllLines(restaurantsDataFile);
                foreach (string line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int tableCount))
                    {
                        AddRestaurant(parts[0], tableCount);
                    }
                    else
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
            }
        }

        public List<string> FindFreeTables(DateTime bookedDate)
        {
            try
            { 
                List<string> freeTables = new List<string>();
                foreach (var restaurant in restaurants)
                {
                    for (int i = 0; i < restaurant.Tables.Length; i++)
                    {
                        if (!restaurant.Tables[i].IsBooked(bookedDate))
                        {
                            freeTables.Add($"{restaurant.Name} - Table {i + 1}");
                        }
                    }
                }
                return freeTables;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                return new List<string>();
            }
        }

        public bool BookTable(string restaurantName, DateTime bookedDate, int tableNumber)
        {
            foreach (var restaurant in restaurants)
            {
                if (restaurant.Name == restaurantName)
                {
                    if (tableNumber < 0 || tableNumber >= restaurant.Tables.Length)
                    {
                        throw new Exception(null); //Invalid table number
                    }

                    return restaurant.Tables[tableNumber].Book(bookedDate);
                }
            }

            throw new Exception(null); //Restaurant not found
        }

        public void SortRestaurantsByAvailability(DateTime bookedDate)
        {
            try
            { 
                bool swapped;
                do
                {
                    swapped = false;
                    for (int i = 0; i < restaurants.Count - 1; i++)
                    {
                        int avTc = CountAvailableTables(restaurants[i], bookedDate); // available tables current
                        int avTn = CountAvailableTables(restaurants[i + 1], bookedDate); // available tables next

                        if (avTc < avTn)
                        {
                            // Swap restaurants
                            var temp = restaurants[i];
                            restaurants[i] = restaurants[i + 1];
                            restaurants[i + 1] = temp;
                            swapped = true;
                        }
                    }
                } while (swapped);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
            }
        }

        // count available tables in a restaurant
        public int CountAvailableTables(Restaurant restaurant, DateTime bookedDate)
        {
            try
            {
                int availableTablesCount = 0;
                foreach (var table in restaurant.Tables)
                {
                    if (!table.IsBooked(bookedDate))
                    {
                        availableTablesCount++;
                    }
                }
                return availableTablesCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                return 0;
            }
        }
    }
}