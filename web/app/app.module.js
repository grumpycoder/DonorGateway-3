﻿//mark.lawrence
//app.module.js

(function () {
    angular.module('app',
        [
            //application modules
            'app.core',
            'app.service',
            'app.filter',

            //feature areas
            'app.nav',
            'app.users',
            'app.tax'
        ]);
})();