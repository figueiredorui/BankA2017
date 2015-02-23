'use strict';

app.controller('HomeCtrl', function ($scope, $state, BankAccountService) {

    loadAccounts();


    function loadAccounts() {
        BankAccountService.getSummary()
            .success(function (response) {
                $scope.Accounts = response;
            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {

            });
    }
})

