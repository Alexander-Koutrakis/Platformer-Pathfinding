using System.Collections.Generic;
namespace RoomPathfinding {

    //Maze of rooms is mapped by each room hashCode
    //there is a second map based on the name of each room
    //to help 
    public class Maze
    {
        private Dictionary<int, Room> roomMap = new Dictionary<int, Room>();
        private Dictionary<string, int> roomNameMap = new Dictionary<string, int>();
        
        public int RoomHashCode(string name)
        {            
            return roomNameMap[name];
        }

        public Room GetRoom(int hashCode)
        {           
            return roomMap[hashCode];
        }

        public Room GetRoom(string roomName)
        {
            int hashCode = roomNameMap[roomName];
            return roomMap[hashCode];
        }

        public void AddRoom(string roomName)
        {
            Room room = new Room(roomName);
            roomNameMap.Add(roomName, room.HasCode);
            roomMap.Add(room.HasCode, room);
        }
        public void AddConnection(string roomName,string connectedRoomName, Direction direction)
        {
            int roomHashCode = roomNameMap[roomName];
            int connectedRoomHashCode = roomNameMap[connectedRoomName];
            AddConnection(roomHashCode, connectedRoomHashCode,direction);
        }

        public void AddConnection(int roomHashCode, int connectedRoomHashCode, Direction direction)
        {
            Room room = roomMap[roomHashCode];
            room.AddConnection(direction, connectedRoomHashCode);
            roomMap[roomHashCode] = room;
        }
    }
}
