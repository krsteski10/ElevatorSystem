using ElevatorSystem.Models;
using ElevatorSystem.Services;

namespace ElevatorSystem.Tests
{
    public class ElevatorSchedulerTests
    {
        // Helper method to create a list of idle elevators
        private List<Elevator> CreateElevators(int count)
        {
            var elevators = new List<Elevator>();
            for (int i = 1; i <= count; i++)
            {
                elevators.Add(new Elevator { Id = i, CurrentFloor = 1, Direction = Direction.Idle });
            }
            return elevators;
        }

        [Fact]
        // Should assign request to an idle elevator when available
        public void AssignRequest_IdleElevator_GetsAssigned()
        {
            var elevators = CreateElevators(3);
            var scheduler = new ElevatorScheduler(elevators);

            var request = new ElevatorRequest(5, Direction.Up);
            scheduler.AssignRequest(request);

            var assigned = elevators.FirstOrDefault(e => e.Destinations.Contains(5));
            Assert.NotNull(assigned);
        }

        [Fact]
        // Should assign request to the closest elevator already moving in the matching direction
        public void AssignRequest_ClosestElevatorWithMatchingDirection_GetsAssigned()
        {
            var elevators = new List<Elevator>
            {
                new Elevator { Id = 1, CurrentFloor = 2, Direction = Direction.Up },
                new Elevator { Id = 2, CurrentFloor = 8, Direction = Direction.Down },
                new Elevator { Id = 3, CurrentFloor = 1, Direction = Direction.Idle }
            };

            var scheduler = new ElevatorScheduler(elevators);
            var request = new ElevatorRequest(5, Direction.Up);

            scheduler.AssignRequest(request);

            // Expect elevator 1 to be assigned since it's already going up and is closer
            Assert.Contains(5, elevators[0].Destinations);
        }

        [Fact]
        // Should not enqueue the same floor twice for a single elevator
        public void AssignRequest_DuplicateFloor_NotEnqueuedAgain()
        {
            var elevators = CreateElevators(1);
            var scheduler = new ElevatorScheduler(elevators);

            var request = new ElevatorRequest(4, Direction.Up);
            scheduler.AssignRequest(request);
            scheduler.AssignRequest(request); // duplicate

            Assert.Single(elevators[0].Destinations);
        }
    }
}
