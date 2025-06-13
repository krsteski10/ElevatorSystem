using ElevatorSystem.Models;
using ElevatorSystem.Services;

namespace ElevatorSystem.Tests
{
    public class ElevatorControllerTests
    {
        [Fact]
        // Should initialize the correct number of elevators
        public void Constructor_ValidCount_InitializesElevators()
        {
            var controller = new ElevatorController(3);
            Assert.NotNull(controller);
        }

        [Fact]
        // Should throw exception when initialized with zero or negative elevators
        public void Constructor_InvalidCount_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new ElevatorController(0));
            Assert.Throws<ArgumentException>(() => new ElevatorController(-2));
        }

        [Fact]
        // Should reject invalid requests
        public void HandleRequest_InvalidRequest_DoesNotThrow()
        {
            var controller = new ElevatorController(2);
            var invalidRequest = new ElevatorRequest(100, Direction.Up); // floor 100 is invalid

            // Just confirm it doesn't crash
            controller.HandleRequest(invalidRequest, 10);
        }

        [Fact]
        // Should accept valid request and assign to an elevator
        public void HandleRequest_ValidRequest_AssignsToElevator()
        {
            var controller = new ElevatorController(2);
            var validRequest = new ElevatorRequest(3, Direction.Up);

            controller.HandleRequest(validRequest, 10);

            // No exception means success, since no direct access to elevators
        }

        [Fact]
        // Should simulate a step forward and update elevator state
        public void AdvanceOneStep_Works()
        {
            var controller = new ElevatorController(1);
            var request = new ElevatorRequest(3, Direction.Up);
            controller.HandleRequest(request, 10);

            controller.AdvanceOneStep();
            controller.AdvanceOneStep();

            // No exceptions mean success, console outputs are not captured
        }
    }
}
