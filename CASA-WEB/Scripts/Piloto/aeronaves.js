var tableAeronaves;
$(document).ready(function () {
    CargarDatosAeronave();
    $.fn.dataTable.ext.errMode = 'throw'
    tableAeronaves = $('#tableAeronaves').dataTable({
        'bJQueryUI': true,
        'bLengthChange': true,
        'scrollCollapse': true,
        'bPaginate': true,
        'bFilter': true,
        "paging": true,
        "ordering": true,
        "info": true,
        'iDisplayLength': 10, //Paginacion
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
        },
        "columns": [
            { "data": "matricula", "sClass": "text-left" },
            { "data": "cant_horas_vuelo", "sClass": "text-left" },
            {
                "data": "f_inspeccion", fnCreatedCell: function (nTd, sData, oData, iRow, iCol) {
                    $(nTd).html("<center>" + oData.f_inspeccion.replace("T00:00:00", "") + "</center>");
                }
            },
            {
                "data": "f_aeronavegabilidad", fnCreatedCell: function (nTd, sData, oData, iRow, iCol) {
                    $(nTd).html("<center>" + oData.f_aeronavegabilidad.replace("T00:00:00", "") + "</center>");
                }
            },
            { "data": "anio_fabricacion", "sClass": "text-center" },
            { "data": "descrip_tipoaeronave", "sClass": "text-center" },
            { "data": "DESCRIP_ESTADOAERONAVE", "sClass": "text-center" }
        ]
    });
});

function CargarDatosAeronave() {
    $.ajax({
        dataType: "json",
        type: "POST",
        url: "/Piloto/ListarAeronavesDisponibles",
        async: true,
        success: function (result) {
            LlenarDatosAeronave(result);
        }, error: function (xhr, ajaxOptions, thrownError) {
            swal("Error detectado!", "Error al cargar el listado de aronaves", "error");
        }
    });
}

function LlenarDatosAeronave(respuesta) {
    tableAeronaves.fnClearTable();
    tableAeronaves.fnAddData(respuesta);
}
