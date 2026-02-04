using HotelManagementSystem_LLD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_LLD.Repositories
{
    public interface IHotelRepository
    {
        Hotel Add(Hotel hotel);
        Hotel? GetById(string hotelId);
        List<Hotel> GetAll();
    }
}
