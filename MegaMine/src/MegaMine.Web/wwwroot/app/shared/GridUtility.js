var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var MegaMine;
(function (MegaMine) {
    var Shared;
    (function (Shared) {
        let GridUtility = class GridUtility {
            constructor(uiGridConstants) {
                this.uiGridConstants = uiGridConstants;
            }
            initializeGrid(gridOptions, model) {
                this.initialize(gridOptions, model, "main-content", "main-grid", 24);
            }
            initializeSubGrid(gridOptions, model) {
                this.initialize(gridOptions, model, "main-content", "sub-grid", 41);
            }
            initializeDialogGrid(gridOptions, model) {
                this.initialize(gridOptions, model, "dialog", "dialog-grid", 100);
            }
            initialize(gridOptions, model, contentClass, gridClass, bottomOffset) {
                const self = this;
                gridOptions.enableColumnResizing = true;
                gridOptions.enableHorizontalScrollbar = self.uiGridConstants.scrollbars.NEVER;
                gridOptions.data = model;
                // resizeGrid(gridOptions, contentClass, gridClass, bottomOffset);
                // setting the grid API
                gridOptions.onRegisterApi = function (gridApi) {
                    gridOptions.gridApi = gridApi;
                };
            }
        };
        GridUtility = __decorate([
            MegaMine.service("megamine", "MegaMine.Shared.GridUtility"),
            MegaMine.inject("uiGridConstants")
        ], GridUtility);
        Shared.GridUtility = GridUtility;
    })(Shared = MegaMine.Shared || (MegaMine.Shared = {}));
})(MegaMine || (MegaMine = {}));
//# sourceMappingURL=GridUtility.js.map