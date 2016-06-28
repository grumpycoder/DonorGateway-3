﻿//core.module.js
//mark.lawrence

(function () {
    angular.module('app.core',
        [
            //angular modules
            'ngMessages',
            'angularLocalStorage',
            'ngRoute',
            'ngAnimate',

            //custom modules
            'blocks.logger',
            'blocks.exception',

            //third party modules
            'smart-table',
            'ui.bootstrap',
            'ngTagsInput',
            'ngFileUpload',
            'rzModule',
            'switcher',
            'gfl.textAvatar',
            'textAngular',
            'ui.bootstrap.datetimepicker', 
            'ng-file-input'
        ])
        .constant('toastr', toastr)
        .constant('moment', moment);;
})();