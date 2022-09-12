
using System.Collections.Generic;
namespace RoomPathfinding
{
    /*
     * Used struct to hold Room Data to avoid memory allocation
     * and garbage collection on heap
     * 
     * Each room is identified by its <int>HashCode for faster comparisons
     */
    public struct Room : IPathable
    {
        public int HasCode { private set; get; }
        public string Name { private set; get; }
        public Dictionary<Direction, int> Connections { private set; get; }
        public override bool Equals(object obj)
        {
            return obj is Room room &&
                   HasCode == room.HasCode;
        }
        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }


        public Room(string name)
        {
            this.Name = name;
            this.Connections = new Dictionary<Direction, int>();
            this.HasCode = 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public void AddConnection(Direction direction, int roomHasCode)
        {
            if (Connections.ContainsKey(direction))
            {
                Connections[direction] = roomHasCode;
            }
            else
            {
                Connections.Add(direction, roomHasCode);
            }
        }

        public bool PathExists(string roomName)
        {
            return Pathfinder.HasPathBFS(Name, roomName);
        }
    }

    public enum Direction { North, East, South, West }
}