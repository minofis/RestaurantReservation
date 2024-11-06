using RestaurantReservation.Classes;

public class TableReservationApp
{
    static void Main(string[] args)
    {
        ReservationManager reservationManager = new ReservationManager();
        reservationManager.AddRestaurant("A", 10);
        reservationManager.AddRestaurant("B", 5);

        Console.WriteLine(reservationManager.BookTable("A", new DateTime(2023, 12, 25), 3)); // True
        Console.WriteLine(reservationManager.BookTable("A", new DateTime(2023, 12, 25), 3)); // False
    }
}