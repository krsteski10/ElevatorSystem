using ElevatorSystem.Models;

namespace ElevatorSystem.Services
{
    public class RequestGenerator
    {
        private static readonly Random _random = new();

        public ElevatorRequest GenerateRandomRequest(int floorCount)
        {
            int floor = _random.Next(1, floorCount + 1);

            Direction direction;

            if (floor == 1)
            {
                direction = Direction.Up;
            }
            else if (floor == floorCount)
            {
                direction = Direction.Down;
            }
            else
            {
                // Randomly choose Up or Down
                direction = _random.Next(2) == 0 ? Direction.Up : Direction.Down;
            }

            return new ElevatorRequest(floor, direction);
        }
    }
}
