﻿//mark.lawrence
//events.js

(function () {
    'use strict';

    var controllerId = 'EventController';

    angular.module('app.events').controller(controllerId, mainController);

    mainController.$inject = ['logger', '$uibModal', 'eventService', 'guestService', 'templateService'];

    function mainController(logger, $modal, service, guestService, templateService) {
        var vm = this;
        var tableStateRef;
        var pageSizeDefault = 10;

        vm.showWaitList = false;
        vm.showMailQueue = false;

        vm.title = 'Event Manager';
        vm.description = "Manage Donor Events";
        vm.currentDate = new Date();

        vm.dateOptions = {
            dateDisabled: false,
            formatYear: 'yy',
            maxDate: new Date(2020, 5, 22),
            minDate: new Date(),
            startingDay: 1
        };

        vm.dateFormat = "MM/DD/YYYY h:mm a";
        vm.events = [];

        vm.searchModel = {
            page: 1,
            pageSize: pageSizeDefault,
            orderBy: 'id',
            orderDirection: 'asc'
        };

        vm.tabs = [
            { title: 'Details', template: 'app/events/views/home.html', active: false, icon: 'fa-info-circle' },
            { title: 'Guests', template: 'app/events/views/guest-list.html', active: true, icon: 'fa-users' },
            //{ title: 'Mail Queue', template: 'app/events/views/mail-queue.html', active: false, icon: '' },
            //{ title: 'Waiting Queue', template: 'app/events/views/wait-queue.html', active: false, icon: '' },
            { title: 'Template', template: 'app/events/views/template.html', active: false, icon: 'fa-file-text' }
        ];

        activate();

        function activate() {
            logger.log(controllerId + ' activated');
            getEvents().then(function () {
                logger.log('loaded events')
            });
        }

        vm.addToMailQueue = function (guest) {
            vm.isBusy = true;
            guest.isWaiting = false;
            guest.isAttending = true;
            guestService.update(guest)
                .then(function (data) {
                    logger.log('added guest', data);
                    logger.info('Added Guest to Queue: ' + data.name);
                    guest = data;
                }).finally(function () {
                    //complete();
                    vm.isBusy = false;
                });
        }

        vm.changeEvent = function () {

            if (!vm.selectedEvent) return;
            //RESET VALUES
            vm.searchModel = {
                page: 1,
                pageSize: pageSizeDefault,
                orderBy: 'id',
                orderDirection: 'asc'
            };
            vm.showWaitList = false;
            vm.showMailQueue = false;
            vm.tabs[0].active = true;

            vm.isBusy = true;
            service.getById(vm.selectedEvent.id)
                .then(function (data) {
                    angular.extend(vm.selectedEvent, data);
                    vm.selectedEvent.isExpired = moment(vm.selectedEvent.endDate).toDate() < vm.currentDate;
                    //vm.guests = vm.selectedEvent.guests;
                    logger.log('event', vm.selectedEvent);
                }).finally(function () {
                    vm.isBusy = false;
                    vm.searchGuests(tableStateRef);
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
                    vm.isBusy = false;
                    getEvents();
                });
        }

        vm.registerGuest = function (guest) {

            $modal.open({
                templateUrl: '/app/events/views/edit-guest.html',
                controller: 'EditGuestController',
                controllerAs: 'vm',
                resolve: {
                    guest: guest,
                    event: vm.selectedEvent
                }
            }).result.then(function (result) {
                angular.extend(guest, result);

                if (vm.selectedEvent.ticketRemainingCount - guest.guestCount >= 0) {
                    vm.selectedEvent.ticketRemainingCount = vm.selectedEvent.ticketRemainingCount - guest.guestCount;
                    vm.selectedEvent.ticketMailedQueueCount += 1;
                }
                vm.selectedEvent.registeredGuestCount += guest.guestCount;
                if (guest.isWaiting) vm.selectedEvent.waitingGuestCount += guest.guestCount;
            });
        }

        vm.filterWaitingQueue = function () {
            vm.showWaitQueue = !vm.showWaitQueue;
            vm.showMailQueue = null;
            vm.searchModel.page = 1;
            vm.searchGuests(tableStateRef);
        }

        vm.filterMailQueue = function () {
            vm.showMailQueue = !vm.showMailQueue;
            vm.showWaitQueue = null;
            vm.searchModel.page = 1;
            vm.searchGuests(tableStateRef);
        }

        vm.mailTicket = function (guest) {
            vm.isBusy = true;
            guest.isMailed = true;
            guest.mailedDate = new Date();
            vm.isWaiting = false;
            guestService.update(guest)
                .then(function (data) {
                    angular.extend(guest, data);
                    logger.success('Issued ticket to: ' + guest.name);
                }).finally(function () {
                    vm.isBusy = false;
                });
        }

        vm.searchGuests = function (tableState) {
            tableStateRef = tableState;
            if (!vm.selectedEvent) return false;

            if (typeof (tableState.sort.predicate) != "undefined") {
                vm.searchModel.orderBy = tableState.sort.predicate;
                vm.searchModel.orderDirection = tableState.sort.reverse ? 'desc' : 'asc';
            }
            if (typeof (tableState.search.predicateObject) != "undefined") {
                vm.searchModel.name = tableState.search.predicateObject.name;
                vm.searchModel.address = tableState.search.predicateObject.address;
                vm.searchModel.city = tableState.search.predicateObject.city;
                vm.searchModel.state = tableState.search.predicateObject.state;
                vm.searchModel.zipcode = tableState.search.predicateObject.zipcode;
                vm.searchModel.guestCount = tableState.search.predicateObject.guestCount;
                vm.searchModel.phone = tableState.search.predicateObject.phone;
                vm.searchModel.email = tableState.search.predicateObject.email;
                vm.searchModel.accountId = tableState.search.predicateObject.accountId;
                vm.searchModel.finderNumber = tableState.search.predicateObject.finderNumber;
                vm.searchModel.isMailed = tableState.search.predicateObject.isMailed;
            }

            vm.searchModel.isAttending = null;
            vm.searchModel.isWaiting = null;
            vm.searchModel.isMailed = null;

            if (vm.showWaitQueue) {
                vm.searchModel.isAttending = true;
                vm.searchModel.isWaiting = true;
            }

            if (vm.showMailQueue) {
                vm.searchModel.isAttending = true;
                vm.searchModel.isWaiting = false;
                vm.searchModel.isMailed = false;
            }

            vm.isBusy = true;
            return service.getGuests(vm.selectedEvent.id, vm.searchModel)
                .then(function (data) {
                    vm.selectedEvent.guests = data.items;
                    vm.searchModel = data;
                    logger.log('search', vm.searchModel);
                    vm.isBusy = false;
                });

        }

        vm.paged = function paged() {
            vm.searchGuests(tableStateRef);
        };

        vm.saveEvent = function (form) {
            vm.isBusy = true;
            return service.update(vm.selectedEvent)
                .then(function (data) {
                    var guests = vm.selectedEvent.guests; 
                    angular.extend(vm.selectedEvent, data);

                    vm.selectedEvent.guests = guests;
                    vm.selectedEvent.isExpired = moment(vm.selectedEvent.endDate).toDate() < vm.currentDate;

                    logger.success('Saved Event: ' + data.name);
                })
                .finally(function () {
                    form.$setPristine();
                    vm.isBusy = false;
                });
        }

        vm.fileSelected = function ($files, $file, $event, $rejectedFiles) {
            var reader = new FileReader();
            reader.onloadstart = function () {

                vm.isBusy = true;
            };
            reader.onloadend = function () {
                vm.isBusy = false;

            }
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

        vm.toggleCancel = function (form) {
            vm.selectedEvent.isCancelled = !vm.selectedEvent.isCancelled;
            vm.saveEvent(form).then(function () {
                if (vm.selectedEvent.isCancelled) {
                    logger.warning('Cancelled event');
                } else {
                    logger.info('Restored event');
                }
            });
        }

        vm.showCreateEvent = function () {
            $modal.open({
                templateUrl: '/app/events/views/create-event.html',
                controller: 'CreateEventController',
                controllerAs: 'vm'
            }).result.then(function (data) {
                vm.selectedEvent = data;
                vm.events.unshift(vm.selectedEvent);
                logger.success('Successfully created ' + data.name);
            });
        }

        vm.showGuestUpload = function () {
            $modal.open({
                keyboard: false,
                backdrop: 'static',
                templateUrl: '/app/events/views/guest-upload.html',
                //controller: ['logger', '$uibModalInstance', 'fileService', 'selectedEvent', UploadGuestController],
                controller: 'UploadGuestController',
                controllerAs: 'vm',
                resolve: {
                    event: vm.selectedEvent
                }
            }).result.then(function (result) {
                if (result.success) {
                    logger.success(result.message);
                } else {
                    logger.error(result.message);
                }
                vm.changeEvent();
            });
        }

        function getEvents() {
            vm.isBusy = true;
            return service.get()
                .then(function (data) {
                    vm.events = data;
                }).finally(function () {
                    vm.isBusy = false;
                });
        }
     
    };

})();