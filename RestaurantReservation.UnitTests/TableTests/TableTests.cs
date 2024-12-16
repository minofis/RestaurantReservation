using RestaurantReservation.App.Classes;

namespace RestaurantReservation.UnitTests
{
    public class TableTests
    {
        [Fact]
        public void Book_TableIsBooked_ReturnsFalse()
        {
            // Arrange
            var table = new Table(10);
            table.Book(new DateTime(2023, 12, 25));

            // Act
            var result = table.Book(new DateTime(2023, 12, 25));

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Book_TableIsAvailable_ReturnsTrue()
        {
            // Arrange
            var table = new Table(10);

            // Act
            var result = table.Book(new DateTime(2023, 12, 25));

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsBooked_TableIsBooked_ReturnsTrue()
        {
            // Arrange
            var table = new Table(10);
            table.Book(new DateTime(2023, 12, 25));

            // Act
            var result = table.IsBooked(new DateTime(2023, 12, 25));

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsBooked_TableIsAvailable_ReturnsFalse()
        {
            // Arrange
            var table = new Table(10);

            // Act
            var result = table.IsBooked(new DateTime(2023, 12, 25));

            // Assert
            Assert.False(result);
        }
    }
}