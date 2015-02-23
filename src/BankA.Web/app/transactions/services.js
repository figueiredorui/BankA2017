'use strict';

app.service("TransactionsService", function ($http, AppSettings) {

    var urlBase = AppSettings.UrlBase + 'Transactions';

    this.uploadUrl = function () {
        return urlBase + '/upload';
    }

    this.getAll = function (accountID, startDate, endDate, tag) {

        var search = {
            AccountID: accountID,
            StartDate: startDate,
            EndDate: endDate,
            Tag: tag
        }

        return $http.get(urlBase + '/Search', {
            params: search
        });
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

    this.getTags = function () {
        return $http.get(urlBase + '/Tags');
    }

});