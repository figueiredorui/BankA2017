'use strict';

app.controller('TransactionsListCtrl', function ($scope, $state, TransactionsService) {

    $scope.transactionList = [];
    $scope.transaction = null;

    $scope.updateTransaction = updateTransaction;

    init();

    function init() {
        loadTransactions();
    };

    function loadTransactions() {
        
        TransactionsService.getAll()
            .success(function (response) {
                $scope.transactionList = response;
            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {
               
            });

    };

    function updateTransaction(transaction) {

        TransactionsService.update(transaction)
            .success(function (response) {
                
            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {

            });

    };

})



