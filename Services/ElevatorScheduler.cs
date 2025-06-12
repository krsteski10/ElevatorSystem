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
            // Find best elevator: either idle or already going in same direction past the request floor
            Elevator? bestElevator = null;
            int bestScore = int.MaxValue;

            foreach (var elevator in _elevators)
            {
                if (elevator.Direction == Direction.Idle)
                {
                    int distance = Math.Abs(elevator.CurrentFloor - request.RequestedFloor);
                    if (distance < bestScore)
                    {
                        bestElevator = elevator;
                        bestScore = distance;
                    }
                }
                else if (elevator.Direction == request.RequestedDirection)
                {
                    if (request.RequestedDirection == Direction.Up &&
                        elevator.CurrentFloor <= request.RequestedFloor)
                    {
                        int distance = request.RequestedFloor - elevator.CurrentFloor;
                        if (distance < bestScore)
                        {
                            bestElevator = elevator;
                            bestScore = distance;
                        }
                    }
                    else if (request.RequestedDirection == Direction.Down &&
                             elevator.CurrentFloor >= request.RequestedFloor)
                    {
                        int distance = elevator.CurrentFloor - request.RequestedFloor;
                        if (distance < bestScore)
                        {
                            bestElevator = elevator;
                            bestScore = distance;
                        }
                    }
                }
            }

            // If none matched and all going opposite direction, assign the closest idle or random
            if (bestElevator == null)
            {
                bestElevator = _elevators.OrderBy(e => Math.Abs(e.CurrentFloor - request.RequestedFloor)).First();
            }

            // Enqueue the request floor if it's not already in the queue
            if (!bestElevator.Destinations.Contains(request.RequestedFloor))
            {
                bestElevator.Destinations.Enqueue(request.RequestedFloor);
            }

            Console.WriteLine($"Assigned request at floor {request.RequestedFloor} to Elevator {bestElevator.Id}");
        }
    }
}
