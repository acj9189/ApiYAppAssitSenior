

$(function ($) {

    var cedula = $("#cedula").val();
    var tipo = $("#tipo").val();

    var listaTurnos = []; // lista para almacenar los id

    $('td[id^="td"]').click(function () {

        var valorId = $(this).attr('id'); // Obtiene el texto en el id de la etiqueta
        var cadena = valorId.substr(2); // le quita el texto td


        if ($(this).css("background-color") == "rgb(255, 255, 255)")
        {
            $(this).css("background-color", "#28a745");

            listaTurnos.push(cadena); // agrega a la lista de id para enviar al c#
        }

        else if ($(this).css("background-color") == "rgb(40, 167, 69)")
        {
            $(this).css("background-color", "#ffffff");

            var pos = listaTurnos.indexOf(cadena);
            listaTurnos.splice(pos, 1); //elimina de la lista si lo desmarcan

        }

    }); // CIERRE A LA FUNCION CLICK DEL TD


    $("#btnAgendarTurnos").click(function ()
    {
        //Envio por AJAX al servidor en C# 
        $.post('/AgendarTurnos/RecibirInfoAgendar',
            $.param({ listaTurnos: listaTurnos }, true),
            function ()
            {
                $('#myModal').modal();

                if (tipo === "M")
                {
                    $('#divCedula').html("Para la Cedula:  " + cedula + "  Del Medico");
                }

                else
                {
                    $('#divCedula').html("Para la Cedula: " + cedula + " Del Enfermero");
                }
               
                $('#cedulaR').val(cedula);
                $('#tipoR').val(tipo);
            });
    });


});