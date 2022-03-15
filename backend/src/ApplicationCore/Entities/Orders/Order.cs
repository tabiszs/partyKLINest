using PartyKlinest.ApplicationCore.Entities.Orders.Opinions;
using PartyKlinest.ApplicationCore.Entities.Users;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyKlinest.ApplicationCore.Entities.Orders
{
    public class Order
    {
        public long OrderId { get; set; }
        
        public DateTimeOffset TimeFrom { get; set; }
        public DateTimeOffset TimeTo { get; set; }

        public decimal MaxPrice { get; set; }
        public int MinCleanerRating { get; set; }
        
        public MessLevel MessLevel { get; set; }
        public OrderStatus Status { get; set; }


        public long ClientId { get; set; }
        public Client Client { get; set; }

        public long? CleanerId { get; set; }
        public Cleaner? Cleaner { get; set; }

        public long AddressId { get; set; }
        public Address Address { get; set; }

        public long? ClientsOpinionId { get; set; }
        public Opinion? ClientsOpinion { get; set; }

        public long? CleanersOpinionId { get; set; }
        public Opinion? CleanersOpinion { get; set; }
    }
}
