'use strict';

app.controller('DashboardCtrl', function ($scope, $rootScope, $state, AccountService, ReportsService) {

    $scope.$on('selectedAccountChanged', function(event, args) {
        loadChart();
    });

    loadChart();

    function loadChart() {

        ReportsService.getGetMonthlyCashFlow($rootScope.accountID)
            .success(function (response) {

            var months = Enumerable.from(response).select(function (x) { return x.MonthYear }).toArray();
            var debit = Enumerable.from(response).select(function (x) { return x.DebitAmount }).toArray();
            var credit = Enumerable.from(response).select(function (x) { return x.CreditAmount }).toArray();

            if (months.length > 0)
            {
                var monthlyCashFlow = {};
                monthlyCashFlow.labels = months;
                monthlyCashFlow.series = ['Income', 'Expenses'];
                monthlyCashFlow.data = [credit,debit];

                $scope.monthlyCashFlow= monthlyCashFlow;
            }
            else
            {
                var monthlyCashFlow = {};
                monthlyCashFlow.labels = [];
                monthlyCashFlow.series = [];
                monthlyCashFlow.data = [];

                $scope.monthlyCashFlow= monthlyCashFlow;
            }

        })
            .error(function (error) {
            $scope.errorMsg = error.Message;
        })
            .finally(function () {

        });


        ReportsService.getRunningBalance($rootScope.accountID)
            .success(function (response) {

            var months = Enumerable.from(response).select(function (x) { return x.MonthYear }).toArray();
            var runningAmount = Enumerable.from(response).select(function (x) { return x.RunningAmount }).toArray();

            var runningBalance = {};
            runningBalance.labels = months;
            runningBalance.series = 'RunningAmount';
            runningBalance.data = [runningAmount];

            $scope.runningBalance= runningBalance;


            LoadChartNvd3(response);

        })
            .error(function (error) {
            $scope.errorMsg = error.Message;
        })
            .finally(function () {
        });
    }


    function LoadChartNvd3(dataValues)
    {
        $scope.data = [{
            key: '',
            values:dataValues
        }];

        $scope.options = {
            chart: {
                type: 'lineChart',
                interpolate: 'basis', 
                height: 450,
                margin : {
                    top: 20,
                    right: 20,
                    bottom: 60,
                    left: 55
                },
                useInteractiveGuideline: true,
                transitionDuration: 500,
                x: function(d){ return new Date(d.TransactionDate) },
                y: function(d){ return d.RunningAmount; },
                xScale : d3.time.scale(), 
                xAxis: {
                    showMaxMin: false,
                    axisLabel: 'Date',
                    ticks : d3.time.months,
                    tickFormat: function(d) {
                        var label = d3.time.format('%b/%y')(new Date(d))
                        return label;
                    },
                    axisLabelDistance: 50
                }
            }
        };
    }
})
