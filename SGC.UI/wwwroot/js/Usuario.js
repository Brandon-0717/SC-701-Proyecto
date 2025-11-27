(() => {
    const Usuarios = {
        tabla: null,
        init: function () {
            this.InicializarTabla();
            this.RegistrarEventos();
        },
        RegistrarEventos() {
            $('#tablaUsuarios').on('click', '.btn-edt', function () {
                const idUsuario = $(this).data('id');
                Usuarios.LlenarSelectRoles('#edt-selectRol');
                Usuarios.LlenarSelectEstados('#edt-selectEstado');
                Usuarios.MostrarDetallesUsuario(idUsuario);
            });

            $('#tablaUsuarios').on('click', '.btn-del', function () {
                const idUsuario = $(this).data('id'); Usuarios.EliminarUsuario(idUsuario);
            });

            $('#formRegistrarUsuario').on('submit', function (e) {
                e.preventDefault();
                Usuarios.RegistrarUsuario();
            });

            $('#modalCrearUsuario').on('shown.bs.modal', function () {
                Usuarios.LlenarSelectRoles('#selectRoles');
            });

            $('#modalCrearUsuario').on('hide.bs.modal', function () {
                const formulario = $('#formCrearUsuario');
                formulario[0].reset();
            });

            $('#formCrearUsuario').on('submit', function (e) {
                e.preventDefault();
                Usuarios.CrearUsuario();
            });

            $('#formModificarUsuario').on('submit', function (e) {
                e.preventDefault();
                Usuarios.ModificarUsuario();
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
        RegistrarUsuario() {
            let formulario = $('#formRegistrarUsuario');
            if (!formulario.valid()) return;

            Swal.fire({
                title: 'Registrando usuario...',
                allowOutsideClick: false,
                didOpen: () => {
                    Swal.showLoading();
                }
            });

            $.ajax({
                url: '/Usuario/Register',
                type: 'POST',
                data: formulario.serialize(),
                success: function (response) {
                    Swal.close(); // Ocultar loader

                    if (!response.esError) {
                        formulario[0].reset();
                        Swal.fire({
                            icon: 'success',
                            title: 'Usuario Creado',
                            text: response.mensaje,
                            showConfirmButton: true,
                        });
                    } else {
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
        LlenarSelectRoles(selectId) {
            $.ajax({
                url: '/Rol/ListarRoles',
                type: 'GET',
                success: function (response) {
                    const selectRoles = $(selectId);
                    selectRoles.empty();
                    if (response.data && response.data.length > 0) {
                        response.data.forEach(rol => {
                            selectRoles.append(`<option value="${rol.name}">${rol.normalizedName}</option>`);
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
        LlenarSelectEstados(selectId) {
            $.ajax({
                url: '/Estado/ListarEstados',
                type: 'GET',
                success: function (response) {
                    const selectEstados = $(selectId);
                    selectEstados.empty();
                    if (response.data && response.data.length > 0) {
                        response.data.forEach(estado => {
                            selectEstados.append(`<option value="${estado.estadoS_PK}">${estado.nombre_Estado}</option>`);
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

            let roles = $('#selectRoles').val(); 

            let data = formulario.serializeArray();
            data.push({ name: "NombreRoles", value: roles });

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
        },
        MostrarDetallesUsuario(id) { //Pendiente de completar
            $.get('/Usuario/ObtenerUsuarioDtoPorId', { id: id }, function (response) {
                if (!response.esError) {
                    let usuario = response.data;

                    // ==== CAMPOS BÁSICOS ====
                    $('#hiddenId').val(usuario.id);
                    $('#edt-identificacion').val(usuario.identificacion);
                    $('#edt-nombre').val(usuario.nombre);
                    $('#edt-apellido1').val(usuario.primerApellido);
                    $('#edt-apellido2').val(usuario.segundoApellido);
                    $('#edt-fechaNacimiento').val(usuario.fechaNacimiento?.substring(0, 10));
                    $('#edt-email').val(usuario.email);
                    $('#edt-telefono').val(usuario.telefono);
                    

                    var test = usuario.roles;
                    
                    // ==== ROLES ====
                    // Primero limpiamos selección
                    $('#edt-selectRol').val([]);

                    // Si vienen IDs de roles en el response, marcamos los que correspondan
                    if (usuario.roles && usuario.roles.length > 0) {

                        // Obtener IDs o nombres, según lo que uses en asp-for
                        let rolesUsuario = usuario.roles.map(r => r.name); // si usas IDs
                        // let rolesUsuario = usuario.roles.map(r => r.name); // si usas nombres

                        $('#edt-selectRol').val(rolesUsuario).change();
                    }

                    // Limpiar posibles mensajes de validación
                    $('#formModificarUsuario')
                        .find('.input-validation-error')
                        .removeClass('input-validation-error');


                    $('#modalModificarUsuario').modal('show');
                } else {
                    Swal.fire({
                        title: 'Ha ocurrido un error',
                        text: response.Mensaje,
                        icon: 'error'
                    });
                }
            });
        },
        ModificarUsuario() {

            let form = $('#formModificarUsuario');

            $.ajax({
                url: '/Usuario/ModificarUsuario',
                type: 'PUT',
                data: form.serialize(),
                success: function (response) {
                    if (!response.EsError) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Usuario modificado',
                            text: response.Mensaje,
                            showConfirmButton: false,
                            timer: 1490
                        })
                        $('#modalModificarUsuario').modal('hide');
                        Usuarios.tabla.ajax.reload();
                        form[0].reset();
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Ha ocurrido un error',
                            text: response.Mensaje,
                        })
                    }
                }
            });

        }
    }
    $(document).ready(() => Usuarios.init());
})();