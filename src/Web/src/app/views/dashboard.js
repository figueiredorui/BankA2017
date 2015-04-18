'use strict';

app.controller('DashboardCtrl', function ($scope, $rootScope, $state, AccountService, ReportsService) {

    $rootScope.accountID = '';
    
    $scope.showTransaction= showTransaction;
    $scope.selectAccount= selectAccount;

    loadAccountSummary();
    loadChart();

    function showTransaction(accountID) {
        $rootScope.accountID = accountID;
        $state.go('app.transactions');
    }

    function selectAccount(accountID) {
        $rootScope.accountID = accountID;
        loadChart();
    }

    function loadAccountSummary() {
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

        ReportsService.getGetMonthlyCashFlow($rootScope.accountID)
            .success(function (response) {

            var months = Enumerable.from(response).select(function (x) { return x.MonthYear }).toArray();
            var debit = Enumerable.from(response).select(function (x) { return x.DebitAmount }).toArray();
            var credit = Enumerable.from(response).select(function (x) { return x.CreditAmount }).toArray();

            if (months.length > 0)
            {
                var monthlyCashFlow = {};
                monthlyCashFlow.labels = months;
                monthlyCashFlow.series = ['Income', 'Expenses'];
                monthlyCashFlow.data = [credit,debit];

                $scope.monthlyCashFlow= monthlyCashFlow;
            }
            else
            {
                var monthlyCashFlow = {};
                monthlyCashFlow.labels = [];
                monthlyCashFlow.series = [];
                monthlyCashFlow.data = [];

                $scope.monthlyCashFlow= monthlyCashFlow;
            }

        })
            .error(function (error) {
            $scope.errorMsg = error.Message;
        })
            .finally(function () {

        });


        ReportsService.getRunningBalance($rootScope.accountID)
            .success(function (response) {

            var months = Enumerable.from(response).select(function (x) { return x.MonthYear }).toArray();
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

