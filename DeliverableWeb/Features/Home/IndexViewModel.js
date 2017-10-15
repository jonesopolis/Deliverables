var IndexViewModel = function ($) {

    this.runSimulation = function () {

        var jObj = {
            standardTrucks: [],
            customTrucks: [],
            deliveries: []
        }
        
        $('#truck-list').children('tr').each(function () {
            var truck = $(this).children('td').first().text();

            if (truck === 'Red' || truck === 'Blue') {
                jObj.standardTrucks.push(truck);
            } else {
                jObj.customTrucks.push(truck);
            }
        });
        
        $('#delivery-list').children('tr').each(function () {
            var delivery = $(this).children('td').first().text();
            jObj.deliveries.push(delivery);
        });

        $.ajax('/simulation',
            {
                data: JSON.stringify(jObj),
                contentType: 'application/json',
                type: 'POST',
                success: function (data) {
                    $('#main-body').html(data);
                }
            });
    }

    this.simulationComplete = function() {
        $.ajax('/complete',
            {
                contentType: 'application/json',
                type: 'POST',
                success: function (data) {
                    $('#main-body').html(data);
                }
            });
    }
}