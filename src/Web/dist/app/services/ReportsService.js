'use strict';

app.service("ReportsService", function ($http, AppSettings) {

    var urlBase = AppSettings.ApiUrl + 'Reports';

    this.getGetMonthlyCashFlow = function (accountID) {
        
        if (accountID == null)
            accountID = '';
        
        return $http.get(urlBase + '/MonthlyCashFlow/' + accountID);
    }

    this.getRunningBalance = function (accountID) {
        
        if (accountID == null)
            accountID = '';
        
        return $http.get(urlBase + '/RunningBalance/' + accountID);
    }

    this.getExpenses = function (accountID) {
        
        if (accountID == null)
            accountID = '';
        
        return $http.get(urlBase + '/Expenses/'+ accountID);
    }

    this.getExpensesByTag = function (accountID) {
        
        if (accountID == null)
            accountID = '';
        
        return $http.get(urlBase + '/ExpensesByTag/'+ accountID);
    }

    this.getIncome = function (accountID) {
        
        if (accountID == null)
            accountID = '';
        
        return $http.get(urlBase + '/Income/'+ accountID);
    }

});
