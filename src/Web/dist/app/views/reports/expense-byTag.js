'use strict';
app.controller('ExpensebyTagCtrl', function($scope, $state, AccountService, ReportsService) {
    
     $scope.selectedAccountID = '';
     $scope.ChangedAccountID = loadExpensesByTag;

    loadAccounts();
    loadExpensesByTag();

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

    function loadExpensesByTag() {
        ReportsService.getExpensesByTag($scope.selectedAccountID).success(function(response) {
            showChart(response);
        }).error(function(error) {
            $scope.errorMsg = error.Message;
        }).
        finally(function() {});
    }

    function showChart(data) {

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
