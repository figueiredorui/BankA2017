'use strict';
app.controller('ExpenseAnalysisCtrl', function($scope, $state, AccountService, ReportsService) {
    loadExpensesReport();

    function loadExpensesReport() {
        ReportsService.getExpenses().success(function(response) {
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
