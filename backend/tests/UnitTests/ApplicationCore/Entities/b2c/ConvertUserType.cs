﻿using PartyKlinest.ApplicationCore.Entities;
using PartyKlinest.ApplicationCore.Entities.b2c;
using Xunit;

namespace UnitTests.ApplicationCore.Entities.b2c
{
    public class ConvertUserType
    {
        private readonly string _stringUserType;
        private readonly UserType _enumUserType;

        public ConvertUserType()
        {
            _stringUserType = "Client";
            _enumUserType = UserType.Client;
        }

        [Fact]
        public void StringToEnum()
        {
            UserType newEnumUserType = UserTypeConverter.StringToEnum(_stringUserType);

            Assert.Equal(newEnumUserType, _enumUserType);
        }

        [Fact]
        public void EnumToString()
        {
            string newStringUserType = UserTypeConverter.EnumToString(_enumUserType);

            Assert.Equal(newStringUserType, _stringUserType);
        }



    }
}
