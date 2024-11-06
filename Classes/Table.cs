namespace RestaurantReservation.Classes
{
    public class Table
    {
        private List<DateTime> bookedDates;

        public Table()
        {
            bookedDates = new List<DateTime>();
        }

        // book
        public bool Book(DateTime bookedDate)
        {
            try
            { 
                if (bookedDates.Contains(bookedDate))
                {
                    return false;
                }
                //add to bd
                bookedDates.Add(bookedDate);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                return false;
            }
        }

        // is booked
        public bool IsBooked(DateTime bookedDate)
        {
            return bookedDates.Contains(bookedDate);
        }
    }
}