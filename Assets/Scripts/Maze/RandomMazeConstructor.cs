using System.Collections.Generic;
using RoomPathfinding;
using System;

/* This Script is used for testing Pathfinding algorith
 * Generate a 1000 room maze
 * each room is randomly connected
 */
public class RandomMazeConstructor
{
    private int roomsLength=1000;
    private List<int> allRoomsHashCodes = new List<int>();
    private Maze maze;

    public Maze GetRandomMaze()
    {
        if (maze == null)
        {
            CreateNewMaze();
        }
        return maze;
    }
    private void CreateNewMaze()
   {
        maze = new Maze();
        for(int i=0;i< roomsLength; i++)
        {
            AddRoom(i);
        }

        for(int i = 0; i < roomsLength; i++)
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
        maze.AddConnection(parentRoomHashCode, connectedRoomHashCode, randomDirection);
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
        maze.AddRoom(roomName);
        int roomHashCode = maze.RoomHashCode(roomName);
        allRoomsHashCodes.Add(roomHashCode);
    }
}
