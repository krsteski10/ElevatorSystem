using ElevatorSystem.Models;

namespace ElevatorSystem.Services
{
    public class ElevatorScheduler
    {
        private readonly List<Elevator> _elevators;

        public ElevatorScheduler(List<Elevator> elevators)
        {
            _elevators = elevators;
        }

        public void AssignRequest(ElevatorRequest request)
        {
            try
            { 
                Elevator? bestElevator = null;
                int bestScore = int.MaxValue;

                foreach (var elevator in _elevators)
                {
                    int distance = Math.Abs(elevator.CurrentFloor - request.RequestedFloor);
                    int score = distance;

                    // If direction doesn't match add a big score to remove it from considiration
                    if (elevator.Direction != Direction.Idle && elevator.Direction != request.RequestedDirection)
                        score += 1000;

                    // If idle then consider it by assinging good score for now
                    if (elevator.Direction == Direction.Idle)
                        score -= 1;

                    if (score < bestScore)
                    {
                        bestScore = score;
                        bestElevator = elevator;
                    }
                }
                // Fallback just in case
                bestElevator ??= _elevators.First();

                if (!bestElevator.Destinations.Contains(request.RequestedFloor))
                {
                    bestElevator.Destinations.Enqueue(request.RequestedFloor);
                }

                Console.WriteLine($"Assigned request at floor {request.RequestedFloor} to Elevator {bestElevator.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error assigning request: {ex.Message}");
            }
        }
    }
}
