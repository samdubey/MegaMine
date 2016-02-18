﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMine.Lib.Shared
{
    public static class Messages
    {
        #region Fleet Messages
        public static class Fleet
        {
            //master tables
            public const string VehicleTypeSaveSuccess = "Vehicle Type Saved Successfully";
            public const string VehicleManufacturerSaveSuccess = "Manufacturer Saved Successfully";
            public const string DriverSaveSuccess = "Driver Saved Successfully";

            //vehicle Messages
            public const string VehicleServiceSaveSuccess = "Vehicle Service Saved Successfully";
            public const string VehicleSaveSuccess = "Vehicle Saved Successfully";
            public const string VehicleModelSaveSuccess = "Vehicle Model Saved Successfully";
            public const string DriveAssessmentError = "Driver is already assigned. Cannot assign another driver";
            public const string FuelSaveSuccess = "Fuel Saved Successfully";
            public const string FuelInvalidOdometer = "Invalid Odometer or Date. There is already a higher Odometer reading";
            public const string FuelResetSuccess = "Reset Fuel Average Successful";
            public const string VehicleDriverSaveSuccess = "Driver record Saved Successfully";
            public const string VehicleTripSaveSuccess = "Vehicle trip record saved Successfully";
            public const string SparePartOrderSaveSuccess = "Spare Part Order Saved Successfully";
            public const string SparePartSaveSuccess = "Spare Part Saved Successfully";
            public const string DriveAssessmentDateError = "Driver assignment start date should not be greater than the end date.";

        }
        #endregion

        #region Quarry Messages
        public static class Quarry
        {
            public const string MaterialColourSaveSuccess = "Colour Saved Successfully";
            public const string MaterialColourDeleteSuccess = "Colour Deleted Successfully";
            public const string ProductTypeSaveSuccess = "Product Type Saved Successfully";
            public const string ProductTypeDeleteSuccess = "Product Type Deleted Successfully";
            public const string QuarrySaveSuccess = "Quarry Saved Successfully";
            public const string QuarryDeleteSuccess = "Quarry Deleted Successfully";
            public const string YardSaveSuccess = "Yard Saved Successfully";
            public const string YardDeleteSuccess = "Yard Deleted Successfully";
            public const string MaterialSaveSuccess = "Material Successfully Added";
            public const string MaterialDeleteSuccess = "Material Deleted Successfully";
            public const string MaterialUpdateSuccess = "Material Saved Successfully";
            public const string MaterialMovementSuccess = "Material Movement Successfully";

        }
        #endregion

        public static class Account
        {
            public const string LogoutSuccess = "Successfully logged out";
            public const string InvalidUsernamePassword = "Invalid Username and/or Password";
            public const string ChangePasswordSuccess = "Password Changed Successfully";
            public const string ChangePasswordError = "Unable to change password";
        }
    }
}