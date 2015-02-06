'use strict';

app.controller('AccountsListCtrl', function ($scope, $state, BankAccountService) {

    $scope.accountList = [];
    $scope.account = null;

    $scope.updateTransaction = updateTransaction;

    init();

    function init() {
        loadAccounts();
    };

    function loadAccounts() {

        BankAccountsService.getAll()
            .success(function (response) {
                $scope.accountList = response;
            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {

            });

    };

    function updateAccount(account) {

        BankAccountsService.update(account)
            .success(function (response) {

            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {

            });

    };

})



