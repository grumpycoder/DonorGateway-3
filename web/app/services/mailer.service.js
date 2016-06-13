//mark.lawrence
//mailer.service.js

(function () {
    'use strict';

    var serviceId = 'mailerService';
    angular.module('app.service').factory(serviceId, serviceController);

    function serviceController(logger, $http, config) {
        logger.log(serviceId + ' loaded');
        var url = config.apiUrl + config.apiEndPoints.Mailer;

        var service = {
            get: get,
            query: query,
            update: update
            //            remove: remove,
            //            save: save,
            //            search: search,
        }

        return service;

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
    }
})();