using System;
using System.Collections.Generic;

namespace Elevator
{

    /*summary
     * We used a modified LOOK Disk Scheduling algorithm
     * where we serve all destinations on the same direction that we are already moving.
     * 
     * If there are no more request on that direction we serve the first of the opposite direction,
     * The highest downRequest or the lowestUpRequest.
     * 
     * Requests are represents as boolean arrays where every index represents
     * a floor and if its true there is a request
     */
    public class ElevatorController : IElevatorController
    {
        public event Action<int> ReachedSummonedFloor;
        public event Action<int> ReachedDestinationFloor;

        private ElevatorMotor elevatorMotor;

        private Dictionary<Direction, bool[]> floorRequests = new Dictionary<Direction, bool[]>();
        private Direction pathDirection=Direction.Up;

        //use this array to trigger ReachDestinationFloor event
        private bool[] destinationFloors= new bool[10] {false, false, false, false, false, false, false, false, false, false };

        //use this Dictionary to trigger ReachedSummonedFloor event
        private Dictionary<Direction, List<int>> summonFloors = new Dictionary<Direction, List<int>>();

         public ElevatorController()
         {
            elevatorMotor = new ElevatorMotor();
            elevatorMotor.ReachedFloor += FloorReached;

            bool[] upRequests = new bool[10] { false, false, false, false, false, false, false, false, false, false };
            bool[] downRequests = new bool[10] { false, false, false, false, false, false, false, false, false, false };
            floorRequests.Add(Direction.Up, upRequests);
            floorRequests.Add(Direction.Down, downRequests);
         }

       
        public void FloorButtonPushed(int floor)
        {
            if (elevatorMotor.CurrentFloor < floor)
            {
                floorRequests[Direction.Up][floor] = true;
            }else if(elevatorMotor.CurrentFloor > floor)
            {
                floorRequests[Direction.Down][floor] = true;
            }

            //Add floor to destination array to trigger ReachDestinationFloor
            destinationFloors[floor] = true;
        }

        /*The summon requests are added to the directions they are aimed to
         * Example:
         * If we are on floor 5 going up , the new request on floor 7 for down
         * will be adden to the downResquest
         */
        public void SummonButtonPushed(int floor, Direction direction)
        {
            floorRequests[direction][floor] = true;

            //Add floor to SummonDictionary to trigger ReachedSummonedFloor event
            if (!summonFloors[direction].Contains(floor))
            {
                summonFloors[direction].Add(floor);
            }


            //Start elevator if its Direction.Stationary
            if (elevatorMotor.CurrentDirection == Direction.Stationary)
            {
                elevatorMotor.GoToFloor(floor);
            }
        }

        //When reaching a floor remove the floor from floorRequests
        //If the floor is a destination floor invoke ReachedDestinationFloor event
        //If the floor is a summon floor invoke ReachSummonedFloor event
        public void FloorReached(int floor)
        {
            floorRequests[pathDirection][floor] = false;


            if (destinationFloors[floor])
            {
                ReachedDestinationFloor?.Invoke(floor);
                destinationFloors[floor] = false;
            }

            if (summonFloors[pathDirection].Contains(floor))
            {
                ReachedSummonedFloor?.Invoke(floor);
                summonFloors[pathDirection].Remove(floor);
            }

            GoToNextFloor();
        }

        //look for the next floor request in that direction
        //if there are no more requests of that direction
        //look for the last floor request of the opposite direction
        //example:
        //We are on floor 5, the last moving up floor,
        //look for the highest downRequest an start from there
        private void GoToNextFloor()
        {
            int nextFloor = -1;
            if (pathDirection == Direction.Up)
            {
                nextFloor = NextUp();
                if (nextFloor < 0)
                    nextFloor = HighestDownRequest();
            }
            else if(pathDirection == Direction.Down)
            {
                nextFloor = NextDown();
                if (nextFloor < 0)
                    nextFloor = LowestUpRequest();
            }


            //If there are no Requests Stop Elevator
            if (nextFloor < 0)
            {
                elevatorMotor.StopElevator();
            }
            else
            {
                elevatorMotor.GoToFloor(nextFloor);
            }
        }

        private int NextUp()
        {
            for (int i = elevatorMotor.CurrentFloor; i < floorRequests[Direction.Up].Length; i++)
            {
                if (floorRequests[Direction.Up][i])
                {                    
                    return i;
                }
            }            
            return -1;
        }

        private int LowestUpRequest()
        {
            for(int i=0;i< floorRequests[Direction.Up].Length; i++)
            {
                if (floorRequests[Direction.Up][i])
                {
                    pathDirection = Direction.Up;
                    return i;
                }
            }
            return -1;
        }

        private int NextDown()
        {
            for (int i = elevatorMotor.CurrentFloor; i > 0; i--)
            {
                if (floorRequests[Direction.Down][i])
                {
                    return i;
                }
            }
            return -1;
        }

        private int HighestDownRequest()
        {
            for (int i = floorRequests[Direction.Down].Length - 1; i > 0; i--)
            {
                if (floorRequests[Direction.Down][i])
                {
                    pathDirection = Direction.Down;
                    return i;
                }
            }

            return -1;
        }
   
    }
}
