using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Elevator
{
    public enum Direction { Stationary, Up, Down }
    public interface IElevatorMotor
    {
        Direction CurrentDirection { get; }
        int CurrentFloor { get; }
        event Action<int> ReachedFloor;
        void GoToFloor(int floor);
    }
    public interface IElevatorController
    {
        void SummonButtonPushed(int floor, Direction direction);
        void FloorButtonPushed(int floor);
        event Action<int> ReachedSummonedFloor;
        event Action<int> ReachedDestinationFloor;
    }
}
