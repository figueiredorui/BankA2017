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
        })
        .state('app.accounts', {
            url: "/accounts",
            templateUrl: "app/views/accounts.html",
            controller: 'AccountsListCtrl',
        })
});
