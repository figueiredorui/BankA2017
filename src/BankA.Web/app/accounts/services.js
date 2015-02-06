'use strict';

app.service("BankAccountService", function ($http, AppSettings) {

    var urlBase = AppSettings.UrlBase + 'Accounts';

    this.getAll = function () {
        return $http.get(urlBase + '');
    }

    this.get = function (id) {
        return $http.get(urlBase + '/' + id);
    }

    this.add = function (todo) {
        return $http.post(urlBase, todo);
    }

    this.update = function (todo) {
        return $http.put(urlBase, todo);
    }

    this.delete = function (id) {
        return $http.delete(urlBase + '/' + id);
    }
    
});