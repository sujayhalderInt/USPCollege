angular.module("umbraco").controller("ExportController", function ($scope) {
	$scope.startDate = null;
	$scope.endDate = null;
    $scope.exportUrl = "/umbraco/backoffice/applications/GetApplications";

	$scope.setHref = function () {
	    var url = "/umbraco/backoffice/applications/GetApplications";
		if ($scope.startDate != null) {
			url += "?startDate=" + $scope.startDate;
		}

		if ($scope.endDate != null) {
			url += ($scope.startDate != null) ? "&" : "?";
			url += "endDate=" + $scope.endDate;
		}

		$scope.exportUrl = url;
	}
});