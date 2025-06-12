namespace ElevatorSystem.Models
{
    public class ElevatorRequest
    {
        public int RequestedFloor { get; set; }
        public Direction RequestedDirection { get; set; }

        public ElevatorRequest(int floor, Direction direction)
        {
            RequestedFloor = floor;
            RequestedDirection = direction;
        }

        public override string ToString()
        {
            return $"Request: Floor {RequestedFloor}, Direction {RequestedDirection}";
        }
    }
}
