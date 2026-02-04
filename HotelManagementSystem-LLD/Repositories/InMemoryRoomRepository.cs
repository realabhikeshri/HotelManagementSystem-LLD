using HotelManagementSystem_LLD.Enums;
using HotelManagementSystem_LLD.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_LLD.Repositories
{
    public class InMemoryRoomRepository : IRoomRepository
    {
        private readonly ConcurrentDictionary<string, Room> _rooms = new();

        public Room Add(Room room)
        {
            _rooms[room.Id] = room;
            return room;
        }

        public Room? GetById(string roomId)
        {
            _rooms.TryGetValue(roomId, out var room);
            return room;
        }

        public List<Room> GetByHotel(string hotelId, RoomType? type = null)
        {
            return _rooms.Values
                .Where(r => r.HotelId == hotelId)
                .Where(r => !type.HasValue || r.Type == type.Value)
                .ToList();
        }

        public void Update(Room room)
        {
            _rooms[room.Id] = room;
        }
    }
}
