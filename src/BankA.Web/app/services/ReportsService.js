'use strict';

app.service("ReportsService", function ($http, AppSettings) {

    var urlBase = AppSettings.UrlBase + 'Reports';

    this.getMonthlyBalance = function () {
        return $http.get(urlBase + '/MonthlyBalance');
    }

});