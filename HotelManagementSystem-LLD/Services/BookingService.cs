using HotelManagementSystem_LLD.Enums;
using HotelManagementSystem_LLD.Exceptions;
using HotelManagementSystem_LLD.Models;
using HotelManagementSystem_LLD.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_LLD.Services
{
    public class BookingService : IBookingService
    {
        private readonly IHotelRepository _hotelRepo;
        private readonly IRoomRepository _roomRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly IPricingService _pricingService;
        private readonly PaymentService _paymentService;

        public BookingService(
            IHotelRepository hotelRepo,
            IRoomRepository roomRepo,
            IBookingRepository bookingRepo,
            IPricingService pricingService,
            PaymentService paymentService)
        {
            _hotelRepo = hotelRepo;
            _roomRepo = roomRepo;
            _bookingRepo = bookingRepo;
            _pricingService = pricingService;
            _paymentService = paymentService;
        }

        public List<Room> SearchAvailableRooms(string hotelId, DateOnly checkIn, DateOnly checkOut, RoomType? type = null)
        {
            var hotel = _hotelRepo.GetById(hotelId) ?? throw new ArgumentException("Invalid hotel id");

            var rooms = _roomRepo.GetByHotel(hotel.Id, type);
            var available = new List<Room>();

            foreach (var room in rooms)
            {
                var bookings = _bookingRepo.GetBookingsForRoom(room.Id)
                    .Where(b => b.Status is not BookingStatus.Cancelled)
                    .ToList();

                bool overlaps = bookings.Any(b => b.Overlaps(checkIn, checkOut));
                if (!overlaps && room.Status == RoomStatus.Available)
                {
                    available.Add(room);
                }
            }

            return available;
        }

        public Booking CreateBooking(Guest guest, string roomId, DateOnly checkIn, DateOnly checkOut)
        {
            var room = _roomRepo.GetById(roomId) ?? throw new RoomNotAvailableException("Room not found");

            // Concurrency-safe check
            lock (room)
            {
                var existing = _bookingRepo.GetBookingsForRoom(room.Id)
                    .Where(b => b.Status is not BookingStatus.Cancelled)
                    .ToList();

                if (existing.Any(b => b.Overlaps(checkIn, checkOut)))
                {
                    throw new RoomNotAvailableException("Room already booked for given dates");
                }

                var total = _pricingService.CalculateTotal(room, checkIn, checkOut);
                var booking = new Booking(Guid.NewGuid().ToString(), room.HotelId, room.Id, guest, checkIn, checkOut, total);
                _bookingRepo.Add(booking);

                // Optionally mark room as occupied only on check-in; here keep it available until then
                return booking;
            }
        }

        public Booking GetBooking(string bookingId)
        {
            return _bookingRepo.GetById(bookingId)
                ?? throw new BookingNotFoundException("Booking not found");
        }

        public void CheckIn(string bookingId)
        {
            var booking = GetBooking(bookingId);
            var room = _roomRepo.GetById(booking.RoomId) ?? throw new RoomNotAvailableException("Room not found");

            if (!room.TryMarkOccupied())
                throw new RoomNotAvailableException("Room is not available to check-in");

            booking.CheckIn();
            _bookingRepo.Update(booking);
            _roomRepo.Update(room);
        }

        public void CheckOut(string bookingId)
        {
            var booking = GetBooking(bookingId);
            var room = _roomRepo.GetById(booking.RoomId) ?? throw new RoomNotAvailableException("Room not found");

            booking.CheckOut();
            room.MarkAvailable();

            _bookingRepo.Update(booking);
            _roomRepo.Update(room);
        }

        public void CancelBooking(string bookingId)
        {
            var booking = GetBooking(bookingId);
            booking.Cancel();
            _bookingRepo.Update(booking);
        }
    }
}
