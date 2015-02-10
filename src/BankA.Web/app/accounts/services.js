'use strict';

app.service("BankAccountService", function ($http, AppSettings) {

    var urlBase = AppSettings.UrlBase + 'Accounts';

    this.getAll = function () {
        return $http.get(urlBase + '');
    }

    this.get = function (id) {
        return $http.get(urlBase + '/' + id);
    }

    this.add = function (account) {
        return $http.post(urlBase, account);
    }

    this.update = function (account) {
        return $http.put(urlBase + '/' + account.AccountID, account);
    }

    this.delete = function (id) {
        return $http.delete(urlBase + '/' + id);
    }
    
});