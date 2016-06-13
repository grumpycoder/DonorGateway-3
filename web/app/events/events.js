//mark.lawrence
//events.js

(function () {
    'use strict';

    var controllerId = 'EventController';

    angular.module('app.events').controller(controllerId, mainController);

    mainController.$inject = ['logger'];

    function mainController(logger) {
        var vm = this;
        vm.title = 'Event Manager';
        vm.description = "Manage Donor Events";

        activate();

        function activate() {
            logger.log(controllerId + ' activated');
            vm.user = JSON.parse(localStorage.getItem('currentUser'));
        }
    };
})();