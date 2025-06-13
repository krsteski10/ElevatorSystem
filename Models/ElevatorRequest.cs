namespace ElevatorSystem.Models
{
    public class ElevatorRequest
    {
        public int RequestedFloor { get; set; }
        public Direction RequestedDirection { get; set; }

        public ElevatorRequest(int requestedFloor, Direction requestedDirection)
        {
            RequestedFloor = requestedFloor;
            RequestedDirection = requestedDirection;
        }

        public override string ToString()
        {
            return $"Request: Floor {RequestedFloor}, Direction {RequestedDirection}";
        }

        public bool IsValid(int maxFloor)
        {
            if (RequestedFloor < 1 || RequestedFloor > maxFloor)
                return false;

            if (RequestedDirection == Direction.Idle)
                return false;

            return true;
        }
    }
}
