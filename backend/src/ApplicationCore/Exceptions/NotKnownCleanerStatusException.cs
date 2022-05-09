using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyKlinest.ApplicationCore.Exceptions
{
    public class NotKnownCleanerStatusException: Exception
    {
        public NotKnownCleanerStatusException(string status) : base($"Not known cleaner status: {status}")
        {
            this.status = status; 
        }

        private string status;
    }
}
