using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Specialized;
using System.Linq;
namespace Elevator
{
    public class ElevatorController : IElevatorController
    {
        public event Action<int> ReachedSummonedFloor;
        public event Action<int> ReachedDestinationFloor;

        private Direction pathDirection=Direction.Up;
        private ElevatorMotor elevatorMotor;

        private List<int> destinationFloors = new List<int>();
        private Dictionary<Direction,List<int>> directionSummonedFloor = new Dictionary<Direction, List<int>>();
        public ElevatorController()
        {
            elevatorMotor = new ElevatorMotor();
            elevatorMotor.ReachedFloor += FloorReached;
        }

        public void FloorButtonPushed(int floor)
        {
            if (!destinationFloors.Contains(floor))
            {
                destinationFloors.Add(floor);
            }
        }

        public void SummonButtonPushed(int floor, Direction direction)
        {
            if (!directionSummonedFloor[direction].Contains(floor))
                directionSummonedFloor[direction].Add(floor);
        }
      
        /*summary
         * When a floor is reached  remove the floor from the destination list
         * If there is a summon request for the same direction as the elevatorMotor
         * remove it from summonList
         * If there are floor requests or summon requests find next floor
         * summary
         */
        private void FloorReached(int floorReached)
        {

            if (directionSummonedFloor[pathDirection].Contains(floorReached))
            {
                directionSummonedFloor[pathDirection].Remove(floorReached);
                ReachedSummonedFloor?.Invoke(floorReached);
            }          

            if (destinationFloors.Contains(floorReached))
            {
                destinationFloors.Remove(floorReached);
                ReachedDestinationFloor?.Invoke(floorReached);
            }

            if (destinationFloors.Count > 0 || directionSummonedFloor.Count > 0)
                FindNextFloor();
        }


        /*summary
        * Find the next
        * summary
        */
        private void FindNextFloor()
        {
            int nextFloor = GetClosestFloor(pathDirection);

            if (pathDirection == Direction.Up)
            {
                nextFloor = GetClosestFloor(pathDirection);
                if (nextFloor == -1)
                {
                    pathDirection = Direction.Down;
                    FindNextFloor();
                }
            }
            else if(pathDirection == Direction.Down)
            {
                nextFloor = GetClosestFloor(pathDirection);
                if (nextFloor == -1)
                {
                    pathDirection = Direction.Up;
                    FindNextFloor();
                }
            }

            elevatorMotor.GoToFloor(nextFloor);
        }

        private int GetClosestFloor(Direction direction)
        {           
            FloorLevel floorLevel;
            if (direction == Direction.Up)
            {
                floorLevel = FloorLevel.Above;
            }
            else
            {
                floorLevel = FloorLevel.Below;
            }
            List<int> availableSummonedFloors = AvailableFloors(directionSummonedFloor[direction], floorLevel);
            List<int> anailableDestinationFloors= AvailableFloors(destinationFloors, floorLevel);
            List<int> availableFloors = availableSummonedFloors.Concat(anailableDestinationFloors).ToList();

            int distance = 10;
            int nextFloor = -1;
            foreach (int floor in availableFloors)
            {
                if (floor - elevatorMotor.CurrentFloor < distance)
                {
                    nextFloor = floor;
                    distance = floor - elevatorMotor.CurrentFloor;
                }
            }
         
            return nextFloor;
        }

        private List<int> AvailableFloors(List<int> floors, FloorLevel floorLevel)
        {
            List<int> availableFloors = new List<int>();
            for(int i = 0; i < floors.Count; i++)
            {
                if(floorLevel==FloorLevel.Above&&floors[i]> elevatorMotor.CurrentFloor)
                {
                    availableFloors.Add(floors[i]);
                }else if(floorLevel == FloorLevel.Below && floors[i] < elevatorMotor.CurrentFloor)
                {
                    availableFloors.Add(floors[i]);
                }
            }

            return availableFloors;
        }

        private Direction ReverseDirection(Direction direction)
        {
            if (direction == Direction.Up)
            {
                return Direction.Down;
            }
            else
            {
                return Direction.Up;
            }
        }

    }

    public enum FloorLevel { Above,Below,Same}
}
