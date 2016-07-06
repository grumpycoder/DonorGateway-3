//mark.lawrence
//editGuest.controller.js

(function () {
    'use strict';

    var controllerId = 'EditGuestController';

    angular.module('app.events').controller(controllerId, mainController);

    mainController.$inject = ['logger', '$uibModalInstance', 'guestService', 'templateService', 'guest', 'event'];

    function mainController(logger, $modal, service, templateService, guest, event) {
        var vm = this;
        vm.title = 'Edit Guest';
        vm.event = event;

        vm.guest = angular.copy(guest);

        logger.log('guest', guest);
        logger.log('event', vm.event);

        vm.cancel = function () {
            $modal.dismiss();
        }

        vm.save = function () {
            service.update(vm.guest)
                .then(function (data) {
                    angular.extend(vm.guest, data);
                    logger.log('guest', vm.guest);
                    $modal.close(vm.guest);
                }).finally(function () {
                });
        }
    }


})();