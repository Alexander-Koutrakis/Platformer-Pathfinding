using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elevator {

    public class ElevatorMotor : IElevatorMotor
    {
        public Direction CurrentDirection { get { return currentDirection; } }
        private Direction currentDirection;
        private int currentFloor;
        public int CurrentFloor { get { return currentFloor; } }
    
        public event Action<int> ReachedFloor;

        public void GoToFloor(int floor)
        {
            while(floor != currentFloor)
            {
                currentDirection = GetDirection(floor);
                Move(currentDirection);
            }

            ReachedFloor?.Invoke(floor);
                      
        }

        private void Move(Direction direction)
        {
            if (direction == Direction.Up)
            {
                if (currentFloor < 9)
                {
                    currentFloor++;
                }
            }else if (direction == Direction.Down)
            {
                if (currentFloor > 0)
                {
                    currentFloor--;
                }
            }
        }

        private Direction GetDirection(int floor)
        {
            if (floor > currentFloor)
            {
                return Direction.Up;
            }
            else if (floor < currentFloor)
            {
                return Direction.Down;
            }
            else
            {
                return Direction.Stationary;
            }
        }
    }
}
