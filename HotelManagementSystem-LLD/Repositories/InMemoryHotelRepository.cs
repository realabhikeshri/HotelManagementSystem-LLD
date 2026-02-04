using HotelManagementSystem_LLD.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_LLD.Repositories
{
    public class InMemoryHotelRepository : IHotelRepository
    {
        private readonly ConcurrentDictionary<string, Hotel> _hotels = new();

        public Hotel Add(Hotel hotel)
        {
            _hotels[hotel.Id] = hotel;
            return hotel;
        }

        public Hotel? GetById(string hotelId)
        {
            _hotels.TryGetValue(hotelId, out var hotel);
            return hotel;
        }

        public List<Hotel> GetAll() => _hotels.Values.ToList();
    }
}
