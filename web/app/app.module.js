//mark.lawrence
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
        'app.tax',
        'app.template',
        'app.mailers',
        'app.events'
    ]);

    //angular.module('app',
    //[
    //    //application modules
    //    'app.core',
    //    'app.service',
    //    'app.filter',

    //    //feature areas
    //    'app.nav',
    //    'app.users',
    //    'app.tax',
    //    'app.template',
    //    'app.mailers',
    //    'app.events'
    //]).directive('myDate', function (dateFilter, $parse) {
    //    return {
    //        restrict: 'EAC',
    //        require: '?ngModel',
    //        link: function (scope, element, attrs, ngModel, ctrl) {
    //            ngModel.$parsers.push(function (viewValue) {
    //                //return dateFilter(viewValue, 'yyyy-MM-ddTHH:mm');
    //                return dateFilter(viewValue, 'MM/dd/yyyy HH:mm');
    //            });
    //        }
    //    }
    //}).directive('moDateInput', function ($window) {
    //    return {
    //        require: '^ngModel',
    //        restrict: 'A',
    //        link: function (scope, elm, attrs, ctrl) {
    //            var moment = $window.moment;
    //            var dateFormat = attrs.moMediumDate;
    //            attrs.$observe('moDateInput', function (newValue) {
    //                if (dateFormat == newValue || !ctrl.$modelValue) return;
    //                dateFormat = newValue;
    //                ctrl.$modelValue = new Date(ctrl.$setViewValue);
    //            });

    //            ctrl.$formatters.unshift(function (modelValue) {
    //                scope = scope;
    //                if (!dateFormat || !modelValue) return "";
    //                var retVal = moment(modelValue).format(dateFormat);
    //                return retVal;
    //            });

    //            ctrl.$parsers.unshift(function (viewValue) {
    //                scope = scope;
    //                var date = moment(viewValue, dateFormat);
    //                return (date && date.isValid() && date.year() > 1950) ? date.toDate() : "";
    //            });
    //        }
    //    };
    //});
})();