'use strict';

app.service("TransactionsService", function ($http, AppSettings) {

    var urlBase = AppSettings.UrlBase + 'Transactions';

    this.getAll = function () {
        return $http.get(urlBase + '');
    }

    this.get = function (id) {
        return $http.get(urlBase + '/' + id);
    }

    this.add = function (expense) {
        return $http.post(urlBase, expense);
    }

    this.update = function (expense) {
        return $http.put(urlBase, expense);
    }

    this.delete = function (id) {
        return $http.delete(urlBase + '/' + id);
    }

    
});