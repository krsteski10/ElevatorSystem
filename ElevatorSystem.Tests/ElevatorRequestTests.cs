using ElevatorSystem.Models;

namespace ElevatorSystem.Tests
{
    public class ElevatorRequestTests
    {
        private const int MaxFloor = 10;

        [Theory]
        [InlineData(1, Direction.Up)]
        [InlineData(5, Direction.Down)]
        [InlineData(10, Direction.Down)]
        // Valid request should be created when floor and direction are correct
        public void Constructor_ValidParameters_CreatesRequest(int floor, Direction direction)
        {
            var request = new ElevatorRequest(floor, direction);

            Assert.Equal(floor, request.RequestedFloor);
            Assert.Equal(direction, request.RequestedDirection);
        }

        [Theory]
        [InlineData(0, Direction.Up)]
        [InlineData(-1, Direction.Down)]
        [InlineData(11, Direction.Down)]
        // Requesting an invalid floor should return false
        public void IsValid_InvalidFloor_ReturnsFalse(int floor, Direction direction)
        {
            var request = new ElevatorRequest(floor, direction);
            bool valid = request.IsValid(10);

            Assert.False(valid);
        }

        [Fact]
        // Requesting with direction Idle should return false
        public void IsValid_IdleDirection_ReturnsFalse()
        {
            var request = new ElevatorRequest(5, Direction.Idle);
            bool valid = request.IsValid(10);

            Assert.False(valid);
        }
    }
}
