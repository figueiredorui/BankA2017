'use strict';
app.controller('ExpenseAnalysisCtrl', function($rootScope, $scope, $state, $stateParams, AccountsService, ReportsService) {

    var accountID = $stateParams.accountID;
    
//    $scope.$on('selectedAccountChanged', function(event, args) {
//        var accountID = $rootScope.accountID;
//        loadExpensesReport();
//    });

    loadExpensesReport();


    function loadExpensesReport() {
        ReportsService.getExpenses(accountID).success(function(response) {
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
