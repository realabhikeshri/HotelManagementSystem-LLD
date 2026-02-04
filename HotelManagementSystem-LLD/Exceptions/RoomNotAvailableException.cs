using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_LLD.Exceptions
{
    public class RoomNotAvailableException : Exception
    {
        public RoomNotAvailableException(string message) : base(message) { }
    }
}
