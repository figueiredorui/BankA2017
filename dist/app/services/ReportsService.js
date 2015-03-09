'use strict';

app.service("ReportsService", function ($http, AppSettings) {

    var urlBase = AppSettings.UrlBase + 'Reports';

    this.getMonthlyDebitCredit = function () {
        return $http.get(urlBase + '/MonthlyDebitCredit');
    }

    this.getRunningBalance = function () {
        return $http.get(urlBase + '/RunningBalance');
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
