//manager.js
//mark.lawrence

(function () {
    var controllerId = 'TaxController';

    angular.module('app.tax').controller(controllerId, MainController);

    MainController.$inject = ['logger', '$uibModal', 'constituentService'];

    function MainController(logger, $modal, service) {
        var vm = this;
        var tableStateRef;

        vm.title = 'Tax Data Manager';
        vm.description = "Update Tax and Constituent Data";

        vm.people = [];
        vm.searchModel = {
            page: 1,
            pageSize: 15,
            orderBy: 'Name',
            orderDirection: 'desc'
        };

        vm.downloadCsv = downloadCsv;
        vm.isBusy = false;

        vm.updateStatusOptions = [
            { id: null, title: 'All' }, { id: 1, title: 'Unchanged' }, { id: 2, title: 'Changed' },
            { id: 6, title: 'Reconciled' }
        ];

        activate();

        function activate() {
            logger.log(controllerId + ' activated');
        }

        function downloadCsv() {
            return service.download(vm.data)
                .then(function (data) {
                    var dataFile = [];
                    _.forEach(data.items,
                        function (item) {
                            dataFile.push({
                                'RecordId': item.id,
                                'LookupID': item.lookupId,
                                'FinderNumber': item.finderNumber,
                                'Name': item.name,
                                'EmailAddress': item.email,
                                'AddressLine1': item.street,
                                'AddressLine2': item.street2,
                                'City': item.city,
                                'State': item.state,
                                'ZIP': item.zipcode
                            });
                        });
                    return dataFile;
                });
        }

        vm.editItem = function (item) {
            $modal.open({
                templateUrl: '/app/tax/views/constituent.html',
                controller: ['logger', '$uibModalInstance', 'constituentService', 'item', EditConstituentController],
                controllerAs: 'vm',
                resolve: {
                    item: function () { return item; }
                }
            }).result.then(function (data) {
                angular.extend(item, data);
                logger.success('Successfully updated ' + item.name);
            });
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
            service.query(vm.searchModel)
                .then(function (data) {
                    vm.people = data.items;
                    vm.searchModel = data;
                    vm.isBusy = false;
                });
        }

        vm.reconcileItem = function (item) {
            //TODO: Replace with CONSTANTS
            item.updateStatus = 6;
            service.update(item).then(function (response) {
                logger.success('Reconciled ' + item.name);
            });
        }

        vm.showTaxItems = function (constituent) {
            $modal.open({
                templateUrl: '/app/tax/views/taxitems.html',
                controller: ['logger', '$uibModalInstance', 'constituent', 'taxService', TaxItemController],
                controllerAs: 'vm',
                resolve: {
                    constituent: function () { return constituent; }
                }
            });
        }
    }

    function TaxItemController(logger, $modal, constituent, service) {
        var vm = this;
        var currentYear = parseInt(moment().get('Year'));

        vm.selectedYear = currentYear - 1;
        logger.log('selectedYear', vm.selectedYear);

        vm.dateOptions = {
            formatYear: 'yyyy',
            maxDate: new Date('12/30/' + vm.selectedYear),
            minDate: new Date('1/1/' + vm.selectedYear),
            startingDay: 1
        };
        vm.altInputFormats = ['M!/d!/yyyy'];

        vm.years = [];

        vm.minDate = new Date('1/1/' + vm.selectedYear);
        vm.maxDate = new Date('12/30/' + vm.selectedYear);

        vm.addItem = addItem;
        vm.cancelEdit = cancelEdit;
        vm.constituent = constituent;
        vm.deleteItem = deleteItem;
        vm.editItem = editItem;
        vm.itemToEdit = {};
        vm.newItem = {};
        vm.saveItem = saveItem;
        vm.taxItems = constituent.taxItems;
        vm.yearChanged = yearChanged;

        activate();

        function activate() {
            getYears();
        }

        function getYears() {
            for (var i = 0; i < 5; i++) {
                vm.years.push(currentYear - i);
            }
        }

        function addItem() {
            vm.newItem.constituentId = constituent.id;
            vm.newItem.taxYear = moment(vm.newItem.donationDate).year();

            service.create(vm.newItem)
                .then(function (data) {
                    vm.taxItems.push(data);
                    vm.newItem = {};
                    logger.success('Added tax item');
                });
        }

        function cancelEdit() {
            vm.currentEdit = {};
        }

        function deleteItem(item) {
            var idx = vm.taxItems.indexOf(item);
            service.remove(item.id)
                .then(function (data) {
                    vm.taxItems.splice(idx, 1);
                    logger.warning('deleted ' + item.donationDate);
                });
        }

        function editItem(item) {
            vm.currentEdit = {};
            vm.currentEdit[item.id] = true;
            vm.itemToEdit = angular.copy(item);
            vm.itemToEdit.donationDate = moment(vm.itemToEdit.donationDate).toDate();
        }

        function saveItem(item) {
            service.update(vm.itemToEdit)
                .then(function (response) {
                    angular.extend(item, vm.itemToEdit);
                    vm.currentEdit = {};
                    logger.success('Updated tax item ' + moment(item.donationDate).format('MM/dd/yyyy'));
                });
        }

        function yearChanged() {
            vm.dateOptions = {
                maxDate: new Date('12/30/' + vm.selectedYear),
                minDate: new Date('1/1/' + vm.selectedYear)
            };
            vm.newItem.donationDate = vm.dateOptions.minDate;
        }
    }

    function EditConstituentController(logger, $modal, service, item) {
        var vm = this;

        vm.item = angular.copy(item);

        vm.close = function () {
            $modal.dismiss();
        }

        vm.save = function () {
            //TODO: Replace with CONSTANTS
            vm.item.updateStatus = 2;
            service.update(vm.item)
                .then(function (data) {
                    //angular.extend(item, vm.item);
                    $modal.close(vm.item);
                });
        }
    }
})();