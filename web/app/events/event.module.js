//mark.lawrence
//event.module.js

(function () {
    angular.module('app.events', [])
        .config(function ($routeProvider, $locationProvider) {
            $locationProvider.html5Mode(true);
            $routeProvider.when('/events',
            {
                templateUrl: 'app/events/views/home.html',
                caseInsensitiveMatch: true
            }).otherwise({
                controller: function () {
                    window.location = window.location.href;
                },
                template: "<div></div>"
            });
        })
        .run(['$rootScope', 'logger', function ($rootScope, logger) {
            $rootScope.$on('$routeChangeStart', function (event, next, current) {
            });
        }]);
})();