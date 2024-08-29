﻿using ProjectLABPSD.Handlers;
using ProjectLABPSD.Model;
using System;
using System.Linq;

namespace ProjectLABPSD.Controllers
{
    public class RegisterController
    {
        public static string CheckRegister(string userName, string userDOB, string userGender, string userAddress, string userPassword, string userPhone)
        {
            string response = "";

            if (string.IsNullOrEmpty(userName) || userName.Length < 5 || userName.Length > 50)
            {
                return "Name must be between 5 and 50 characters.";
            }

            DateTime dobDate;
            if (!DateTime.TryParse(userDOB, out dobDate) || (DateTime.Now.Year - dobDate.Year) < 1)
            {
                return "Please input a valid date and at least 1 year old.";
            }

            if (string.IsNullOrEmpty(userGender))
            {
                return "Please select a gender.";
            }

            if (string.IsNullOrEmpty(userAddress))
            {
                return "Address must be filled.";
            }

            if (string.IsNullOrEmpty(userPassword))
            {
                return "Password must be filled.";
            }

            if (!userPassword.All(char.IsLetterOrDigit))
            {
                return "Password must be alphanumeric.";
            }

            if (string.IsNullOrEmpty(userPhone))
            {
                return "Phone must be filled.";
            }

            MsUser user = UserHandler.CheckUser(userName);

            if (user != null)
            {
                return "Username already exists";
            }
            else
            {
                UserHandler.DoRegisterUser(userName, userPassword, userGender, userPhone, userAddress, "Customer", dobDate);
            }

            return response;
        }

    }
}