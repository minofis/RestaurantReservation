namespace RestaurantReservation.UnitTests
{
    public class ReservationManagerTests
    {
        [Fact]
        public void AddRestaurant_RestaurantNameIsNullOrEmpty_ThrowsArgumentException()
        {
            // Arrange
            var mockReservationManager = new MockReservationManager();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => mockReservationManager.AddRestaurant("", 13));
            Assert.Throws<ArgumentException>(() => mockReservationManager.AddRestaurant(null, 13));
        }

        [Fact]
        public void AddRestaurant_TablesCountIsZeroOrNegative_ThrowsArgumentException()
        {
            // Arrange
            var mockReservationManager = new MockReservationManager();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => mockReservationManager.AddRestaurant("Dominos", 0));
            Assert.Throws<ArgumentException>(() => mockReservationManager.AddRestaurant("Dominos", -1));
        }

        [Fact]
        public void LoadRestaurantsFromFile_FilePathIsNullOrEmpty_ThrowsArgumentException()
        {
            // Arrange
            var mockReservationManager = new MockReservationManager();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => mockReservationManager.LoadRestaurantsFromFile(""));
            Assert.Throws<ArgumentException>(() => mockReservationManager.LoadRestaurantsFromFile(null));
        }

        [Fact]
        public void LoadRestaurantsFromFile_FileDoesNotExists_ThrowsArgumentException()
        {
            // Arrange
            var mockReservationManager = new MockReservationManager();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => mockReservationManager.LoadRestaurantsFromFile("hello.txt"));
        }

        [Fact]
        public void FindAllAvailableTables_AllTablesIsBooked_ThrowsArgumentException()
        {
            // Arrange
            var mockReservationManager = new MockReservationManager();

            // Booking all tables in all restaurants
            foreach (var restaurant in mockReservationManager.restaurants)
            {
                foreach (var table in restaurant.Tables)
                {
                    table.Book(new DateTime(2023, 12, 25));
                }
            }

            // Act & Assert
            Assert.NotNull(mockReservationManager.FindAllAvailableTables(new DateTime(2023, 12, 25)));
            Assert.Empty(mockReservationManager.FindAllAvailableTables(new DateTime(2023, 12, 25)));
        }

        [Fact]
        public void BookTable__TableIsAvailable_ReturnsTrue()
        {
            // Arrange
            var mockReservationManager = new MockReservationManager();

            // Act
            var result = mockReservationManager.BookTable("PuzataHouse", new DateTime(2023, 12, 25), 3);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void BookTable__TableIsBooked_ReturnsFalse()
        {
            // Arrange
            var mockReservationManager = new MockReservationManager();

            // Booking the table
            mockReservationManager.BookTable("PuzataHouse", new DateTime(2023, 12, 25), 3);

            // Act
            var result = mockReservationManager.BookTable("PuzataHouse", new DateTime(2023, 12, 25), 3);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void BookTable_RestaurantNameIsNullOrEmpty_ThrowsArgumentException()
        {
            // Arrange
            var mockReservationManager = new MockReservationManager();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => mockReservationManager.BookTable("", new DateTime(2023, 12, 25), 3));
            Assert.Throws<ArgumentException>(() => mockReservationManager.BookTable(null, new DateTime(2023, 12, 25), 3));
        }

        [Fact]
        public void BookTable_RestaurantNotFound_ThrowsArgumentException()
        {
            // Arrange
            var mockReservationManager = new MockReservationManager();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => mockReservationManager.BookTable("Dominos", new DateTime(2023, 12, 25), 3));
        }

        [Fact]
        public void BookTable_TableNumberIsZeroOrNegative_ThrowsArgumentException()
        {
            // Arrange
            var mockReservationManager = new MockReservationManager();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => mockReservationManager.BookTable("PuzataHouse", new DateTime(2023, 12, 25), 0));
            Assert.Throws<ArgumentException>(() => mockReservationManager.BookTable("PuzataHouse", new DateTime(2023, 12, 25), -1));
        }

        [Fact]
        public void CountAvailableTablesInRestaurant_NoAvailableTables_ReturnsZero()
        {
            // Arrange
            var mockReservationManager = new MockReservationManager();

            // Booking all tables in a restaurant
            foreach (var restaurant in mockReservationManager.restaurants)
            {
                foreach (var table in restaurant.Tables)
                {
                    table.Book(new DateTime(2023, 12, 25));
                }
            }

            // Act
            var result = mockReservationManager.CountAvailableTablesInRestaurant("PuzataHouse", new DateTime(2023, 12, 25));

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void CountAvailableTablesInRestaurant_AllTablesAvailable_ReturnsTotalCount()
        {
            // Arrange
            var mockReservationManager = new MockReservationManager();

            // Act
            var result = mockReservationManager.CountAvailableTablesInRestaurant("PuzataHouse", new DateTime(2023, 12, 25));

            // Assert
            Assert.Equal(10, result);
        }

        [Fact]
        public void SortRestaurantsByAvailability_SortsCorrectlyByAvailableTables()
        {
            // Arrange
            var mockReservationManager = new MockReservationManager();

            // Act
            mockReservationManager.SortRestaurantsByAvailability(new DateTime(2023, 12, 25));

            // Assert
            Assert.Equal("PuzataHouse", mockReservationManager.restaurants[1].Name);
            Assert.Equal("Mak", mockReservationManager.restaurants[0].Name);
            Assert.Equal("AromaCava", mockReservationManager.restaurants[2].Name);
        }
    }
}