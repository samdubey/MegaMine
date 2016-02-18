﻿'use strict';
angular.module('megamine').controller('material', material)
material.$inject = ['$scope', '$mdDialog', 'quarryService', 'gridUtility', 'utility', 'quarryUtility', 'constants', 'template'];

function material($scope, $mdDialog, quarryService, gridUtility, utility, quarryUtility, constants, template) {

    var gridOptions = {
        columnDefs: [
                    { name: 'quarry', field: 'quarry', type: 'string', displayName: 'Quarry' },
                    { name: 'materialDate', field: 'materialDate', displayName: 'Date', type: 'date', cellFilter: 'date:"' + constants.dateFormat + '"' },
                    { name: 'colour', field: 'materialColour', type: 'string', displayName: 'Colour' },
                    { name: 'blockNumber', field: 'blockNumber', type: 'string', displayName: 'Block Number' },
                    { name: 'length', field: 'length', type: 'number', displayName: 'Length' },
                    { name: 'width', field: 'width', type: 'number', displayName: 'Width' },
                    { name: 'height', field: 'height', type: 'number', displayName: 'Height' },
                    { name: 'weight', field: 'weight', type: 'number', displayName: 'Weight' },
                    { name: 'productType', field: 'productType', displayName: 'Product Type', type: 'string' },
                    template.getButtonColumnDefs('materialId', [{ buttonType: constants.enum.buttonType.edit, ngClick: 'grid.appScope.vm.editRowMaterial(row.entity, $event)' }, { buttonType: constants.enum.buttonType.delete, ngClick: 'grid.appScope.vm.deleteRowMaterial(row.entity, $event)' }])
        ]
    };


    var vm = {
        list: [],
        model: {},
        previousModel: {},
        viewModel: {},
        gridOptions: gridOptions,
        viewDialog: {},
        addMaterial: addMaterial,
        saveMaterial: saveMaterial,
        editRowMaterial: editRowMaterial,
        deleteRowMaterial: deleteRowMaterial,
        cancelMaterial: cancelMaterial,
        updateMaterial: updateMaterial,
        editMode: false
    };

    init();

    return vm;

    function init() {
        vm.viewModel = quarryService.materialViewModel;
        vm.model = vm.viewModel.model;
        vm.model.materialDate = new Date();

        gridUtility.initializeGrid(vm.gridOptions, $scope, vm.list);

        quarryUtility.addMaterialWatchers($scope, vm.model); 
    }

    function updateDropDownText() {
        vm.model.productType = utility.getItem(vm.viewModel.productType, vm.model.productTypeId, 'productTypeId', 'productTypeName');
        vm.model.materialColour = utility.getListItem(vm.viewModel.materialColour, vm.model.materialColourId);
        vm.model.quarry = utility.getListItem(vm.viewModel.quarry, vm.model.quarryId);
    }

    function resetModel() {
        vm.model.blockNumber = "";
        vm.model.length = "";
        vm.model.width = "";
        vm.model.height = "";
        vm.model.weight = "";
    }

    function addMaterial(form) {
        if (form.$valid) {
            updateDropDownText();
            vm.model.index = vm.list.length;
            vm.list.push(angular.copy(vm.model));
            resetModel();
        }
    }

    function saveMaterial(ev) {
        if (vm.list.length === 0) {
            $mdDialog.show(
              $mdDialog.alert()
                .parent(angular.element(document.body))
                .title('No Materials')
                .content('Please add materials to save')
                .ariaLabel('No Materials')
                .ok('Ok')
                .targetEvent(ev)
            );
        }
        else {
            var confirm = $mdDialog.confirm()
              .parent(angular.element(document.body))
              .title('Confirm Save')
              .content('Please confirm to save the material')
              .ariaLabel('Save Material')
              .ok('Yes')
              .cancel('No')
              .targetEvent(ev);
            $mdDialog.show(confirm).then(function () {
                quarryService.saveMaterial(vm.list)
                    .then(function (data) {
                            vm.list.splice(0, vm.list.length);
                            resetModel();
                        });
                    });
        }
    }

    function editRowMaterial(row, ev) {
        vm.previousModel = angular.copy(vm.model);
        angular.extend(vm.model, row);
        //bypassing watchers that calculate product type and weight
        vm.model.bypassWeightWatcher = true;
        vm.model.bypassProductTypeWatcher = true;
        vm.editMode = true;
    }

    function deleteRowMaterial(row, ev) {
        var message = "Are you sure you want to delete the material";
        var confirm = $mdDialog.confirm()
          .parent(angular.element(document.body))
          .title('Delete Material')
          .content(message)
          .ariaLabel('Delete Material')
          .ok('Yes')
          .cancel('No')
          .targetEvent(ev);
        $mdDialog.show(confirm).then(function () {
            vm.list.splice(row.index, 1);
        });
    }

    function cancelMaterial(form) {
        angular.extend(vm.model, vm.previousModel);
        vm.editMode = false;
    }

    function updateMaterial(form) {
        if (form.$valid) {
            updateDropDownText();
            angular.extend(vm.list[vm.model.index], vm.model);
            resetModel();
            vm.editMode = false;
        }
    }
}
