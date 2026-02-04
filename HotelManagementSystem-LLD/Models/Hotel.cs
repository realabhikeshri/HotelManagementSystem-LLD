using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_LLD.Models
{
    public class Hotel
    {
        public string Id { get; }
        public string Name { get; }
        public string Location { get; }

        public Hotel(string name, string location)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Location = location;
        }
    }
}
