var TrucksViewModel = function($) {

    var addTruck = function (truck) {

        var removeButton = $('<button>', { 'class': 'btn btn-danger remove-truck', text: 'Remove' });
        

        $('#truck-list').append($('<tr>')
            .append($('<td>', { text: truck.name }))
            .append($('<td>')
                .append(removeButton)));
    }

    this.addRedTruck = function() {
        addTruck({ name: 'Red' });
    }

    this.addBlueTruck = function () {
        addTruck({ name: 'Blue' });
    }

    this.addCustomTruck = function (name) {
        addTruck({ name: name });
    }

    $(document).ready(function() {

        $('#custom-truck-name').on("propertychange change click keyup input paste", function () {
            var isEmpty = !$(this).val().length;
            $('#add-custom-truck').prop('disabled', isEmpty);
        });

        $(document).on('click', '.remove-truck', function () {
            $(this).closest('tr').remove();
        });
    });
}