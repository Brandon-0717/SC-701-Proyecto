document.addEventListener("DOMContentLoaded", () => {

    document.querySelectorAll(".btn-enviar").forEach(btn => {
        btn.addEventListener("click", () =>
            confirmarYEnviar(btn.dataset.id)
        );
    });

    document.querySelectorAll(".btn-aprobar").forEach(btn => {
        btn.addEventListener("click", () =>
            confirmarYAprobar(btn.dataset.id)
        );
    });

    document.querySelectorAll(".btn-devolver").forEach(btn => {
        btn.addEventListener("click", () =>
            pedirComentario(btn.dataset.id)
        );
    });

});

// -------------------------
// UTIL: TOKEN CSRF
// -------------------------
function getToken() {
    return document.querySelector('input[name="__RequestVerificationToken"]')?.value;
}

// -------------------------
// ENVIAR A APROBACIÓN
// -------------------------
function confirmarYEnviar(id) {
    Swal.fire({
        title: "¿Enviar a aprobación?",
        icon: "question",
        showCancelButton: true,
        confirmButtonText: "Sí",
        cancelButtonText: "Cancelar"
    }).then(result => {
        if (result.isConfirmed) {
            fetch("/SeguimientoSolicitudes/EnviarAprobacion", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "RequestVerificationToken": getToken()
                },
                body: JSON.stringify({ solicitudId: id })
            })
                .then(res => {
                    if (!res.ok) throw new Error();
                    Swal.fire("Enviado", "La solicitud fue enviada a aprobación", "success")
                        .then(() => location.reload());
                })
                .catch(() => Swal.fire("Error", "No se pudo enviar", "error"));
        }
    });
}

// -------------------------
// APROBAR
// -------------------------
function confirmarYAprobar(id) {
    Swal.fire({
        title: "¿Aprobar solicitud?",
        icon: "success",
        showCancelButton: true,
        confirmButtonText: "Aprobar",
        cancelButtonText: "Cancelar"
    }).then(result => {
        if (result.isConfirmed) {
            fetch("/SeguimientoSolicitudes/Aprobar", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "RequestVerificationToken": getToken()
                },
                body: JSON.stringify({ solicitudId: id })
            })
                .then(res => {
                    if (!res.ok) throw new Error();
                    Swal.fire("Aprobada", "La solicitud fue aprobada", "success")
                        .then(() => location.reload());
                })
                .catch(() => Swal.fire("Error", "No se pudo aprobar", "error"));
        }
    });
}

// -------------------------
// DEVOLVER
// -------------------------
function pedirComentario(id) {
    Swal.fire({
        title: "Motivo de devolución",
        input: "textarea",
        inputPlaceholder: "Ingrese el comentario...",
        showCancelButton: true,
        confirmButtonText: "Devolver",
        cancelButtonText: "Cancelar",
        inputValidator: value => {
            if (!value) return "Debe ingresar un comentario";
        }
    }).then(result => {
        if (result.isConfirmed) {
            fetch("/SeguimientoSolicitudes/Devolver", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "RequestVerificationToken": getToken()
                },
                body: JSON.stringify({
                    solicitudId: id,
                    comentario: result.value
                })
            })
                .then(res => {
                    if (!res.ok) throw new Error();
                    Swal.fire("Devuelta", "La solicitud fue devuelta", "warning")
                        .then(() => location.reload());
                })
                .catch(() => Swal.fire("Error", "No se pudo devolver", "error"));
        }
    });
}
