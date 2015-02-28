'use strict';

app.controller('DashboardCtrl', function ($scope, $state, AccountService, ReportsService) {

    loadAccounts();
    loadChart();

    function loadAccounts() {
        AccountService.getSummary()
            .success(function (response) {
                $scope.Accounts = response;
            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {

            });
    }

    function loadChart() {

        ReportsService.getMonthlyBalance()
            .success(function (response) {

                var months = Enumerable.from(response).select(function (x) { return x.Month }).toArray();
                var debit = Enumerable.from(response).select(function (x) { return x.DebitAmount }).toArray();
                var credit = Enumerable.from(response).select(function (x) { return x.CreditAmount }).toArray();

                $scope.labels = months;
                $scope.series = ['Income', 'Expenses'];
                $scope.data = [credit,debit];

            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {

            });

    }
})

