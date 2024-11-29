namespace RestaurantReservation.Classes
{
    public class Table
    {
        private List<DateTime> BookedDates;
        public int Number;

        public Table(int number)
        {
            BookedDates = new List<DateTime>();
            Number = number;
        }

        // Book table
        public bool Book(DateTime bookedDate)
        {
            try
            {
                // Checking table availability
                if (!IsBooked(bookedDate)) 
                {
                    // Adding a book date to the booked dates
                    BookedDates.Add(bookedDate);
                    return true;
                }
                throw new ArgumentException("Error: Table is not available");
            }
            catch (Exception ex)
            {
                // Error output            
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                return false;
            }
        }

        // Check a table is available
        public bool IsBooked(DateTime bookedDate)
        {
            return BookedDates.Contains(bookedDate);
        }
    }
}