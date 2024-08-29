﻿using ProjectLABPSD.Model;

namespace ProjectLABPSD.Factories
{
    public class StationeryFactories
    {

        public static MsStationery CreateStationery(string stationeryName, int stationeryPrice)
        {
            MsStationery newStationery = new MsStationery();
            newStationery.StationeryPrice = stationeryPrice;
            newStationery.StationeryName = stationeryName;

            return newStationery;
        }
    }
}

