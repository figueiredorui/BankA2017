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

                //F7464A 46BFBD

                var months = Enumerable.From(response).Select(function (x) { return x.Month }).ToArray();
                var debit = Enumerable.From(response).Select(function (x) { return x.DebitAmount }).ToArray();
                var credit = Enumerable.From(response).Select(function (x) { return x.CreditAmount }).ToArray();

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

