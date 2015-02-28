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

    this.add = function (transaction) {
        return $http.post(urlBase, transaction);
    }

    this.update = function (transaction) {
        return $http.put(urlBase + '/' + transaction.ID, transaction);
    }

    this.delete = function (id) {
        return $http.delete(urlBase + '/' + id);
    }

    this.getTags = function () {
        return $http.get(urlBase + '/Tags');
    }

    this.getChart = function () {
        return $http.get(urlBase + '/Chart');
    }

});
