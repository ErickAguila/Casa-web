$(document).ready(function () {
    $('#tableVuelos').DataTable({
        'paging': true,
        'lengthChange': true,
        'searching': true,
        'ordering': true,
        'info': true,
        'autoWidth': true,
        'responsive': true,
        'oLanguage': {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            }
        }
    })
    listarVuelo();
});

function listarVuelo() {
    $("#bodyTableVuelos").empty();
    var codigoUsu = $("#codigoUsuario").text()
    $.ajax({
        type: "POST",
        url: "/Piloto/ObtenerUsuario",
        data: { codigo: codigoUsu },
        success: function (resultado) {
            if (resultado > 0) {
                $.ajax({
                    dataType: "json",
                    type: "POST",
                    url: "/Piloto/ListarVuelos",
                    data: { rutPiloto: resultado },
                    success: function (resultado) {
                        $.each(resultado, function (i, item) {
                            $("#bodyTableVuelos").append("<tr><td>" + item.rut_piloto +
                            "</td><td>" + item.rut_copiloto +
                            "</td><td>" + item.aeronave_matricula +
                            "</td><td>" + item.fecha_vuelo.replace("T00:00:00", "") +
                            "</td><td>" + item.condicion_vuelo +
                            "</td><td>" + item.cant_horas_total +
                            "</td><td>" + item.cant_horas_piloto +
                            "</td><td>" + item.cant_horas_copiloto +
                            "</td><td>" + item.origen +
                            "</td><td>" + item.destino +
                            "</td><td>" + item.mision + "</td></tr>");
                        });
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        swal("Error detectado!", "Error al listar los vuelos", "error");
                    }
                });
            }
        }
    });   
}
