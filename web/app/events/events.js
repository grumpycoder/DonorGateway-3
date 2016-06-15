//mark.lawrence
//events.js

(function () {
    'use strict';

    var controllerId = 'EventController';

    angular.module('app.events').controller(controllerId, mainController);

    mainController.$inject = ['logger', 'eventService'];

    function mainController(logger, service) {
        var vm = this;
        vm.title = 'Event Manager';
        vm.description = "Manage Donor Events";

        vm.events = [];

        vm.tabs = [
            { title: 'Details', template: 'app/events/views/home.html', active: true },
            { title: 'Guest List', template: 'app/events/views/guest-list.html', active: true },
            { title: 'Waiting List', template: 'app/events/views/wait-list.html', active: false },
            { title: 'Tickets', template: 'app/events/views/ticket-list.html', active: false }
        ];

        activate();

        function activate() {
            logger.log(controllerId + ' activated');
            getEvents();
        }

        vm.changeEvent = function () {
            logger.log('changed', vm.selectedEvent);
            service.getById(vm.selectedEvent.id)
                .then(function (data) {
                    logger.log('data', data);
                    angular.extend(vm.selectedEvent, data);
                });
        }

        function getEvents() {
            service.get()
                .then(function (data) {
                    vm.events = data;
                });
        }
    };
})();