'use strict';

app.controller('ReportsCtrl', function ($scope, $state, AccountService, ReportsService) {

    loadAccounts();
    loadChart();


    function getProducts() {
        var entity = 'Product';
        var resource = 'getProducts';
        var url = 'http://' + window.location.host + '/api/' + entity + '/' + resource;
        return $http.get(url).
        success(function (data) {
            $scope.products = data;
            pivotUi();
        }).
        error(function (error) {
        });
    }

    function pivotUi() {
        var derivers = $.pivotUtilities.derivers;
        $("#productsPivotTableOutput").pivotUI($scope.products, {
            rows: ["Name"],
            cols: ["CanBeSold"],
            rendererName: "Table"
        });
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

