using RestaurantReservation.Classes;
using RestaurantReservation.Interfaces;

namespace RestaurantReservation;
public class Program
{
    static void Main(string[] args)
    {
        // Creating a reservation manager
        ReservationManager reservationManager = new();

        // Loading restaurants from file to the reservation manager
        reservationManager.LoadRestaurantsFromFile("load.txt");

        // Sorting restaurants by availability
        reservationManager.SortRestaurantsByAvailability(new DateTime(2023, 12, 25));

        // Searching for all available tables in all restaurants
        var availableTables = reservationManager.FindAllAvailableTables(new DateTime(2023, 12, 25));
        
        // Printing all available tables in all restaurants
        Console.WriteLine("AVAILABLE TABLES:");
        foreach (var availableTable in availableTables) Console.WriteLine(availableTable.ToString());

        // Booking a table in a restaurant
        if(reservationManager.BookTable("PuzataHouse", new DateTime(2023, 12, 25), 3))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Table booked successfully!");
            Console.ResetColor();
        };

        // Sorting restaurants by availability
        reservationManager.SortRestaurantsByAvailability(new DateTime(2023, 12, 25));
        
        // Searching for all available tables in all restaurants
        availableTables = reservationManager.FindAllAvailableTables(new DateTime(2023, 12, 25));

        // Printing all available tables in all restaurants
        Console.WriteLine("AVAILABLE TABLES:");
        foreach (var availableTable in availableTables) Console.WriteLine(availableTable.ToString());

        // Booking a table in a restaurant
        if(reservationManager.BookTable("PuzataHouse", new DateTime(2023, 12, 25), 3))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Table booked successfully!");
            Console.ResetColor();
        };
    }
}