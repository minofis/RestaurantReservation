namespace RestaurantReservation.Classes
{
    public class Table
    {
        private List<DateTime> bd; //booked dates

        public Table()
        {
            bd = new List<DateTime>();
        }

        // book
        public bool Book(DateTime d)
        {
            try
            { 
                if (bd.Contains(d))
                {
                    return false;
                }
                //add to bd
                bd.Add(d);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                return false;
            }
        }

        // is booked
        public bool IsBooked(DateTime d)
        {
            return bd.Contains(d);
        }
    }
}