'use strict';
app.controller('IncomeAnalysisCtrl', function($rootScope, $scope, $state, AccountService, ReportsService) {

   $scope.$on('selectedAccountChanged', function(event, args) {
        loadIncomeReport();
    });
    
    loadIncomeReport();

    function loadIncomeReport() {
        ReportsService.getIncome($rootScope.accountID).success(function(response) {
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
