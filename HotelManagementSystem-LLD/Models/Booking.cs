using HotelManagementSystem_LLD.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_LLD.Models
{
    public class Booking
    {
        public string Id { get; }
        public string HotelId { get; }
        public string RoomId { get; }
        public Guest Guest { get; }
        public DateOnly CheckInDate { get; }
        public DateOnly CheckOutDate { get; }
        public BookingStatus Status { get; private set; }
        public decimal TotalAmount { get; }

        public Booking(string id, string hotelId, string roomId, Guest guest,
            DateOnly checkInDate, DateOnly checkOutDate, decimal totalAmount)
        {
            Id = id;
            HotelId = hotelId;
            RoomId = roomId;
            Guest = guest;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            TotalAmount = totalAmount;
            Status = BookingStatus.Confirmed;
        }

        public void CheckIn()
        {
            if (Status != BookingStatus.Confirmed)
                throw new InvalidOperationException("Can only check-in confirmed bookings");
            Status = BookingStatus.CheckedIn;
        }

        public void CheckOut()
        {
            if (Status != BookingStatus.CheckedIn)
                throw new InvalidOperationException("Can only check-out checked-in bookings");
            Status = BookingStatus.CheckedOut;
        }

        public void Cancel()
        {
            if (Status == BookingStatus.CheckedOut)
                throw new InvalidOperationException("Cannot cancel checked-out booking");
            Status = BookingStatus.Cancelled;
        }

        public bool Overlaps(DateOnly start, DateOnly end)
        {
            return start < CheckOutDate && end > CheckInDate;
        }
    }
}
