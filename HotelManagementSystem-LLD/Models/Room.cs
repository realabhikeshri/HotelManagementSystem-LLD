using HotelManagementSystem_LLD.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_LLD.Models
{
    public class Room
    {
        private readonly object _lock = new();

        public string Id { get; }
        public string HotelId { get; }
        public string RoomNumber { get; }
        public RoomType Type { get; }
        public RoomStatus Status { get; private set; }
        public decimal BasePricePerNight { get; }

        public Room(string hotelId, string roomNumber, RoomType type, decimal basePricePerNight)
        {
            Id = Guid.NewGuid().ToString();
            HotelId = hotelId;
            RoomNumber = roomNumber;
            Type = type;
            Status = RoomStatus.Available;
            BasePricePerNight = basePricePerNight;
        }

        public bool TryMarkOccupied()
        {
            lock (_lock)
            {
                if (Status != RoomStatus.Available) return false;
                Status = RoomStatus.Occupied;
                return true;
            }
        }

        public void MarkAvailable()
        {
            lock (_lock)
            {
                Status = RoomStatus.Available;
            }
        }
    }
}
