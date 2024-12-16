using RestaurantReservation.App.Classes;

namespace RestaurantReservation.UnitTests
{
    public class MockReservationManager : ReservationManager
    {
        public MockReservationManager()
        {
            AddRestaurant("PuzataHouse", 10);
            AddRestaurant("Mak", 500);
            AddRestaurant("AromaCava", 8);
        }
    }
}