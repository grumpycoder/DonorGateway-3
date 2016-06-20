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
                });
        })
        .run([
            '$rootScope', 'logger', function ($rootScope, logger) {
                $rootScope.$on('$locationChangeStart',
                    function (event, next, current) {
                        //HACK: Route to MVC routing if not Events url
                        if (!next.endsWith("Events")) window.location = next;
                    });
            }
        ]);
})();