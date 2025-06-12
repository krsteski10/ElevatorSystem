namespace ElevatorSystem.Models
{
    public class Elevator
    {
        public int Id { get; set; }
        public int CurrentFloor { get; set; } = 1;
        public Direction Direction { get; set; } = Direction.Idle;
        public Queue<int> Destinations { get; set; } = new();

        public override string ToString()
        {
            return $"Elevator {Id}: Floor {CurrentFloor}, Direction {Direction}, Destinations: [{string.Join(",", Destinations)}]";
        }
    }
}
