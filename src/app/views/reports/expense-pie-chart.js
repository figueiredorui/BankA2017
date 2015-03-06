'use strict';
app.controller('ExpensePieChartCtrl', function($scope, $state, AccountService, ReportsService) {
    loadDebitReport();

    function loadDebitReport() {
        ReportsService.getDebitReport().success(function(response) {
            showPieChart(response);
        }).error(function(error) {
            $scope.errorMsg = error.Message;
        }).
        finally(function() {});
    }

    function showPieChart(data) {
        
        var months = Enumerable.from(data).select(function(x) {return x.Tag}).toArray();
        var debit = Enumerable.from(data).select(function(x) {return x.DebitAmount}).toArray();
        var credit = Enumerable.from(data).select(function(x) {return x.CreditAmount}).toArray();
        
        var chart = {};
        chart.labels = months;
        chart.data = [credit, debit];
        $scope.chart = chart;
    }
})