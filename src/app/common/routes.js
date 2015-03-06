app.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise("/dashboard");
    $stateProvider
        .state('app', {
            abstract: true,
            url: "",
            templateUrl: "app/views/layout.html",
            controller: 'LayoutCtrl',
        })
        .state('app.dashboard', {
            url: "/dashboard",
            templateUrl: "app/views/dashboard.html",
            controller: 'DashboardCtrl',
            data: { pageTitle: 'Dashboard' }
        })
        .state('app.transactions', {
            url: "/transactions",
            templateUrl: "app/views/transactions.html",
            controller: 'TransactionsListCtrl',
            data: { pageTitle: 'Transactions' }
        })
        .state('app.accounts', {
            url: "/accounts",
            templateUrl: "app/views/accounts.html",
            controller: 'AccountsListCtrl',
            data: { pageTitle: 'Accounts' }
        })
        .state('app.reports', {
            url: "/reports",
            templateUrl: "app/views/reports/reports.html",
            controller: 'ReportsCtrl',
            data: { pageTitle: 'Reports' }
        })
        .state('app.reports.expenses-analysis', {
            url: "/expenses-analysis",
            templateUrl: "app/views/reports/expense-analysis.html",
            controller: 'ExpenseAnalysisCtrl',
            data: { pageTitle: 'Reports' }
        })
    .state('app.reports.expenses-pie', {
            url: "/expenses-pie",
            templateUrl: "app/views/reports/expense-pie-chart.html",
            controller: 'ExpensePieChartCtrl',
            data: { pageTitle: 'Reports' }
        })
    
});
