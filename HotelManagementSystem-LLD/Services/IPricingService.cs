using HotelManagementSystem_LLD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_LLD.Services
{
    public interface IPricingService
    {
        decimal CalculateTotal(Room room, DateOnly checkIn, DateOnly checkOut);
    }
}
