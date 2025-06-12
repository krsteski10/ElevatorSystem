using ElevatorSystem.Models;

namespace ElevatorSystem.Services
{
    public class ElevatorController
    {
        private readonly List<Elevator> _elevators;
        private readonly ElevatorScheduler _scheduler;

        public ElevatorController(int elevatorCount)
        {
            _elevators = Enumerable.Range(1, elevatorCount)
                                   .Select(id => new Elevator { Id = id })
                                   .ToList();

            _scheduler = new ElevatorScheduler(_elevators);
        }

        public void HandleRequest(ElevatorRequest request)
        {
            Console.WriteLine(request.ToString());
            _scheduler.AssignRequest(request);
        }

        public void AdvanceOneStep()
        {
            foreach (var elevator in _elevators)
            {
                MoveElevator(elevator);
                Console.WriteLine(elevator.ToString());
            }
        }

        private void MoveElevator(Elevator elevator)
        {
            if (elevator.Destinations.Count == 0)
            {
                elevator.Direction = Direction.Idle;
                return;
            }

            int nextFloor = elevator.Destinations.Peek();

            if (elevator.CurrentFloor < nextFloor)
            {
                elevator.CurrentFloor++;
                elevator.Direction = Direction.Up;
            }
            else if (elevator.CurrentFloor > nextFloor)
            {
                elevator.CurrentFloor--;
                elevator.Direction = Direction.Down;
            }
            else
            {
                Console.WriteLine($"Elevator {elevator.Id} stopped at floor {elevator.CurrentFloor} for boarding/deboarding...");
                Thread.Sleep(10000);
                elevator.Destinations.Dequeue();

                if (elevator.Destinations.Count == 0)
                {
                    elevator.Direction = Direction.Idle;
                }
            }
        }


        //can be used to determine if the elevators have not reached their destinations
        //after all the steps, as an improvement
        public bool HasPendingWork()
        {
            return _elevators.Any(e => e.Destinations.Count > 0);
        }
    }
}
