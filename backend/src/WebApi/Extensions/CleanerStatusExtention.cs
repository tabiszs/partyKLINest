using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.ApplicationCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyKlinest.WebApi.Extensions
{
    public static class CleanerStatusExtention
    {
        public static CleanerStatus FromString(string status)
        {
            return status switch
            {
                "Registered" => CleanerStatus.Registered,
                "Banned" => CleanerStatus.Banned,
                "Active" => CleanerStatus.Active,
                _ => throw new NotKnownCleanerStatusException(status),
            };
        }

        public static string ToString(this CleanerStatus cleanerStatus)
        {
            return cleanerStatus switch
            {
                CleanerStatus.Registered => "Registered",
                CleanerStatus.Banned => "Banned",
                CleanerStatus.Active => "Active",
                _ => String.Empty,
            };
        }
    }
}
