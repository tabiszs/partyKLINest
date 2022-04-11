using PartyKlinest.ApplicationCore.Entities;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Factories
{
    public  class CleanerBuilder
    {
        private Cleaner _cleaner;
        public string cleanerId = "123";
        public MessLevel maxMessLevel = MessLevel.Huge;
        public decimal minPrice = 100;
        public int minClientRating = 4;
        public CleanerStatus status = CleanerStatus.Active;

        public CleanerBuilder()
        {
            _cleaner = GetWithDefaultValues();
        }

        private Cleaner GetWithDefaultValues()
        {
            var ord = new Cleaner();
            ord.CleanerId = cleanerId;
            ord.SetCleanerStatus(status);
            ord.SetMaxMessLevel(maxMessLevel);
            ord.SetMinPrice(minPrice);
            ord.SetMinCientRating(minClientRating);
            return ord;
        }

        public Cleaner Build()
        {
            return _cleaner;
        }

    }
}
