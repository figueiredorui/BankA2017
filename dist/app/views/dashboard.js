'use strict';

app.controller('DashboardCtrl', function ($scope, $rootScope, $state, AccountService, ReportsService) {


    $scope.showTransaction= showTransaction;

    loadAccounts();
    loadChart();

    function showTransaction(accountID) {
        $rootScope.accountID = accountID;
        $state.go('app.transactions');
    }

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


        ReportsService.getMonthlyDebitCredit()
            .success(function (response) {

                var months = Enumerable.from(response).select(function (x) { return x.Month }).toArray();
                var debit = Enumerable.from(response).select(function (x) { return x.DebitAmount }).toArray();
                var credit = Enumerable.from(response).select(function (x) { return x.CreditAmount }).toArray();

                var monthlyDebitCredit = {};
                monthlyDebitCredit.labels = months;
                monthlyDebitCredit.series = ['Income', 'Expenses'];
                monthlyDebitCredit.data = [credit,debit];

                $scope.monthlyDebitCredit= monthlyDebitCredit;

            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {

            });


        ReportsService.getRunningBalance()
            .success(function (response) {

            var months = Enumerable.from(response).select(function (x) { return x.Month }).toArray();
            var runningAmount = Enumerable.from(response).select(function (x) { return x.RunningAmount }).toArray();

                var runningBalance = {};
                runningBalance.labels = months;
                runningBalance.series = 'RunningAmount';
                runningBalance.data = [runningAmount];

                $scope.runningBalance= runningBalance;

            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {

            });


    }
})

