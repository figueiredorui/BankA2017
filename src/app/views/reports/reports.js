'use strict';

app.controller('ReportsCtrl', function ($scope, $state, AccountService, ReportsService) {

    loadAccounts();
    loadChart();

    function loadChart() {

        ReportsService.getDebitReport()
        .success(function (response) {

            expensesPivot(response);

        })
        .error(function (error) {
            $scope.errorMsg = error.Message;
        })
        .finally(function () {

        });
    }

    function expensesPivot(data) {
        $("#pivotTableOutput").pivotUI(data, {
            rows: ["Tag"],
            cols: ["Year", "Month"],
            vals: ["Amount"],
            rendererName: "Table",
            aggregatorName: "Sum"
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


})

