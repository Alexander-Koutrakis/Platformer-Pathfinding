using System.Collections.Generic;
using RoomPathfinding;
using System;

/* This Script is used for testing Pathfinding algorith in MazeTestConstructor
 * Generate a room maze
 * each room is randomly connected
 */
public class RandomMazeConstructor
{
    private int mazeSize;
    private List<int> allRoomsHashCodes = new List<int>();
    public Maze Maze { private set; get; }

    public RandomMazeConstructor(int size)
    {
        mazeSize = size;
        CreateNewMaze();
    }


   

    //Create all the rooms
    //for each room create a random number of connections
    private void CreateNewMaze()
   {
        Maze = new Maze();
        for(int i=0;i< mazeSize; i++)
        {
            AddRoom(i);
        }

        for(int i = 0; i < mazeSize; i++)
        {
            int parentRoomHashCode = allRoomsHashCodes[i];
            CreateRandomConnections(parentRoomHashCode);
        }
   }
    private void CreateRandomConnections(int parentRoomHashCode)
    {
        int randomConnections = UnityEngine.Random.Range(10, 30);
        for(int i = 0; i < 5; i++)
        {
            int index = UnityEngine.Random.Range(0, allRoomsHashCodes.Count);
            CreateRandomConnection(parentRoomHashCode, allRoomsHashCodes[index]);
        }
    }
    private void CreateRandomConnection(int parentRoomHashCode,int connectedRoomHashCode)
    {
        Direction randomDirection = RandomDirection();
        Maze.AddConnection(parentRoomHashCode, connectedRoomHashCode, randomDirection);
    }
    private Direction RandomDirection()
    {
        System.Random random = new System.Random();
        Type type = typeof(Direction);
        Array values = type.GetEnumValues();
        int index = random.Next(values.Length);
        Direction direction = (Direction)values.GetValue(index);
        return direction;
    }
    private void AddRoom(int index)
    {
        string roomName = "Room" + index;
        Maze.AddRoom(roomName);
        int roomHashCode = Maze.RoomHashCode(roomName);
        allRoomsHashCodes.Add(roomHashCode);
    }
}
