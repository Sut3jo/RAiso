﻿using ProjectLABPSD.Model;
using System;

namespace ProjectLABPSD.Factories
{
    public class UserFactories
    {
        public static MsUser CreateUser(string userName, string userPassword, string userGender, string userPhone, string userAddress, string userRole, DateTime DOB)
        {
            MsUser user = new MsUser();

            user.userName = userName;
            user.userPassword = userPassword;
            user.userGender = userGender;
            user.userDOB = DOB;
            user.userPhone = userPhone;
            user.userAddress = userAddress;
            user.userRole = userRole;

            return user;
        }
    }
}