'use strict';
app.controller('IncomeAnalysisCtrl', function($scope, $state, AccountService, ReportsService) {
    loadIncomeReport();

    function loadIncomeReport() {
        ReportsService.getIncome().success(function(response) {
            showPivot(response);
        }).error(function(error) {
            $scope.errorMsg = error.Message;
        }).
        finally(function() {});
    }

    function showPivot(data) {
        $("#pivotTableOutput").pivotUI(data, {
            rows: ["Tag"],
            cols: ["Year", "Month"],
            vals: ["Amount"],
            rendererName: "Table",
            aggregatorName: "Sum"
        });
    }
})
