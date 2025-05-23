using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderModules
{
    public class Order : BaseEntity<int>
    {
        public Order()
        {
            
        }

        public Order(string userEmail,
                    OrderAddress address, 
                    IEnumerable<OrderItem> orderItems, 
                    DeliveryMethod deliveryMethod, 
                    decimal subTotal)
        {
            UserEmail = userEmail;
            Address = address;
            OrderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
        }

        public Order(string userEmail,
               OrderAddress address,
               IEnumerable<OrderItem> orderItems,
               DeliveryMethod deliveryMethod,
               decimal subTotal,
               string paymentIntent)
        {
            UserEmail = userEmail;
            Address = address;
            OrderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntent;
        }



        // email of user
        public string UserEmail { get; set; }
        // Address (shiping address)
        public OrderAddress Address { get; set; }
        // order items
        // Nav prop
        public IEnumerable<OrderItem> OrderItems { get; set; }

        // payment status
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        // delivery method
        // Nave prop
        public int? DeliveryMethodId { get; set; }
        public DeliveryMethod? DeliveryMethod { get; set; }

        // sub total => order items (sum of price * quantity)
        public decimal SubTotal { get; set; }

        // total => sub total + shipping cost
        public decimal Total { get; set; }

        // payment IntentId
        public string? PaymentIntentId { get; set; }

        // Order Date
        public DateTimeOffset OrderDate { get; internal set; } = DateTimeOffset.Now;
    }
}
