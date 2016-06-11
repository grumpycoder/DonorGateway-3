//tax.service.js
//mark.lawrence

(function () {
    'use strict';

    var serviceId = 'taxService';
    angular.module('app.service').factory(serviceId, serviceController);

    function serviceController(logger, $http, config) {
        logger.log(serviceId + ' loaded');
        var url = config.apiUrl + config.apiEndPoints.Tax;

        var service = {
            create: create,
            remove: remove,
            update: update

            //TODO: Remove ???
            //get: get,
            //query: query,
            //save: save
        }

        function remove(id) {
            return $http.delete(url + '/' + id)
                .then(function (response) {
                    return response.data;
                });
        }

        function create(item) {
            logger.log('item', item);
            logger.log('url', url);
            return $http.post(url, item)
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

        return service;

        function get(id) { }

        function query() {
        }
    }
})();