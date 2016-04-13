var SupplierController = function ($scope, $routeParams, $http) {
    var idParam = $routeParams.suppliertype;
    $scope.idParam = idParam;

    //declare variable for mainain ajax load and entry or edit mode
    $scope.loading = true;
    $scope.addMode = false;

    //get all customer information
    $http.get('/api/suppliers/').success(function (data) {
        $scope.suppliers = data;
        $scope.loading = false;
    })
    .error(function () {
        $scope.error = "An Error has occured while loading posts!";
        $scope.loading = false;
    });

    //by pressing toggleEdit button ng-click in html, this method will be hit
    $scope.toggleEdit = function () {
        this.supplier.editMode = !this.supplier.editMode;
    };

    //by pressing toggleAdd button ng-click in html, this method will be hit
    $scope.toggleAdd = function () {
        $scope.addMode = !$scope.addMode;
    };

    //Insert Customer Types
    $scope.add = function () {
        $scope.loading = true;
        $http.post('/api/suppliers/', this.newsupplier).success(function (data) {
            alert("Added Successfully!!");
            $scope.addMode = false;
            $scope.suppliers.push(data);
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Adding Customer Type! " + data;
            $scope.loading = false;
        });
    };

    //Edit Customer Types
    $scope.save = function () {
        //alert("Edit");
        $scope.loading = true;
        var frien = this.supplier;
        //alert(frien);
        $http.put('/api/suppliers/' + frien.Id, frien).success(function (data) {
            alert("Saved Successfully!!");
            frien.editMode = false;
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving customer type! " + data;
            $scope.loading = false;
        });
    };

    //Delete Customer
    $scope.deletesupplier = function () {
        $scope.loading = true;
        var Id = this.supplier.Id;
        $http.delete('/api/suppliers/' + Id).success(function (data) {
            alert("Deleted Successfully!!");
            $.each($scope.suppliers, function (i) {
                if ($scope.suppliers[i].Id === Id) {
                    $scope.suppliers.splice(i, 1);
                    return false;
                }
            });
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving Customer Type! " + data;
            $scope.loading = false;
        });
    };
}

// The $inject property of every controller (and pretty much every other type of object in Angular) needs to be a string array equal to the controllers arguments, only as strings
SupplierController.$inject = ['$scope', '$routeParams', '$http'];