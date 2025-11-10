(() => {
    const Usuarios = {
        tabla: null,
        init: function () {
            this.InicializarTabla();
            this.RegistrarEventos();
        },
        RegistrarEventos() {
            $('#tablaUsuarios').on('click', '.btn-del', function () {
                const idUsuario = $(this).data('id'); Usuarios.EliminarUsuario(idUsuario);
            });

            $('#modalCrearUsuario').on('shown.bs.modal', function () {
                Usuarios.LlenarSelectRoles();
            });
            $('#modalCrearUsuario').on('hide.bs.modal', function () {
                const formulario = $('#formCrearUsuario');
                formulario[0].reset();
            });

            $('#formCrearUsuario').on('submit', function (e) {
                e.preventDefault();
                Usuarios.CrearUsuario();
            });



        },
        //---------------------------------
        InicializarTabla() {
            this.tabla = $('#tablaUsuarios').DataTable({
                ajax: { url: '/Usuario/ObtenerUsuarios', type: 'GET', dataSrc: 'data' },

                columns: [
                    { data: 'identificacion', title: 'Identificación' },
                    { data: 'nombre', title: 'Nombre' },
                    {
                        data: 'roles',
                        title: 'Roles',
                        render: function (data) {
                            if (!data || data.length === 0) return 'Sin roles';
                            // une los nombres de los roles con comas
                            return data.map(r => r.name).join(', ');
                        }
                    },
                    {
                        data: 'nombreEstado',
                        title: 'Estado',
                        render: function (data) {
                            if (data === null) return 'Sin estado';
                            return data;
                        }

                    },
                    { data: 'email', title: 'Correo Electrónico' },
                    {
                        data: 'emailConfirmed', title: 'Email Confirmado',
                        render: function (data) {
                            if (!data) {
                                return '<span class="badge bg-danger text-white px-3 py-2 shadow-sm">No</span>';
                            } else {
                                return '<span class="badge bg-success text-white px-3 py-2 shadow-sm">Sí</span>';
                            }
                        }
                    },
                    {
                        data: null, title: 'Acciones', orderable: false,
                        render: row => `
                            <button class="btn btn-sm btn-primary btn-edt" data-id="${row.id}" title="Editar">
                                <i class="bi bi-pencil-square"></i>
                            </button>
                            <button class="btn btn-sm btn-danger btn-del" data-id="${row.id}" title="Eliminar">
                                <i class="bi bi-trash3"></i>
                            </button>                       `
                    }
                ],
                responsive: true,
                processing: true,
                pageLength: 10,
                columnDefs: [
                    { targets: '_all', className: 'text-center' }
                ],
                language: {
                    search: "Búsqueda",
                    lengthMenu: "Mostrar _MENU_ registros por página",
                    info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
                    paginate: {
                        first: "Primero",
                        last: "Último",
                        next: "Siguiente",
                        previous: "Anterior"
                    },
                    processing: "Procesando...",
                    zeroRecords: "No se encontraron registros coincidentes",
                    infoEmpty: "Mostrando 0 a 0 de 0 registros",
                    infoFiltered: "(filtrado de _MAX_ registros totales)",
                }
            });
        },
        EliminarUsuario(idUsuario) {
            Swal.fire({
                title: "Estas seguro de eliminar este usuario?",
                icon: "warning",
                showCancelButton: true,
                cancelButtonText: "Cancelar",
                confirmButtonText: "Si, eliminar",
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: '/Usuario/EliminarUsuario',
                        type: 'DELETE',
                        data: { id: idUsuario },
                        success: function (response) {
                            if (!response.esError) {
                                Swal.fire({
                                    title: "Elemento eliminado!",
                                    text: response.mensaje,
                                    icon: "success",
                                    showConfirmButton: false,
                                    timer: 1500
                                });
                                Usuarios.tabla.ajax.reload();

                            } else {
                                Swal.fire({
                                    title: "Ha ocurrido un error",
                                    text: response.mensaje,
                                    icon: "error"
                                });
                                Usuarios.tabla.ajax.reload();
                            }
                        }
                    });
                }
            });
        },
        LlenarSelectRoles() {
            $.ajax({
                url: '/Rol/ListarRoles',
                type: 'GET',
                success: function (response) {
                    const selectRoles = $('#selectRoles');
                    selectRoles.empty();
                    if (response.data && response.data.length > 0) {
                        response.data.forEach(rol => {
                            selectRoles.append(`<option value="${rol.id}">${rol.normalizedName}</option>`);
                        });
                    } else {
                        Swal.fire({
                            title: 'Ha ocurrido un error',
                            text: response.mensaje,
                            icon: 'error'
                        });
                    }
                }
            });
        },
        CrearUsuario() {
            let formulario = $('#formCrearUsuario');
            if (!formulario.valid()) return;

            let roles = $('#selectRoles option:selected').map(function () {
                return $(this).data('name'); // o .val() si tus valores son nombres
            }).get();

            let data = formulario.serializeArray();
            data.push({ name: 'roles', value: JSON.stringify(roles) });

            Swal.fire({
                title: 'Creando usuario...',
                allowOutsideClick: false,
                didOpen: () => {
                    Swal.showLoading();
                }
            });

            $.ajax({
                url: '/Usuario/CrearUsuario',
                type: 'POST',
                data: data,
                success: function (response) {
                    Swal.close(); // Ocultar loader

                    if (!response.esError) {
                        $('#modalCrearUsuario').modal('hide');
                        Usuarios.tabla.ajax.reload();
                        formulario[0].reset();

                        Swal.fire({
                            icon: 'success',
                            title: 'Usuario Creado',
                            text: response.mensaje,
                            showConfirmButton: false,
                            timer: 1490
                        });
                    } else {
                        $('#modalCrearUsuario').modal('hide');
                        formulario[0].reset();

                        Swal.fire({
                            icon: 'error',
                            title: "Ha ocurrido un error",
                            text: response.mensaje,
                            showConfirmButton: true,
                            confirmButtonText: 'Aceptar'
                        });
                    }
                },
                error: function () {
                    Swal.close(); // Ocultar también en error
                    Swal.fire({
                        icon: 'error',
                        title: "Error de conexión",
                        text: "No se pudo completar la solicitud.",
                        showConfirmButton: true
                    });
                }
            });
        }
    }
    $(document).ready(() => Usuarios.init());
})();