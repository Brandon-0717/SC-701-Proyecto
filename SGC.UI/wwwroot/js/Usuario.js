(() => {
    const Usuarios = {
        tabla: null,
        init: function () {
            this.InicializarTabla();
        },
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
    }
    $(document).ready(() => Usuarios.init());
})();