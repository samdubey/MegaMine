﻿'use strict';

angular.module('emine').config(quarryRoute);
quarryRoute.$inject = ['$stateProvider'];

function quarryRoute($stateProvider) {

    $stateProvider
        .state("colour", {
            url: window.virtualDirectory + "/colour",
            title: "Product Colour",
            previousState: "dashboard",
            templateUrl: "/app/quarry/materialColour.html",
            controller: "materialColour",
            controllerAs: "vm",
            resolve: ['quarryService', function (quarryService) {
                return quarryService.getMaterialColours();
            }]
        })
        .state("producttype", {
            url: window.virtualDirectory + "/producttype",
            title: "Product Type",
            previousState: "dashboard",
            templateUrl: "/app/quarry/productType.html",
            controller: "productType",
            controllerAs: "vm",
            resolve: ['quarryService', function (quarryService) {
                return quarryService.getProductTypes();
            }]
        })
        .state("quarry", {
            url: window.virtualDirectory + "/quarry",
            title: "Quarry",
            previousState: "dashboard",
            templateUrl: "/app/quarry/quarry.html",
            controller: "quarry",
            controllerAs: "vm",
            resolve: { quarries: ['quarryService', function (quarryService) {
                    return quarryService.getQuarries();
                     }],
                colours: ['quarryService', function (quarryService) {
                    return quarryService.getMaterialColours();
            }]}
        })
        .state("yard", {
            url: window.virtualDirectory + "/yard",
            title: "Yard",
            previousState: "dashboard",
            templateUrl: "/app/quarry/yard.html",
            controller: "yard",
            controllerAs: "vm",
            resolve: ['quarryService', function (quarryService) {
                return quarryService.getYards();
            }]
        })
        .state("material", {
            url: window.virtualDirectory + "/material",
            title: "Add Material",
            previousState: "dashboard",
            templateUrl: "/app/quarry/material.html",
            controller: "material",
            controllerAs: "vm",
            resolve: ['quarryService', function (quarryService) {
                return quarryService.getMaterialViewModel();
            }]
        })

}
