using System;
using System.Collections.Generic;

namespace Elevator
{


    /*summary
     * We used a modified Look algorith
     * where we get all destinations on the same direction that we are already moving.
     * 
     * If there are no more request on hat direction we get the further up or down
     * request of the opposite direction(The highest for downRequests and Lowest for UpRequests)
     * 
     * Requests are represents as boolean arrays where every index represents
     * a floor and if its true there is a request
     */
    public class ElevatorController : IElevatorController
    {
        public event Action<int> ReachedSummonedFloor;
        public event Action<int> ReachedDestinationFloor;

        private ElevatorMotor elevatorMotor;

        private bool[] upRequests = new bool[10] {false, false, false, false, false, false, false, false, false, false };
        private bool[] downRequests = new bool[10] { false, false, false, false, false, false, false, false, false, false };

        private Direction pathDirection;

        private bool[] destinationFloors= new bool[10] {false, false, false, false, false, false, false, false, false, false };
        private Dictionary<Direction, List<int>> summonFloors = new Dictionary<Direction, List<int>>();

    public ElevatorController()
        {
            elevatorMotor = new ElevatorMotor();
            elevatorMotor.ReachedFloor += FloorReached;
        }

       
        public void FloorButtonPushed(int floor)
        {
            if (elevatorMotor.CurrentFloor < floor)
            {
                upRequests[floor] = true;
            }else if(elevatorMotor.CurrentFloor > floor)
            {
                downRequests[floor] = true;
            }

            destinationFloors[floor] = true;
        }

        /*The summon requests are added to the directions they are aimed to
         * Example:
         * If we are on floor 5 going up , the new request on floor 7 for down
         * will be adden to the downResquest and ingored by the elevator going up
         */
        public void SummonButtonPushed(int floor, Direction direction)
        {
            if (direction == Direction.Up)
            {
                upRequests[floor] = true;
            }
            else
            {
                downRequests[floor] = true;
            }

            if (elevatorMotor.CurrentDirection == Direction.Stationary)
            {
                elevatorMotor.GoToFloor(floor);
            }

            if (!summonFloors[direction].Contains(floor))
            {
                summonFloors[direction].Add(floor);
            }
        }

        public void FloorReached(int floor)
        {
            if (pathDirection == Direction.Down)
            {
                if (downRequests[floor])
                {
                    downRequests[floor] = false;
                }
                //if we came to this floor while decending and it wasnt requested on downrequest
                //It means that the requests bellow this floor are finished, this is the lowest
                //Up request and we should start going Up
                else
                {
                    upRequests[floor] = false;
                    pathDirection = Direction.Up;
                }
                
            }else if (pathDirection == Direction.Up)
            {
                if (upRequests[floor])
                {
                    upRequests[floor] = false;
                }
                //if we came to this floor while ascending and it wasnt requested on upRequest
                //It means that the requests above this floor are finished, this is the highest
                //Down request and we should start going Up
                else
                {
                    upRequests[floor] = false;
                    pathDirection = Direction.Down;
                }
            }

            NextFloor(pathDirection);


            if (destinationFloors[floor])
            {
                ReachedSummonedFloor?.Invoke(floor);
                destinationFloors[floor] = false;
            }

            if (summonFloors[pathDirection].Contains(floor))
            {
                ReachedSummonedFloor?.Invoke(floor);
                summonFloors[pathDirection].Remove(floor);
            }
        }



        private void NextFloor(Direction direction)
        {
            if (direction == Direction.Up)
            {
                //look for the next floor request in that direction
                for(int i = elevatorMotor.CurrentFloor; i < upRequests.Length;i++)
                {
                    if (upRequests[i])
                    {
                        elevatorMotor.GoToFloor(i);
                        return;
                    }
                }
                
                //if there are no more requests of that direction
                //look for the last floor request of the opposite direction
                //Example
                //We are on floor 5 the last moving up floor
                //look for the highest down request an go there
                //
                for(int i= downRequests.Length-1; i > elevatorMotor.CurrentFloor; i--)
                {
                    if (downRequests[i])
                    {
                        elevatorMotor.GoToFloor(i);
                        return;
                    }
                }
            }
            else if(direction == Direction.Down)
            {
                //look for the next floor request in that direction
                for (int i = elevatorMotor.CurrentFloor; i > 0; i--)
                {
                    if (upRequests[i])
                    {
                        elevatorMotor.GoToFloor(i);
                        return;
                    }
                }

                //if there are no more requests of that direction
                //look for the last floor request of the opposite direction
                //Example
                //We are on floor 5 the last moving up floor
                //look for the lowest up request an go there
                //
                for (int i = 0 - 1; i < elevatorMotor.CurrentFloor; i++)
                {
                    if (upRequests[i])
                    {
                        elevatorMotor.GoToFloor(i);
                        return;
                    }
                }
            }

            //If there are no Requests Stop Elevator
            elevatorMotor.StopElevator();
        }

    }
}
