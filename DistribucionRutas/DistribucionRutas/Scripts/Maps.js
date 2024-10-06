let map;
let marker;
let autocomplete;

function initMap() {
    const latitudInput = document.getElementById('Latitude').value;
    const longitudInput = document.getElementById('Longitude').value;

    const initialLocation = {
        lat: latitudInput ? parseFloat(latitudInput) : 14.6349149, // Valor por defecto si está vacío
        lng: longitudInput ? parseFloat(longitudInput) : -90.5068824  // Valor por defecto si está vacío
    };

    // Crear el mapa
    map = new google.maps.Map(document.getElementById('map'), {
        center: initialLocation,
        zoom: 20
    });

    marker = new google.maps.Marker({
        position: initialLocation,
        map: map,
        title: "Ubicación inicial" // Texto del marcador
    });

    // Crear un Autocomplete asociado al input de búsqueda
    autocomplete = new google.maps.places.Autocomplete(document.getElementById('autocomplete'));

    // Listener para cuando se selecciona un lugar en el autocomplete
    autocomplete.addListener('place_changed', () => {
        const place = autocomplete.getPlace();

        // Asegurarse de que el lugar tenga geometría
        if (!place.geometry) {
            console.error("No se encontró el lugar.");
            return;
        }

        // Obtener la ubicación seleccionada (latitud y longitud)
        const location = place.geometry.location;
        document.getElementById('Latitude').value = location.lat();
        document.getElementById('Longitude').value = location.lng();

        // Centramos el mapa en la nueva ubicación
        map.setCenter(location);
        map.setZoom(15); // Acercamos el zoom al lugar

        // Colocar o mover el marcador en el lugar seleccionado
        if (marker) {
            marker.setPosition(location);
        } else {
            marker = new google.maps.Marker({
                position: location,
                map: map,
                title: "Ubicación seleccionada"
            });
        }
    });

    // Listener para cuando se hace clic en el mapa
    map.addListener('click', function (event) {
        const clickedLocation = event.latLng;

        // Actualizar los inputs con las coordenadas
        document.getElementById('Latitude').value = clickedLocation.lat();
        document.getElementById('Longitude').value = clickedLocation.lng();

        // Colocar o mover el marcador en el lugar seleccionado
        if (marker) {
            marker.setPosition(clickedLocation);
        } else {
            marker = new google.maps.Marker({
                position: clickedLocation,
                map: map,
                title: "Ubicación seleccionada"
            });
        }
    });
}

// Inicializar el mapa cuando se muestre el modal
$('#modalNuevo').on('shown.bs.modal', function () {
    initMap();  // Inicializar el mapa cuando el modal esté completamente visible
});