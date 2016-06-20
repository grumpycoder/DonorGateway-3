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
            vm.currentDate = new Date();
            service.getById(vm.selectedEvent.id)
                .then(function (data) {
                    angular.extend(vm.selectedEvent, data);
                    vm.selectedEvent.startDate = moment(vm.selectedEvent.startDate).toDate();
                    vm.selectedEvent.endDate = moment(vm.selectedEvent.endDate).toDate();
                    vm.selectedEvent.venueOpenDate = moment(vm.selectedEvent.venueOpenDate).toDate();
                    vm.selectedEvent.registrationCloseDate = moment(vm.selectedEvent.registrationCloseDate).toDate();

                    vm.guests = [].concat(vm.selectedEvent.guests);
                    logger.log('Selected Event', vm.selectedEvent);
                });
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

            //vm.newEvent.template = {
            //    name: vm.newEvent.name
            //};
            //service.create(vm.newEvent)
            //    .then(function (data) {
            //        vm.selectedEvent = data;
            //        vm.events.push(vm.selectedEvent);
            //        logger.success('Created new event: ' + data.name);
            //    }).finally(function () {
            //        vm.newEvent = {};
            //        complete();
            //    });
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
            vm.selectedEvent.startDate = moment.utc(vm.selectedEvent.startDate);
            vm.selectedEvent.endDate = moment.utc(vm.selectedEvent.endDate);
            vm.selectedEvent.venueOpenDate = moment.utc(vm.selectedEvent.venueOpenDate);
            vm.selectedEvent.registrationCloseDate = moment.utc(vm.selectedEvent.registrationCloseDate);

            return service.update(vm.selectedEvent)
                .then(function (data) {
                    logger.success('Saved Event: ' + data.name);
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