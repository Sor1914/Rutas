﻿function mostrarNotificacion(tipo, mensaje) {
    var claseEnc = definirTipoNotificacion(tipo, "Encabezado");
    var claseBoton = definirTipoNotificacion(tipo, "Boton");
    var header = document.getElementById("headerNotificacion");
    header.className = claseEnc;
    var btnCerrarNotificacion = document.getElementById("btnCerrarNotificacion");
    btnCerrarNotificacion.className = claseBoton;
    var mensajeNotificaion = document.getElementById("mensajeNotificacion");
    mensajeNotificaion.innerHTML = '<i class="fa fa-comments-o"></i> ' + mensaje;
    //mensajeNotificaion.textContent = mensaje;
    var modalNotificacion = new bootstrap.Modal(document.getElementById('modalNotificacion'), {
        backdrop: 'static',
        keyboard: false
    });
    modalNotificacion.show();
}

function definirTipoNotificacion(tipo, elemento) {
    if (tipo == 1) {
        claseEnc = "modal-header bg-success";
        claseBoton = "btn btn-outline-success";
    } else if (tipo == 2) {
        claseEnc = "modal-header bg-info";
        claseBoton = "btn btn-outline-info";
    } else if (tipo == 3) {
        claseEnc = "modal-header bg-danger";
        claseBoton = "btn btn-outline-danger";
    } else if (tipo == 4) {
        claseEnc = "modal-header bg-warning";
        claseBoton = "btn btn-outline-warning";
    }
    if (elemento == "Boton") {
        return claseBoton;
    } else {
        return claseEnc;
    }
}