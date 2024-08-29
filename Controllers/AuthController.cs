using ProjectLABPSD.Handlers;
using ProjectLABPSD.Model;
using System;
using System.Linq;
using System.Web;

namespace ProjectLABPSD.Controllers
{
    public class AuthController
    {
        public static string CheckNameAndPassword(string userName, string userPassword)
        {
            string response = "";
            if (string.IsNullOrEmpty(userName))
            {
                response = "Name must be filled";
                return response;
            }

            if (string.IsNullOrEmpty(userPassword))
            {
                response = "Password must be filled";
                return response;
            }
            return response;
        }

        public static Boolean Login(string userName, string userPassword, bool isChecked)
        {
            MsUser user = UserHandler.Login(userName, userPassword);
            if (user != null)
            {
                if (isChecked)
                {
                    HttpCookie cookie = new HttpCookie("user_cookie");
                    cookie["userID"] = user.userID.ToString();
                    cookie["userName"] = user.userName;
                    cookie["userRole"] = user.userRole;
                    cookie.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
                HttpContext.Current.Session.Add("user", user.userID);
                HttpContext.Current.Session.Add("userName", user.userName);
                HttpContext.Current.Session.Add("userRole", user.userRole);
                return true;
            }
            return false;
        }

        public static bool UserIsAdmin()
        {
            HttpCookie isAdmin = HttpContext.Current.Request.Cookies["user_cookie"];
            if (isAdmin != null && isAdmin["userRole"] == "Admin")
            {
                return true;
            }
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["userRole"] != null)
            {
                if (HttpContext.Current.Session["userRole"].ToString() == "Admin")
                {
                    return true;
                }
            }
            return false;
        }

        public static bool UserIsLoggedIn()
        {
            return HttpContext.Current.Session["user"] != null || HttpContext.Current.Request.Cookies["user_cookie"] != null;
        }
        public static bool UserIsCustomer()
        {
            HttpCookie isCustomer = HttpContext.Current.Request.Cookies["user_cookie"];
            if (isCustomer != null && isCustomer["userRole"] == "Customer")
            {
                return true;
            }

            if (HttpContext.Current.Session != null && HttpContext.Current.Session["userRole"] != null)
            {
                if (HttpContext.Current.Session["userRole"].ToString() == "Customer")
                {
                    return true;
                }
            }
            return false;
        }

        public static string GetUserIDbyCookieOrSession()
        {
            HttpCookie temp = HttpContext.Current.Request.Cookies["user_cookie"];
            string userID;

            if (temp == null)
            {
                userID = HttpContext.Current.Session["user"].ToString();
            }
            else
            {
                userID = temp["userID"];
            }

            return userID;
        }

        public static string CheckProfile(int userID, string userName, string userDOB, string userGender, string userAddress, string userPassword, string userPhone)
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

            if (user != null && user.userID != userID)
            {
                return "Username already exists";
            }
            else
            {
                HttpCookie usercookie = HttpContext.Current.Request.Cookies["user_cookie"];
                if (usercookie != null) usercookie["userName"] = userName;

                HttpContext.Current.Session["userName"] = userName;
                UserHandler.DoUpdateUser(userID, userName, userPassword, userGender, userPhone, userAddress, dobDate);
            }

            return response;
        }
    }
}