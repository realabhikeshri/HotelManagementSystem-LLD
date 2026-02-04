using HotelManagementSystem_LLD.Enums;
using HotelManagementSystem_LLD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_LLD.Repositories
{
    public interface IRoomRepository
    {
        Room Add(Room room);
        Room? GetById(string roomId);
        List<Room> GetByHotel(string hotelId, RoomType? type = null);
        void Update(Room room);
    }
}
