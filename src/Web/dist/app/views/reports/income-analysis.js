'use strict';
app.controller('IncomeAnalysisCtrl', function($scope, $state, AccountService, ReportsService) {

    $scope.selectedAccountID = '';
    $scope.ChangedAccountID = loadIncomeReport;

    loadAccounts();
    loadIncomeReport();

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

    function loadIncomeReport() {
        ReportsService.getIncome($scope.selectedAccountID).success(function(response) {
            showPivot(response);
        }).error(function(error) {
            $scope.errorMsg = error.Message;
        }).
        finally(function() {});
    }

    function showPivot(data) {

        var sum = $.pivotUtilities.aggregatorTemplates.sum;
        var numberFormat = $.pivotUtilities.numberFormat;
        var intFormat = numberFormat({digitsAfterDecimal: 2}); 

        $("#pivotTableOutput").pivot(data, {
            rows: ["Tag"],
            cols: ["Year", "Month"],
            aggregator: sum(intFormat)(["Amount"]),
        });
    }
})
