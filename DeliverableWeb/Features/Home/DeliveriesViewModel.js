var DeliveriesViewModel = function ($) {

    this.addDelivery = function (hours) {

        var removeButton = $('<button>', { 'class': 'btn btn-danger remove-delivery', text: 'Remove' });
        
        $('#delivery-list').append($('<tr>')
            .append($('<td>', { text: hours }))
            .append($('<td>')
                .append(removeButton)));

        $('#delivery-hours').val('');
    }

    $(document).ready(function () {

        $('#delivery-hours').on("propertychange change click keyup input paste", function () {
            var val = $(this).val();
            var num = parseInt(val);
            $('#add-delivery').prop('disabled', isNaN(num) || num <= 0);
        });

        $(document).on('click', '.remove-delivery', function () {
            $(this).closest('tr').remove();
        });
    });
}