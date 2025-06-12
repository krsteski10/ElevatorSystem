# ElevatorSystem

# Elevator Control System 🚪⬆⬇

This is a simple simulation of an elementary elevator control system written in C#.

## 🏗️ Features

- Simulates 4 elevators in a 10-floor building.
- Random or custom elevator requests (floor + direction).
- Elevators move one floor per 10 seconds and stop for 10 seconds for boarding.
- Basic scheduling logic keeps elevators moving in one direction until idle.
- Console output shows request handling and elevator status in each step.

## 🧠 Implementation

- **Model**: `Elevator`, `ElevatorRequest`, `Direction` enums
- **Services**: 
  - `ElevatorController`: Manages elevators and handles requests.
  - `ElevatorScheduler`: Assigns requests to the best available elevator.
  - `RequestGenerator`: Generates random floor/direction requests.
- **Entry Point**: `Program.cs` runs the simulation loop.

## 🧪 Usage

Run the simulation from `Program.cs`. You can use:
- Random requests (default)
- Or uncomment the custom request block to simulate predefined scenarios.

---------------------------------------------------------------------------------

This is meant as a coding exercise. Focus was on clean structure and readable logic.


