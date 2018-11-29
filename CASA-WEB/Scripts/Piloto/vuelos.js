var tableVuelos;
$(document).ready(function () {
    CargarDatosVuelos();
    $.fn.dataTable.ext.errMode = 'throw'
    tableVuelos = $('#tableVuelos').dataTable({
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
            { "data": "rut_piloto", "sClass": "text-left" },
            { "data": "rut_copiloto", "sClass": "text-left" },
            { "data": "aeronave_matricula", "sClass": "text-left" },
            {
                "data": "fecha_vuelo", fnCreatedCell: function (nTd, sData, oData, iRow, iCol) {
                    $(nTd).html("<center>" + oData.fecha_vuelo.replace("T00:00:00", "") + "</center>");
                }
            },
            { "data": "condicion_vuelo", "sClass": "text-center" },
            { "data": "cant_horas_total", "sClass": "text-center" },
            { "data": "cant_horas_piloto", "sClass": "text-center" },
            { "data": "cant_horas_copiloto", "sClass": "text-center" },
            { "data": "origen", "sClass": "text-center" },
            { "data": "destino", "sClass": "text-center" },
            { "data": "mision", "sClass": "text-center" },
        ]
    });
});

function CargarDatosVuelos() {
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
                    async: true,
                    success: function (result) {
                        LlenarDatosVuelos(result);
                    }, error: function (xhr, ajaxOptions, thrownError) {
                        swal("Error detectado!", "Error al cargar los vuelos", "error");
                    }
                });
            }
        }
    })
}

function LlenarDatosVuelos(respuesta) {
    tableVuelos.fnClearTable();
    tableVuelos.fnAddData(respuesta);
}
