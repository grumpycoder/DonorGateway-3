//mark.lawrence
//events.js

(function () {
    'use strict';

    var controllerId = 'EventController';

    angular.module('app.events').controller(controllerId, mainController);

    mainController.$inject = ['logger', '$uibModal', 'eventService', 'guestService', 'templateService'];

    function mainController(logger, $modal, service, guestService, templateService) {
        var vm = this;
        vm.title = 'Event Manager';
        vm.description = "Manage Donor Events";

        vm.events = [];
        vm.tabs = [
            { title: 'Details', template: 'app/events/views/home.html', active: true },
            { title: 'Guest List', template: 'app/events/views/guest-list.html', active: true },
            { title: 'Waiting List', template: 'app/events/views/wait-list.html', active: false },
            { title: 'Ticket Queue', template: 'app/events/views/ticket-list.html', active: false },
            { title: 'Template', template: 'app/events/views/template.html', active: false }
        ];

        vm.dateFormat = "MM/DD/YYYY hh:mm";

        activate();

        function activate() {
            logger.log(controllerId + ' activated');
            getEvents();
        }

        vm.addToTicketQueue = function (guest) {
            vm.isBusy = true;
            guest.isWaiting = false;
            guest.isAttending = true;
            guestService.update(guest)
                .then(function (data) {
                    logger.log('added guest', data);
                    logger.info('Added Guest to Queue: ' + data.name);
                }).finally(function () {
                    complete();
                });
        }

        vm.changeEvent = function () {
            if (!vm.selectedEvent) return;
            service.getById(vm.selectedEvent.id)
                .then(function (data) {
                    angular.extend(vm.selectedEvent, data);
                    vm.guests = [].concat(vm.selectedEvent.guests);
                    logger.log('Selected Event', vm.selectedEvent);
                });
        }

        vm.startDateChange = function () {
            logger.log('startDate', vm.selectedEvent.startDate);
        }

        vm.onTimeSet = function (newDate, startDate) {
            //newDate = moment(newDate).format();
            //newDate = moment.utc(newDate).format('l LT');
            logger.log('newDate', newDate);
            startDate = newDate;
            logger.log('startDate', startDate);
        }

        vm.showCreateEvent = function () {
            $modal.open({
                templateUrl: '/app/events/views/create-event.html',
                controller: ['logger', '$uibModalInstance', 'eventService', CreateEventController],
                controllerAs: 'vm'
            }).result.then(function (data) {
                vm.selectedEvent = data;
                vm.events.unshift(vm.selectedEvent);
                logger.success('Successfully created ' + data.name);
            });
        }

        vm.deleteEvent = function (id) {
            //TODO: Confirmation on delete
            vm.isBusy = true;
            service.remove(id)
                .then(function (data) {
                    vm.selectedEvent = null;
                    logger.success('Deleted event');
                }).finally(function () {
                    complete();
                });
        }

        vm.issueTicket = function (guest) {
            vm.isBusy = true;
            guest.ticketIssued = true;

            guestService.update(guest)
                .then(function (data) {
                    angular.extend(guest, data);
                    logger.success('Issued ticket to: ' + guest.name);
                }).finally(function () {
                    complete();
                });
        }

        vm.save = function () {
            vm.isBusy = true;
            //vm.selectedEvent.startDate = moment.toISOString(vm.selectedEvent.startDate.toString());
            //logger.log('before save', vm.selectedEvent);
            //vm.selectedEvent.startDate = moment.utc(vm.selectedEvent.startDate).format();
            //logger.log('after conversion', vm.selectedEvent);

            //debugger;
            return service.update(vm.selectedEvent)
                .then(function (data) {
                    logger.success('Saved Event: ' + data.name);
                    //angular.extend(vm.selectedEvent, data);

                    //vm.selectedEvent.startDate = moment.utc(vm.selectedEvent.startDate).format('l LT');
                    logger.log('after save', vm.selectedEvent);
                })
                .finally(function () {
                    complete();
                });
        }

        vm.fileSelected = function ($files, $file, $event, $rejectedFiles) {
            var reader = new FileReader();
            reader.onload = function (e) {
                var dataURL = reader.result;
                vm.selectedEvent.template.image = dataURL.split(',')[1];
                vm.selectedEvent.template.mimeType = $file.type;
            };
            reader.readAsDataURL($file);
        };

        vm.saveTemplate = function () {
            vm.isBusy = true;
            templateService.update(vm.selectedEvent.template)
                .then(function (data) {
                    vm.selectedEvent.template = angular.extend(vm.selectedEvent.template, data);
                    logger.success('Saved template: ' + data.name);
                }).finally(function () {
                    complete();
                });
        }

        vm.toggleCancel = function () {
            vm.selectedEvent.isCancelled = !vm.selectedEvent.isCancelled;
            vm.save().then(function () {
                if (vm.selectedEvent.isCancelled) {
                    logger.warning('Cancelled event');
                } else {
                    logger.info('Restored event');
                }
            });
        }

        function getEvents() {
            service.get()
                .then(function (data) {
                    vm.events = data;
                    //TODO: Remove this. Debugging only
                    vm.selectedEvent = vm.events[0];
                    vm.changeEvent();
                }).finally(function () {
                    complete();
                });
        }

        function complete() {
            vm.isBusy = false;
        }
    };

    function CreateEventController(logger, $modal, service) {
        var vm = this;

        vm.event = {};

        vm.cancel = function () {
            $modal.dismiss();
        }

        vm.save = function () {
            service.create(vm.event)
                .then(function (data) {
                    $modal.close(data);
                });
        }
    }
})();