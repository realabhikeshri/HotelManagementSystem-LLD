using HotelManagementSystem_LLD.Enums;
using HotelManagementSystem_LLD.Models;
using HotelManagementSystem_LLD.Repositories;
using HotelManagementSystem_LLD.Services;

namespace HotelManagementSystem;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== Hotel Management System (C# LLD) ===");

        IHotelRepository hotelRepo = new InMemoryHotelRepository();
        IRoomRepository roomRepo = new InMemoryRoomRepository();
        IBookingRepository bookingRepo = new InMemoryBookingRepository();
        IPricingService pricingService = new PricingService();
        var paymentService = new PaymentService();

        IBookingService bookingService =
            new BookingService(hotelRepo, roomRepo, bookingRepo, pricingService, paymentService);

        // Setup: create hotel and rooms
        var hotel = hotelRepo.Add(new Hotel("Hotel Amsterdam", "Amsterdam"));
        var room1 = roomRepo.Add(new Room(hotel.Id, "101", RoomType.Single, 100m));
        var room2 = roomRepo.Add(new Room(hotel.Id, "102", RoomType.Double, 150m));

        var guest = new Guest(Guid.NewGuid().ToString(), "Alice", "alice@example.com", "+31-123456789");

        var checkIn = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(1));
        var checkOut = checkIn.AddDays(3);

        Console.WriteLine($"Searching rooms at {hotel.Name} from {checkIn} to {checkOut}");
        var available = bookingService.SearchAvailableRooms(hotel.Id, checkIn, checkOut, RoomType.Single);
        Console.WriteLine($"Available Single rooms: {available.Count}");

        if (available.Count > 0)
        {
            var booking = bookingService.CreateBooking(guest, available[0].Id, checkIn, checkOut);
            Console.WriteLine($"Created booking {booking.Id} amount: {booking.TotalAmount}, status: {booking.Status}");

            var payment = paymentService.ProcessPayment(booking);
            Console.WriteLine($"Payment status: {payment.Status}");

            bookingService.CheckIn(booking.Id);
            Console.WriteLine($"Booking {booking.Id} status after check-in: {booking.Status}");

            bookingService.CheckOut(booking.Id);
            Console.WriteLine($"Booking {booking.Id} status after check-out: {booking.Status}");
        }

        Console.WriteLine("Demo finished.");
    }
}
