//mark.lawrence
//mailers.js

(function () {
    var controllerId = 'MailerController';

    angular.module('app.mailers').controller(controllerId, MainController);

    MainController.$inject = ['logger', '$uibModal', 'campaignService', 'reasonService', 'mailerService'];

    function MainController(logger, $modal, campaignService, reasonService, mailerService) {
        var vm = this;
        var tableStateRef;

        vm.title = 'Mailers Manager';
        vm.description = "Update Mailer Acquisition Data";

        vm.campaigns = [];
        vm.reasons = [];
        vm.currentEdit = {};
        vm.itemToEdit = {};

        vm.mailers = [];
        vm.searchModel = {
            page: 1,
            pageSize: 10,
            orderBy: 'campaignId',
            orderDirection: 'desc',
            suppress: false
        };
        vm.supress = false;
        //vm.downloadCsv = downloadCsv;
        vm.isBusy = false;

        activate();

        function activate() {
            logger.log(controllerId + ' activated');
            getCampaigns();
            getReasons();
        }

        vm.cancelEdit = function (id) {
            vm.currentEdit[id] = false;
        };

        vm.editItem = function (item) {
            vm.currentEdit[item.id] = true;
            angular.copy(item, vm.itemToEdit = {});
        }

        vm.paged = function paged(pageNum) {
            vm.lastDeleted = null;
            vm.lastUpdated = null;
            vm.search(tableStateRef);
        };

        vm.save = function (item) {
            vm.currentEdit[item.id] = false;
            vm.itemToEdit.suppress = true;
            mailerService.update(vm.itemToEdit)
                .then(function (data) {
                    angular.extend(item, data);
                    logger.success('Suppressed ' + item.finderNumber);
                });
        }

        vm.search = function (tableState) {
            tableStateRef = tableState;

            if (typeof (tableState.sort.predicate) != "undefined") {
                vm.searchModel.orderBy = tableState.sort.predicate;
                vm.searchModel.orderDirection = tableState.sort.reverse ? 'desc' : 'asc';
            }
            if (typeof (tableState.search.predicateObject) != "undefined") {
                vm.searchModel.firstName = tableState.search.predicateObject.firstName;
                vm.searchModel.lastName = tableState.search.predicateObject.lastName;
                vm.searchModel.address = tableState.search.predicateObject.address;
                vm.searchModel.city = tableState.search.predicateObject.city;
                vm.searchModel.state = tableState.search.predicateObject.state;
                vm.searchModel.zipcode = tableState.search.predicateObject.zipcode;
                vm.searchModel.finderNumber = tableState.search.predicateObject.finderNumber;
                vm.searchModel.sourceCode = tableState.search.predicateObject.sourceCode;
                vm.searchModel.campaignId = tableState.search.predicateObject.campaignId;
                vm.searchModel.reasonId = tableState.search.predicateObject.reasonId;
            }

            vm.isBusy = true;
            mailerService.query(vm.searchModel)
                .then(function (data) {
                    vm.mailers = data.items;
                    vm.searchModel = data;
                    vm.isBusy = false;
                });
        }

        vm.filterSuppress = function () {
            vm.search(tableStateRef);
        }

        function getCampaigns() {
            campaignService.get()
                .then(function (data) {
                    vm.campaigns = data;
                });
        }

        function getReasons() {
            reasonService.get()
                .then(function (data) {
                    vm.reasons = data;
                });
        }
    }
})();