//template.js
//mark.lawrence

(function () {
    var controllerId = 'TemplateController';

    angular.module('app.template').controller(controllerId, MainController);

    MainController.$inject = ['logger', 'templateService'];

    function MainController(logger, service) {
        var vm = this;

        vm.title = 'Templates';
        vm.description = 'Update event templates';

        vm.templates = [];
        vm.selectedTemplate = {};

        vm.template = {};
        vm.templateCopy = {};

        activate();

        function activate() {
            logger.log(controllerId + ' activated');
            getTemplates();
        }

        function getTemplates() {
            service.get()
                .then(function (data) {
                    vm.templates = data;
                    logger.log('templates', data);
                });
        }

        vm.fileSelected = function ($files, $file, $event, $rejectedFiles) {
            var reader = new FileReader();
            reader.onload = function (e) {
                var dataURL = reader.result;
                vm.selectedTemplate.image = dataURL.split(',')[1];
                vm.selectedTemplate.mimeType = $file.type;
            };
            reader.readAsDataURL($file);
        };

        vm.save = function () {
            service.update(vm.selectedTemplate)
                .then(function (data) {
                    vm.selectedTemplate = angular.extend(vm.selectedTemplate, data);
                });
        }
    }
})();