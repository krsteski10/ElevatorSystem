using ElevatorSystem.Models;

namespace ElevatorSystem.Services
{
    public class ElevatorController
    {
        private readonly List<Elevator> _elevators;
        private readonly ElevatorScheduler _scheduler;

        public ElevatorController(int elevatorCount)
        {
            if (elevatorCount <= 0)
                throw new ArgumentException("Elevator count must be greater than zero.", nameof(elevatorCount));

            _elevators = Enumerable.Range(1, elevatorCount)
                                   .Select(id => new Elevator { Id = id })
                                   .ToList();

            _scheduler = new ElevatorScheduler(_elevators);
        }

        public void HandleRequest(ElevatorRequest request, int floorCount)
        {
            if (!request.IsValid(floorCount))
            {
                Console.WriteLine($"Invalid request: {request}");
                return;
            }

            try
            {
                Console.WriteLine(request.ToString());
                _scheduler.AssignRequest(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling request: {ex.Message}");
            }
        }

        public void AdvanceOneStep()
        {
            foreach (var elevator in _elevators)
            {
                MoveElevator(elevator);
            }
        }

        private void MoveElevator(Elevator elevator)
        {
            if (elevator.Destinations.Count > 0)
            {
                var destinationList = elevator.Destinations.ToList();

                // Check if the elevator should stop at the current floor
                if (destinationList.Contains(elevator.CurrentFloor))
                {
                    Console.WriteLine($"Elevator {elevator.Id} stopped at floor {elevator.CurrentFloor} for boarding/deboarding...");
                    Thread.Sleep(2000); // simulate 2 sec stop

                    // Remove all matching floors from the queue
                    elevator.Destinations = new Queue<int>(destinationList.Where(f => f != elevator.CurrentFloor));
                }
                else
                {
                    // Determine movement direction
                    if (elevator.Direction == Direction.Idle)
                    {
                        var firstDestination = elevator.Destinations.Peek();
                        elevator.Direction = firstDestination > elevator.CurrentFloor ? Direction.Up : Direction.Down;
                    }

                    // Move elevator one floor in its direction
                    elevator.CurrentFloor += elevator.Direction == Direction.Up ? 1 : -1;
                }

                // If no more destinations, set direction to idle
                if (elevator.Destinations.Count == 0)
                    elevator.Direction = Direction.Idle;
            }

            Console.WriteLine(elevator.ToString());
        }
    }
}
