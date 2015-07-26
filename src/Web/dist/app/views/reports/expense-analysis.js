'use strict';
app.controller('ExpenseAnalysisCtrl', function($scope, $state, $stateParams, AccountService, ReportsService) {

    $scope.selectedAccountID = '';
    $scope.ChangedAccountID = loadExpensesReport;
    

    loadAccounts();
    loadExpensesReport();

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


    function loadExpensesReport() {
        ReportsService.getExpenses($scope.selectedAccountID).success(function(response) {
            showPivot(response);
        }).error(function(error) {
            $scope.errorMsg = error.Message;
        }).
        finally(function() {});
    }


    function showPivot(data) {
        $scope.data = data;

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
