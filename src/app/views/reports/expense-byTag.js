'use strict';
app.controller('ExpensebyTagCtrl', function($scope, $state, AccountService, ReportsService) {
    loadExpensesByTag();

    function loadExpensesByTag() {
        ReportsService.getExpensesByTag().success(function(response) {
            showPieChart(response);
        }).error(function(error) {
            $scope.errorMsg = error.Message;
        }).
        finally(function() {});
    }

    function showPieChart(data) {

        var tags = Enumerable.from(data).select(function(x) {return x.Tag}).take(10).toArray();
        var amount = Enumerable.from(data).select(function(x) {return x.Amount}).take(10).toArray();
        var amountTotal = Enumerable.from(data).take(10).sum(function(x) {return x.Amount});
        var percentage = Enumerable.from(data).select(function(x) {return (x.Amount / amountTotal) * 100}).take(10).toArray();


        var chart = {};
        chart.labels = tags;
        chart.data = amount;
        chart.percentage = percentage;

        $scope.chart = chart;
    }
})
