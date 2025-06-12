using ElevatorSystem.Services;
using ElevatorSystem.Models;

namespace ElevatorSystem;

public class Program
{
    private const int FloorCount = 10;
    private const int ElevatorCount = 4;
    private const int SimulationSteps = 10;

    public static void Main(string[] args)
    {
        var controller = new ElevatorController(ElevatorCount);
        var generator = new RequestGenerator();

        for (int step = 0; step < SimulationSteps; step++)
        {
            Console.WriteLine($"\n--- Simulation Step {step + 1} ---");

            var request = generator.GenerateRandomRequest(FloorCount);
            controller.HandleRequest(request);

            controller.AdvanceOneStep();

            Thread.Sleep(10000); // Simulate 10-second time step
        }

        Console.WriteLine("\nSimulation completed.");
    }
}

////Uncomment code below for custom elevator requests
//using ElevatorSystem.Services;
//using ElevatorSystem.Models;

//namespace ElevatorSystem;

//public class Program
//{
//    private const int FloorCount = 10;
//    private const int ElevatorCount = 4;

//    public static void Main(string[] args)
//    {
//        var controller = new ElevatorController(ElevatorCount);

//        var customRequests = new List<ElevatorRequest>
//        {
//            new ElevatorRequest(3, Direction.Up),
//            new ElevatorRequest(7, Direction.Up),
//            new ElevatorRequest(1, Direction.Up),
//            new ElevatorRequest(9, Direction.Down),
//            new ElevatorRequest(5, Direction.Down),
//            new ElevatorRequest(3, Direction.Up),
//        };

//        for (int i = 0; i < customRequests.Count; i++)
//        {
//            Console.WriteLine($"\n--- Step {i + 1}: Handling Request on Floor {customRequests[i].RequestedFloor} ({customRequests[i].RequestedDirection}) ---");
//            controller.HandleRequest(customRequests[i]);
//            controller.Step();

//            Thread.Sleep(10000); // Simulate 10 seconds per step
//        }

//        Console.WriteLine("\nCustom simulation completed.");
//    }
//}