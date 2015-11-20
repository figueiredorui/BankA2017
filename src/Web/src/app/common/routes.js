
// /banka/1001/dashboard/1
// /banka/1001/transactions/1
// /banka/1001/account/1/transactions

app.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
    $urlRouterProvider.otherwise("/dashboard/");
    $stateProvider
        .state('app', {
        abstract: true,
        url: "",
        templateUrl: "app/views/layout/layout.html",
        controller: 'LayoutCtrl',
    })
        .state('app.dashboard', {
        url: "/dashboard/:accountID",
        templateUrl: "app/views/dashboard/dashboard.html",
        controller: 'DashboardCtrl',
        data: { pageTitle: 'Dashboard' }
    })
        .state('app.transactions', {
        url: "/transactions/:accountID",
        templateUrl: "app/views/transactions/transactions.html",
        controller: 'TransactionsListCtrl',
        data: { pageTitle: 'Transactions' }
    })
        .state('app.accounts', {
        url: "/accounts",
        templateUrl: "app/views/accounts/accounts.html",
        controller: 'AccountsListCtrl',
        data: { pageTitle: 'Accounts' }
    })
    .state('app.files', {
        url: "/files",
        templateUrl: "app/views/files/files.html",
        controller: 'FilesListCtrl',
        data: { pageTitle: 'Files' }
    })
    .state('app.rules', {
        url: "/rules",
        templateUrl: "app/views/rules/rules.html",
        controller: 'RulesListCtrl',
        data: { pageTitle: 'Rules' }
    })
        .state('app.reports', {
        abstract: true,
        url: "/reports/:accountID",
        templateUrl: "app/views/reports/reports.html",
        data: { pageTitle: 'Reports' }
    })
        .state('app.reports.menu', {
        url: "",
        templateUrl: "app/views/reports/menu.html",
        data: { pageTitle: 'Reports' }
    })
        .state('app.reports.expenses-analysis', {
        url: "/expenses-analysis",
        templateUrl: "app/views/reports/expense-analysis.html",
        controller: 'ExpenseAnalysisCtrl',
        data: { pageTitle: 'Reports' }
    })
        .state('app.reports.expenses-byTag', {
        url: "/expenses-byTag",
        templateUrl: "app/views/reports/expense-byTag.html",
        controller: 'ExpensebyTagCtrl',
        data: { pageTitle: 'Reports' }
    })
        .state('app.reports.income-analysis', {
        url: "/income-analysis",
        templateUrl: "app/views/reports/income-analysis.html",
        controller: 'IncomeAnalysisCtrl',
        data: { pageTitle: 'Reports' }
    })
    .state('app.settings', {
        url: "/settings",
        templateUrl: "app/views/layout/settings.html",
        controller: 'SettingsCtrl',
        data: { pageTitle: 'Settings' }
    });
    
    
    //$locationProvider.html5Mode(true);

});
