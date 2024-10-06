var ruteMap, directionsService, directionsRenderer;

function initRuteMap(latitudInicial, longitudInicial, latitudFinal, longitudFinal) {
    // Inicializa el mapa solo si los valores son válidos
    if (!isNaN(latitudInicial) && !isNaN(longitudInicial)) {
        ruteMap = new google.maps.Map(document.getElementById('ruteMap'), {
            zoom: 7,
            center: { lat: latitudInicial, lng: longitudInicial }
        });

        // Inicializa los servicios de Google Maps para rutas
        directionsService = new google.maps.DirectionsService();
        directionsRenderer = new google.maps.DirectionsRenderer();
        directionsRenderer.setMap(ruteMap);

        // Verifica si las coordenadas finales también son válidas
        if (!isNaN(latitudFinal) && !isNaN(longitudFinal)) {
            calcularRuta(latitudInicial, longitudInicial, latitudFinal, longitudFinal);
        } else {
            console.log("Latitud o longitud final no están disponibles, no se puede generar la ruta.");
        }
    } else {
        console.log("Latitud o longitud inicial no están disponibles, no se puede generar el mapa.");
    }
}

function calcularRuta(latitudInicial, longitudInicial, latitudFinal, longitudFinal) {
    var origen = { lat: latitudInicial, lng: longitudInicial };
    var destino = { lat: latitudFinal, lng: longitudFinal };

    var request = {
        origin: origen,
        destination: destino,
        travelMode: 'DRIVING' // Modo de viaje (DRIVING, WALKING, BICYCLING, TRANSIT)
    };

    directionsService.route(request, function (result, status) {
        if (status === 'OK') {
            directionsRenderer.setDirections(result);

            // Calcular la distancia entre los dos puntos
            var distancia = google.maps.geometry.spherical.computeDistanceBetween(
                new google.maps.LatLng(origen.lat, origen.lng),
                new google.maps.LatLng(destino.lat, destino.lng)
            );

            distancia = distancia / 1000;
            var distanciaRedondeada = parseFloat(distancia.toFixed(2)); 
            document.getElementById("kmDistancia").value = distanciaRedondeada;
           
        } else {
            console.log("Error al calcular la ruta: " + status);
        }
    });
}