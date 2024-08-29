using ProjectLABPSD.Handlers;
using ProjectLABPSD.Model;
using System.Collections.Generic;

namespace ProjectLABPSD.Controllers
{
    public class CartController
    {
        public static List<object> GetCartList(int userID)
        {
            return CartHandler.GetCartList(userID);
        }
        public static void AddToCart(string userID, string stationeryID, int quantity)
        {
            CartHandler.AddToCart(userID, stationeryID, quantity);
        }


        public static Cart GetCartItemByID(int userID, int StationeryID)
        {
            return CartHandler.GetCartItemByID(userID, StationeryID);
        }
        public static void UpdateCart(string userID, string stationeryID, int quantity)
        {
            CartHandler.UpdateCart(userID, stationeryID, quantity);
        }

        public static void DeleteFromCart(int userID, string stationeryID)
        {
            CartHandler.DeleteFromCart(userID, stationeryID);
        }

        public static void DeleteAllItem(int userID)
        {
            CartHandler.DeleteAllItem(userID);
        }
    }
}