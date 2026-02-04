using HotelManagementSystem_LLD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_LLD.Services
{
    // Simple pricing: nights * base price, weekend +20% (for demo)
    public class PricingService : IPricingService
    {
        public decimal CalculateTotal(Room room, DateOnly checkIn, DateOnly checkOut)
        {
            if (checkOut <= checkIn)
                throw new ArgumentException("Check-out must be after check-in");

            decimal total = 0;
            for (var date = checkIn; date < checkOut; date = date.AddDays(1))
            {
                var price = room.BasePricePerNight;
                var dayOfWeek = date.ToDateTime(TimeOnly.MinValue).DayOfWeek;
                if (dayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday)
                {
                    price *= 1.2m;
                }

                total += price;
            }

            return total;
        }
    }
}
