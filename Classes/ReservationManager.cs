namespace RestaurantReservation.Classes
{
    public class ReservationManager
    {
        public List<Restaurant> res;

        public ReservationManager()
        {
            res = new List<Restaurant>();
        }

        // Add Restaurant Method
        public void AddRestaurantMethod(string n, int t)
        {
            try
            {
                Restaurant r = new Restaurant();
                r.n = n;
                r.t = new Table[t];
                for (int i = 0; i < t; i++)
                {
                    r.t[i] = new Table();
                }
                res.Add(r);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
            }
        }

        // Load Restaurants From
        // File
        private void LoadRestaurantsFromFileMethod(string fileP)
        {
            try
            {
                string[] ls = File.ReadAllLines(fileP);
                foreach (string l in ls)
                {
                    var parts = l.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int tableCount))
                    {
                        AddRestaurantMethod(parts[0], tableCount);
                    }
                    else
                    {
                        Console.WriteLine(l);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
            }
        }

        //Find All Free Tables
        public List<string> FindAllFreeTables(DateTime dt)
        {
            try
            { 
                List<string> free = new List<string>();
                foreach (var r in res)
                {
                    for (int i = 0; i < r.t.Length; i++)
                    {
                        if (!r.t[i].IsBooked(dt))
                        {
                            free.Add($"{r.n} - Table {i + 1}");
                        }
                    }
                }
                return free;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                return new List<string>();
            }
        }

        public bool BookTable(string rName, DateTime d, int tNumber)
        {
            foreach (var r in res)
            {
                if (r.n == rName)
                {
                    if (tNumber < 0 || tNumber >= r.t.Length)
                    {
                        throw new Exception(null); //Invalid table number
                    }

                    return r.t[tNumber].Book(d);
                }
            }

            throw new Exception(null); //Restaurant not found
        }

        public void SortRestaurantsByAvailabilityForUsersMethod(DateTime dt)
        {
            try
            { 
                bool swapped;
                do
                {
                    swapped = false;
                    for (int i = 0; i < res.Count - 1; i++)
                    {
                        int avTc = CountAvailableTablesForRestaurantClassAndDateTimeMethod(res[i], dt); // available tables current
                        int avTn = CountAvailableTablesForRestaurantClassAndDateTimeMethod(res[i + 1], dt); // available tables next

                        if (avTc < avTn)
                        {
                            // Swap restaurants
                            var temp = res[i];
                            res[i] = res[i + 1];
                            res[i + 1] = temp;
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
        public int CountAvailableTablesForRestaurantClassAndDateTimeMethod(Restaurant r, DateTime dt)
        {
            try
            {
                int count = 0;
                foreach (var t in r.t)
                {
                    if (!t.IsBooked(dt))
                    {
                        count++;
                    }
                }
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                return 0;
            }
        }
    }
}