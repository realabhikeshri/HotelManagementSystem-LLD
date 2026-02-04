using HotelManagementSystem_LLD.Enums;
using HotelManagementSystem_LLD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_LLD.Services
{
    public interface IBookingService
    {
        List<Room> SearchAvailableRooms(string hotelId, DateOnly checkIn, DateOnly checkOut, RoomType? type = null);
        Booking CreateBooking(Guest guest, string roomId, DateOnly checkIn, DateOnly checkOut);
        Booking GetBooking(string bookingId);
        void CheckIn(string bookingId);
        void CheckOut(string bookingId);
        void CancelBooking(string bookingId);
    }
}
