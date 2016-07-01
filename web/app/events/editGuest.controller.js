//mark.lawrence
//editGuest.controller.js

(function () {
    'use strict';

    var controllerId = 'EditGuestController';

    angular.module('app.events').controller(controllerId, mainController);

    mainController.$inject = ['logger', '$uibModalInstance', 'guestService', 'templateService', 'guest'];

    function mainController(logger, $modal, service, templateService, guest) {
        var vm = this;
        vm.title = 'Edit Guest';

        vm.guest = guest;

        logger.log('guest', guest);

        vm.cancel = function () {
            $modal.dismiss();
        }

        vm.save = function () {
            service.update(guest)
                .then(function (data) {
                    vm.guest = data;
                    $modal.close(data);
                }).finally(function () {
                });
        }
    }


})();