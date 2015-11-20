'use strict';

app.controller('FilesListCtrl', function ($scope, $state, $uibModal , FilesService) {

    $scope.pageTitle = 'Files';
    $scope.fileList = [];

    $scope.deleteFile = deleteFile;

    init();

    function init() {
        loadFiles();
    };

    function loadFiles() {

        FilesService.getAll()
            .success(function (response) {
                $scope.fileList = response;
            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {

            });

    };

    

    function deleteFile(id) {

        
    }
})




