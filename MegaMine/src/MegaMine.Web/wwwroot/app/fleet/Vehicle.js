﻿'use strict';
angular.module('megamine').controller('vehicle', vehicle)
vehicle.$inject = ['$state', 'vehicleService', 'vehicleDialog', "MegaMine.Shared.Utility", "MegaMine.Shared.Profile", "MegaMine.Shared.Navigation", "MegaMine.Shared.Constants"];

function vehicle($state, vehicleService, vehicleDialog, utility, profile, navigation, constants) {

    var vm = {
        model: {},
        menuItems: [],
        menuSelect: menuSelect,
        viewVehicle: viewVehicle,
    };

    init();

    return vm;

    function init() {
        vm.model = vehicleService.vehicle;
        menuInitialize();
    }

    function viewVehicle(ev) {
        vehicleDialog.viewDialog(vm.model, constants.enum.dialogMode.save, ev);
    }

    function menuInitialize() {
        //setting menu item for the first time
        if (navigation.vehicleMenuItems.length === 0) {
            navigation.populateVehicleMenu(vm.model.vehicleId);
        }
        vm.menuItems = navigation.vehicleMenuItems;

        //setting the first menu item if no menu item is selected
        var menuSelected = false;
        angular.forEach(vm.menuItems, function (item) {
            if (item.cssClass === "highlight") {
                menuSelected = true;
            }
        });
        if (!menuSelected) {
            vm.menuItems[0].cssClass = "highlight";
        }
    }

    function menuSelect(menuItem) {
        angular.forEach(vm.menuItems, function (item) {
            item.cssClass = "";
        });
        menuItem.cssClass = "highlight";
    }

}
