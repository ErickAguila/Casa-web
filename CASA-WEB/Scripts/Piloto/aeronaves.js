$(document).ready(function () {
    $('#tableAeronaves').DataTable({
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
    ListarAeronavesDispónibles()
});

function ListarAeronavesDispónibles() {
    $("#bodyTableAeronaves").empty();
    $.ajax({
        dataType: "json",
        type: "POST",
        url: "/Piloto/ListarAeronavesDisponibles",
        success: function (resultado) {
            $.each(resultado, function (i, item) {
                $("#bodyTableAeronaves").append("<tr><td>" + item.matricula + "</td><td>" + item.cant_horas_vuelo + "</td><td>" + item.f_inspeccion.replace("T00:00:00", "") + "</td><td>" + item.f_aeronavegabilidad.replace("T00:00:00", "") + "</td><td>" + item.anio_fabricacion + "</td><td>" + item.descrip_tipoaeronave + "</td><td>" + item.DESCRIP_ESTADOAERONAVE + "</td></tr>");
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Error detectado!", "Error al listar las aeronaves disponibles", "error");
        }
    });
}