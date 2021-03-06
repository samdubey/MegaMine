﻿//-------------------------------------------------------------------------------------------------
// <copyright file="FleetMappingProfile.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Automapper mapping for Fleet Entities and DTOs
// </description>
//-------------------------------------------------------------------------------------------------
namespace MegaMine.Modules.Fleet.Mapping
{
    using AutoMapper;
    using MegaMine.Modules.Fleet.Entities;
    using MegaMine.Modules.Fleet.Models;

    public class FleetMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "FleetMappingProfile"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<VehicleTypeEntity, VehicleTypeModel>().ReverseMap();
            Mapper.CreateMap<VehicleTripEntity, VehicleTripModel>().ReverseMap();
            Mapper.CreateMap<VehicleDriverEntity, VehicleDriverModel>().ReverseMap();
            Mapper.CreateMap<VehicleModelEntity, VehicleManufacturerModelModel>().ReverseMap();
            Mapper.CreateMap<VehicleFuelEntity, FuelModel>().ReverseMap();
            Mapper.CreateMap<VehicleDriverAssignmentEntity, VehicleDriverAssignmentModel>().ReverseMap();
            Mapper.CreateMap<VehicleManufacturerEntity, VehicleManufacturerModel>().ReverseMap();
            Mapper.CreateMap<VehicleManufacturerEntity, ManufacturerDetailsModel>().ReverseMap();
            Mapper.CreateMap<VehicleEntity, VehicleModel>().ReverseMap();
            Mapper.CreateMap<VehicleServiceEntity, VehicleServiceModel>().ReverseMap();
        }
    }
}
