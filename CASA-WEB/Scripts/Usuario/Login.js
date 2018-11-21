$(document).ready(function () {
    
});

function btnIngresar() {
    var cadena;
    var Usuario = $("#txtUsuario").val();
    var Pass = $("#txtContrasenna").val();
    $.ajax({
        dataType: "json",
        type: "POST",
        url: "/Usuario/Login",
        data: {
            username: Usuario,
            password: Pass
        },
        success: function (resultado) {
            var arreglo = [resultado];
            console.log(arreglo);
            $.each(arreglo, function (i, item) {
                if (item.estadousuario_codigo == 1) {
                    if (item.perfil_codigo == 4) {
                        window.location.href = "/Piloto/Index";
                        pasarDato(item.codigo_usuario);
                    } else {
                        swal("Error detectado!", "Solo pilotos estan autorizados a ingresar en plataforma web", "error");
                    }
                } else {
                    swal("Error detectado!", "Usuario deshabilitado", "error");
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Error detectado!", "Error al iniciar sesión", "error");
        }
    });
}

function btnSalir() {
    $.ajax({
        type: "POST",
        url: "/Usuario/CerrarSesion",
        success: function (resultado) {
            if (resultado == "ok") {
                window.location.href = "/Usuario/index";
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            swal("Error detectado!", "Error al cerrar sesión", "error");
        }
    });
}
