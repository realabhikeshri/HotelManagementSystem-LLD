using HotelManagementSystem_LLD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_LLD.Repositories
{
    public interface IBookingRepository
    {
        Booking Add(Booking booking);
        Booking? GetById(string bookingId);
        void Update(Booking booking);
        List<Booking> GetBookingsForRoom(string roomId);
    }
}
