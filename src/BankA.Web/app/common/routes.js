app.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise("/home/dashboard");
    $stateProvider
        .state('home', {
            abstract: true,
            url: "/home",
            templateUrl: "app/common/views/master.html",
            controller: 'MasterCtrl',
        })
        .state('home.dashboard', {
            url: "/dashboard",
            templateUrl: "app/home/views/dashboard.html",
            controller: 'HomeCtrl',
        })
        .state('home.dashboard1', {
            url: "/home/dashboard1",
            templateUrl: "app/home/views/home.dashboard1.html",
            controller: 'HomeCtrl',
        })
        .state('transactions', {
            abstract: true,
            url: "/transactions",
            templateUrl: "app/common/views/master.html",
            controller: 'MasterCtrl',
        })
        .state('transactions.list', {
            url: "/list",
            templateUrl: "app/transactions/views/list.html",
            controller: 'TransactionsListCtrl',
        })
        .state('accounts', {
            abstract: true,
            url: "/accounts",
            templateUrl: "app/common/views/master.html",
            controller: 'MasterCtrl',
        })
        .state('accounts.list', {
            url: "/list",
            templateUrl: "app/accounts/views/list.html",
            controller: 'AccountsListCtrl',
        })
});