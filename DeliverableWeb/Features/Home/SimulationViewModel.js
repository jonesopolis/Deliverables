var SimulationViewModel = function ($) {



    var increaseHour = function () {
        var val = 1 + Number($('#simulation-hour').text());
        $('#simulation-hour').text(val);
    }

    var setTrucks = function (trucks) {

        $('#simulation-trucks').empty();

        $.each(trucks, function (i, truck) {

            var trackingNumber;
            var hoursLeft;

            if (truck.activeDelivery == null) {
                trackingNumber = "N/A";
                hoursLeft = "N/A";
            } else {
                trackingNumber = truck.activeDelivery.trackingNumber;
                hoursLeft = truck.activeDeliveryHoursLeft;
            }
            

            $('#simulation-trucks').append($('<tr>')
                .append($('<td>', { text: truck.name }))
                .append($('<td>', { text: trackingNumber }))
                .append($('<td>', { text: hoursLeft })));
        });

    }

    var setDeliveries = function (deliveries) {

        $('#simulation-deliveries').empty();

        $.each(deliveries, function (i, delivery) {
            
            $('#simulation-deliveries').append($('<tr>')
                .append($('<td>', { text: delivery.hoursToComplete }))
                .append($('<td>', { text: delivery.trackingNumber == null ? "N/A" : delivery.trackingNumber }))
                .append($('<td>', { text: delivery.status })));
        });
    }

    this.run = function () {

        let connection = new signalR.HubConnection('/simulationHub');

        connection.on('update', data => {
            increaseHour();
            setTrucks(data.trucks);
            setDeliveries(data.deliveries);

            if (data.complete) {
                connection.stop();
                indexVm.simulationComplete();
            }
        });

        connection.start();
    }


}