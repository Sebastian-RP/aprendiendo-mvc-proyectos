﻿async function manejarErrorApi(respuesta) {
    let mensajeError = '';

    if (respuesta.status === 400) {
        mensajeError = await respuesta.text();
    } else if (respuesta.status === 404) {
        mensajeError = "El recurso solicitado no fue encontrado";
    } else {
        mensajeError = "Ha ocurrido un error con su solicitud";
    }

    mostrarMensajeError(mensajeError);
}

function mostrarMensajeError(mensaje) {
    Swal.fire({
        icon: 'error',
        title: 'Error...',
        text: mensaje
    });
}

function confirmarAccion({ callbackAceptar, callbackCancelar, titulo }) {
    Swal.fire({
        title: titulo || '¿Realmente deseas hacer esto?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí',
        focusConfirm: true
    }).then((resultado) => {
        if (resultado.isConfirmed) {
            callbackAceptar();
        } else if (callbackCancelar) {
            // El usuario ha presionado el botón de cancelar
            callbackCancelar();
        }
    })
}