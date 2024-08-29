using ProjectLABPSD.Model;
using ProjectLABPSD.Repository;
using ProjectLABPSD.Views.Repository;
using System.Collections.Generic;
using System.Linq;

namespace ProjectLABPSD.Handlers
{
    public class StationeryHandler
    {

        public static List<MsStationery> GetStationery()
        {
            return StationeryRepository.GetStationery();
        }

        public static MsStationery GetStationeryByID(int stationeryID)
        {
            return StationeryRepository.GetStationeryByID(stationeryID);
        }

        public static void CreateStationery(string itemName, int itemPrice)
        {
            StationeryRepository.InsertStationery(itemName, itemPrice);
        }

        public static void UpdateStationery(int stationeryID, string itemName, int itemPrice)
        {
            StationeryRepository.UpdateStationery(stationeryID, itemName, itemPrice);
        }
        public static void DeleteStationery(string id)
        {
            MsStationery stationery = StationeryRepository.GetStationeryByID(int.Parse(id));

            if(stationery.Carts.Count > 0)
            {
                CartRepository.RemoveCarts(stationery.Carts.ToList());
            }
            StationeryRepository.DeleteStationery(id);
        }

        public static int GetStationeryPrice(int stationeryID)
        {
            return StationeryRepository.GetStationeryPrice(stationeryID);
        }
    }
}