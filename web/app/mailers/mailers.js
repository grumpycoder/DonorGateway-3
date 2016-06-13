//mark.lawrence
//mailers.js

(function () {
    var controllerId = 'MailerController';

    angular.module('app.mailers').controller(controllerId, MainController);

    MainController.$inject = ['logger', '$uibModal', 'campaignService', 'mailerService'];

    function MainController(logger, $modal, campaignService, mailerService) {
        var vm = this;
        var tableStateRef;

        vm.title = 'Mailers Manager';
        vm.description = "Update Mailer Acquisition Data";

        vm.campaigns = [];

        vm.mailers = [];
        vm.searchModel = {
            page: 1,
            pageSize: 10,
            orderBy: 'lastName',
            orderDirection: 'desc'
        };

        //vm.downloadCsv = downloadCsv;
        vm.isBusy = false;

        activate();

        function activate() {
            logger.log(controllerId + ' activated');
            getCampaigns();
        }

        vm.paged = function paged(pageNum) {
            vm.lastDeleted = null;
            vm.lastUpdated = null;
            vm.search(tableStateRef);
        };

        vm.search = function (tableState) {
            tableStateRef = tableState;

            if (!vm.searchModel.isPriority) vm.searchModel.isPriority = null;

            if (typeof (tableState.sort.predicate) != "undefined") {
                vm.searchModel.orderBy = tableState.sort.predicate;
                vm.searchModel.orderDirection = tableState.sort.reverse ? 'desc' : 'asc';
            }
            if (typeof (tableState.search.predicateObject) != "undefined") {
                vm.searchModel.name = tableState.search.predicateObject.name;
                vm.searchModel.lookupId = tableState.search.predicateObject.lookupId;
                vm.searchModel.finderNumber = tableState.search.predicateObject.finderNumber;
                vm.searchModel.zipcode = tableState.search.predicateObject.zipcode;
                vm.searchModel.email = tableState.search.predicateObject.email;
                vm.searchModel.phone = tableState.search.predicateObject.phone;
                vm.searchModel.updateStatus = tableState.search.predicateObject.updateStatus;
            }

            vm.isBusy = true;
            mailerService.query(vm.searchModel)
                .then(function (data) {
                    vm.mailers = data.items;
                    vm.searchModel = data;
                    vm.isBusy = false;
                });
        }

        function getCampaigns() {
            campaignService.get()
                .then(function (data) {
                    vm.campaigns = data;
                    logger.log('campaigns', vm.campaigns);
                });
        }
    }
})();