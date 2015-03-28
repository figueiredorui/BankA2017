'use strict';

app.service("ReportsService", function ($http, AppSettings) {

    var urlBase = AppSettings.UrlBase + 'Reports';

    this.getGetMonthlyCashFlow = function (accountID) {
        return $http.get(urlBase + '/MonthlyCashFlow/' + accountID);
    }

    this.getRunningBalance = function (accountID) {
        return $http.get(urlBase + '/RunningBalance/' + accountID);
    }

    this.getExpenses = function () {
        return $http.get(urlBase + '/Expenses');
    }

    this.getExpensesByTag = function () {
        return $http.get(urlBase + '/ExpensesByTag');
    }

    this.getIncome = function () {
        return $http.get(urlBase + '/Income');
    }

});
