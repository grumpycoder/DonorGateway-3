//constituent.service.js
//mark.lawrence

(function () {
    'use strict';

    var serviceId = 'constituentService';
    angular.module('app.service').factory(serviceId, serviceController);

    function serviceController(logger, $http, config) {
        logger.log(serviceId + ' loaded');
        var url = config.apiUrl + config.apiEndPoints.Constituent;

        var service = {
            query: query,
            update: update,

            //TODO: Remove???
            remove: remove,
            get: get,
            create: create,
            download: download,
            save: save
        }

        return service;

        function create() {
        }

        function download(vm) {
            vm.allRecords = true;
            return $http.post(URL + '/search', vm)
                .then(function (response) {
                    return response.data;
                });
        }

        function get(id) {
            return $http.get(URL + '/id')
                .then(function (response) {
                    return response.data;
                });
        }

        function query(vm) {
            return $http.post(url + '/search', vm)
              .then(function (response) {
                  return response.data;
              });
        }

        function update(item) {
            return $http.put(url, item)
                .then(function (response) {
                    return response.data;
                });
        }

        function remove() { }

        function save() { }
    }
})();