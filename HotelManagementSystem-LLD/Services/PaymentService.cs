using HotelManagementSystem_LLD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_LLD.Services
{
    public class PaymentService
    {
        public Payment ProcessPayment(Booking booking)
        {
            // Mocked payment gateway: always succeed in this demo
            var payment = new Payment(Guid.NewGuid().ToString(), booking.Id, booking.TotalAmount);
            payment.MarkCompleted();
            return payment;
        }
    }
}
