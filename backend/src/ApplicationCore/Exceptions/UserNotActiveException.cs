using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyKlinest.ApplicationCore.Exceptions
{
    public  class UserNotActiveException : Exception
    {
        public UserNotActiveException(Cleaner cleaner)
            : base($"cliner {cleaner.CleanerId} is {cleaner.Status} != Active")
        {
            Cleaner = cleaner;
        }
        
        public Cleaner Cleaner { get; set; }
    }
}
