
/* |--------------------------------------------------------|
 * |              Pathfinding Algorith Test Results         |
 * |--------------------------------------------------------| 
 * |    Algorithm       |   Speed   |   Tests   | Maze Size |
 * |--------------------------------------------------------|        
 * |Depth-First Search  |  0.0105ms |  1000000 | 1000 rooms |
 * |Breadth-First Search|  0.0018ms |  1000000 | 1000 rooms |
 * |        Dijkstra    |  0.0460ms |  1000000 | 1000 rooms |
 * |--------------------------------------------------------| 
 * |Specs                                                   |
 * |CPU:    Intel(R) Core(TM) i5-10600K CPU @ 4.10GHz       |
 * |GPU:    Radeon RX 480 4gb                               |
 * |Ram:    DD4 16gb 4200MHz                                |
 * |--------------------------------------------------------|
 * 
 * Test reults proved that Breadth-First Search algorithn is
 * faster and will be used for pathfinding
 */


using System.Collections.Generic;
using Priority_Queue;

namespace RoomPathfinding
{

    public static class Pathfinder
    {
        private static Maze maze;
       
        public static void SetMaze(Maze newMaze)
        {
            maze = newMaze;
        }

        public static bool HasPathBFS(string startingRoom, string targetRoom)
        {
            int startRoomHashCode = maze.RoomHashCode(startingRoom);
            int targetRoomHashCode = maze.RoomHashCode(targetRoom);

            Queue<int> toSearch = new Queue<int>();
            List<int> explored = new List<int>();
            toSearch.Enqueue(startRoomHashCode);

            while (toSearch.Count > 0)
            {
                int currentRoomHashCode = toSearch.Dequeue();
                if (currentRoomHashCode == targetRoomHashCode)
                {
                    return true;
                }
                
                Room currentRoom = maze.GetRoom(currentRoomHashCode);
                foreach (int hashCode in currentRoom.Connections.Values) {
                    if (!explored.Contains(hashCode))
                    {
                        explored.Add(hashCode);
                        toSearch.Enqueue(hashCode);
                    }
                }
            }

            return false;

        }

        public static bool HasPathDFS(string startingRoom, string targetRoom)
        {
            int startRoomHashCode = maze.RoomHashCode(startingRoom);
            int targetRoomHashCode = maze.RoomHashCode(targetRoom);

            Stack<int> toSearch = new Stack<int>();
            List<int> explored = new List<int>();
            toSearch.Push(startRoomHashCode);

            while (toSearch.Count > 0)
            {
                int currentRoomHashCode = toSearch.Pop();
                if (currentRoomHashCode == targetRoomHashCode)
                {
                    return true;
                }

                Room currentRoom = maze.GetRoom(currentRoomHashCode);
                foreach (int hashCode in currentRoom.Connections.Values)
                {
                    if (!explored.Contains(hashCode))
                    {
                        explored.Add(hashCode);
                        toSearch.Push(hashCode);
                    }
                }
            }

            return false;

        }
    
        public static bool HasPathDijkstra(string startingRoom, string targetRoom)
        {
            int startRoomHashCode = maze.RoomHashCode(startingRoom);
            int targetRoomHashCode = maze.RoomHashCode(targetRoom);

            Dictionary<int, int> distanceScore = new Dictionary<int, int>();
            SimplePriorityQueue<int, int> toSearch = new SimplePriorityQueue<int, int>();

            toSearch.Enqueue(startRoomHashCode,0);
            distanceScore.Add(startRoomHashCode, 0);

            while (toSearch.Count > 0)
            {
                int currentRoomHashCode = toSearch.Dequeue();
                if (currentRoomHashCode == targetRoomHashCode)
                {
                    return true;
                }

                Room currentRoom = maze.GetRoom(currentRoomHashCode);
                foreach (int connectedRoomHashCode in currentRoom.Connections.Values)
                {
                    if (!distanceScore.ContainsKey(connectedRoomHashCode))
                    {
                        distanceScore.Add(connectedRoomHashCode, int.MaxValue);
                    }


                    int currentScore = distanceScore[currentRoomHashCode] + 1;
                    if(currentScore< distanceScore[connectedRoomHashCode])
                    {
                        distanceScore[connectedRoomHashCode] = currentScore;
                        if (!toSearch.Contains(connectedRoomHashCode))
                        {
                            toSearch.Enqueue(connectedRoomHashCode, currentScore);
                        }
                    }
                   
                }
            }

            return false;

        }

    }
}
