using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyKlinest.ApplicationCore.Entities.Users
{
    public class User
    {
        public long UserId { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public long? ClientId { get; set; }
        public Client? Client { get; set; }

        public long? CleanerId { get; set; }
        public Cleaner? Cleaner { get; set; }

        public bool IsOrganizer { get; set; } = false;
    }
}
