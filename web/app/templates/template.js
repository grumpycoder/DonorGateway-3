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
                var dataUrl = reader.result;
                vm.selectedTemplate.image = dataUrl.split(',')[1];
                vm.selectedTemplate.mimeType = $file.type;
            };
            reader.readAsDataURL($file);
        };

        vm.save = function () {
            vm.isBusy = true;
            service.update(vm.selectedTemplate)
                .then(function (data) {
                    vm.selectedTemplate = angular.extend(vm.selectedTemplate, data);
                    logger.success('Saved ' + vm.selectedTemplate.name);
                }).finally(function () {
                    vm.isBusy = false;
                });
        }
    }
})();