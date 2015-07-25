'use strict';

app.service("StatementsService", function ($http, AppSettings) {

    var urlBase = AppSettings.ApiUrl + 'Statements';

    this.getAll = function () {
        return $http.get(urlBase + '');
    }

    this.get = function (id) {
        return $http.get(urlBase + '/' + id);
    }

    this.add = function (statement) {
        return $http.post(urlBase, statement);
    }

    this.update = function (statement) {
        return $http.put(urlBase + '/' + statement.FileID, statement);
    }

    this.delete = function (id) {
        return $http.delete(urlBase + '/' + id);
    }

});
