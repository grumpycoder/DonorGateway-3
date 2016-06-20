//mark.lawrence
//events.js

(function () {
    'use strict';

    var controllerId = 'EventController';

    angular.module('app.events').controller(controllerId, mainController);

    mainController.$inject = ['logger', 'eventService', 'guestService'];

    function mainController(logger, service, guestService) {
        var vm = this;
        vm.title = 'Event Manager';
        vm.description = "Manage Donor Events";

        vm.events = [];
        vm.isBusy = true;
        vm.tabs = [
            { title: 'Details', template: 'app/events/views/home.html', active: true },
            { title: 'Guest List', template: 'app/events/views/guest-list.html', active: true },
            { title: 'Waiting List', template: 'app/events/views/wait-list.html', active: false },
            { title: 'Ticket Queue', template: 'app/events/views/ticket-list.html', active: false },
            { title: 'Template', template: 'app/events/views/ticket-list.html', active: false }
        ];

        activate();

        function activate() {
            logger.log(controllerId + ' activated');
            getEvents();
        }

        vm.addToTicketQueue = function (guest) {
            guest.isWaiting = false;
            guest.isAttending = true;
            guestService.update(guest)
                .then(function (data) {
                    logger.log('added guest', data);
                });
        }

        vm.changeEvent = function () {
            if (!vm.selectedEvent) return;
            vm.currentDate = new Date();
            service.getById(vm.selectedEvent.id)
                .then(function (data) {
                    angular.extend(vm.selectedEvent, data);
                    logger.log('utc', moment.utc(vm.selectedEvent.startDate));
                    vm.selectedEvent.startDate = moment(vm.selectedEvent.startDate).toDate();
                    vm.selectedEvent.endDate = moment(vm.selectedEvent.endDate).toDate();
                    vm.selectedEvent.venueOpenDate = moment(vm.selectedEvent.venueOpenDate).toDate();
                    vm.selectedEvent.registrationCloseDate = moment(vm.selectedEvent.registrationCloseDate).toDate();

                    vm.guests = [].concat(vm.selectedEvent.guests);
                });
        }

        vm.createEvent = function () {
            logger.log('create event', vm.newEvent);
            service.create(vm.newEvent)
                .then(function (data) {
                    vm.selectedEvent = data;
                    vm.events.push(vm.selectedEvent);
                    logger.success('Created new event: ' + data.name);
                }).finally(function () {
                    vm.newEvent = {};
                });
        }

        vm.delete = function (id) {
            service.remove(id)
                .then(function (data) {
                    vm.selectedEvent = null;
                });
        }

        vm.issueTicket = function (guest) {
            guest.ticketIssued = true;

            guestService.update(guest)
                .then(function (data) {
                    angular.extend(guest, data);
                });
        }

        vm.save = function () {
            vm.isBusy = true;
            vm.selectedEvent.startDate = moment.utc(vm.selectedEvent.startDate);
            vm.selectedEvent.endDate = moment.utc(vm.selectedEvent.endDate);
            vm.selectedEvent.venueOpenDate = moment.utc(vm.selectedEvent.venueOpenDate);
            vm.selectedEvent.registrationCloseDate = moment.utc(vm.selectedEvent.registrationCloseDate);

            service.update(vm.selectedEvent)
                .then(function (data) {
                    //angular.extend(vm.selectedEvent, data);
                    logger.success('Saved Event: ' + data.name);
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }

        vm.toggleCancel = function () {
            vm.selectedEvent.isCancelled = !vm.selectedEvent.isCancelled;
            vm.save();
        }

        function getEvents() {
            service.get()
                .then(function (data) {
                    vm.events = data;
                    //TODO: Remove this. Debugging only
                    vm.selectedEvent = vm.events[0];
                    vm.changeEvent();
                }).finally(function () {
                    vm.isBusy = false;
                });
        }
    };
})();