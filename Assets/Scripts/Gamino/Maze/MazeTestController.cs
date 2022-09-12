
using UnityEngine;
using RoomPathfinding;
using UnityEditor;
using System.Diagnostics;
using System;

/* this class is used for testing the Pathfinder results
 * Steps:
 *  -Add the script on a gameobject
 *  -Create Maze
 *  -Enter the names of the Rooms you want to test
 *      *Room names are Room+number from 0-999 (example Room345)
 *  -Get reults
 * 
 * Show Connections shows the connections of each room and its used for Debugging
 */

#if UNITY_EDITOR
public class MazeTestController : MonoBehaviour
{
    private Maze maze;
    public string StartingRoom;
    public string TargetRoom;
    public void CreateMaze()
    {
        RandomMazeConstructor randomMazeConstructor = new RandomMazeConstructor();
        maze = randomMazeConstructor.GetRandomMaze();
        Pathfinder.SetMaze(maze);
    }

    public void HasPathTest()
    {
        Room startRoom=maze.GetRoom(StartingRoom);
        Room targetRoom = maze.GetRoom(TargetRoom);
        bool hasPath = false;
        int numberOfTests = 1000000;
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < numberOfTests; i++)
        {
            hasPath = startRoom.PathExists(TargetRoom);
        }
        stopwatch.Stop();
        TimeSpan timeSpan = stopwatch.Elapsed;
        UnityEngine.Debug.Log("Average Pathfind "+timeSpan.TotalMilliseconds/ numberOfTests);
        UnityEngine.Debug.Log("Path Exist "+hasPath+" from "+startRoom.Name+" to "+targetRoom.Name);
    }

    public void ShowConnections()
    {
        Room room = maze.GetRoom(StartingRoom);
        foreach (int connectedRoomHash in room.Connections.Values)
        {
            Room connectedRoom = maze.GetRoom(connectedRoomHash);
            UnityEngine.Debug.Log(connectedRoom.Name);
        }      
    }

}
#endif

[CustomEditor(typeof(MazeTestController))]
public class MazeTestControllerEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MazeTestController mazeTestController = (MazeTestController)target;

        if(GUILayout.Button("Create Maze"))
        {
            mazeTestController.CreateMaze();
        }

        if (GUILayout.Button("Has Path"))
        {
            mazeTestController.HasPathTest();
        }

        if (GUILayout.Button("Show Connections"))
        {
            mazeTestController.ShowConnections();
        }
    }

}

