// Javascript CODE

$(function ($)
{
    //Informacion del tipo del servicio 
    var enfermero = $("#enfermero").val();
    var tipoS = $("#tipoS").attr('name');
    var duracion = $("#duracionS").attr('name');

    ////////////////////////////////////
    var Contador = 0;
    

    var listaIdsServicio = []; // lista para almacenar los id
    var fechaAux = "";

    $('td[id^="td"]').click(function ()
    {
        var valorId = $(this).attr('id'); // Obtiene el texto en el id de la etiqueta
        var idS = valorId.substr(2); // le quita el texto td

        var fechaServicio = $(this).attr('name');

        if (Contador === 0)
        {
            fechaAux = fechaServicio;
        }

        if (Contador < duracion)
        {
            if ($(this).css("background-color") == "rgb(40, 167, 69)")
            {
                if (fechaServicio == fechaAux)
                {
                    $(this).css("background-color", "#ffd800");
                    Contador++;

                    listaIdsServicio.push(idS); // agrega a la lista de id para enviar al c#
                }

                else
                {
                    alert("Por favor solicite un servicio por Dia")
                }
            }

            else if ($(this).css("background-color") == "rgb(255, 216, 0)")
            {
                $(this).css("background-color", "#28a745");
                Contador--;

                var pos = listaIdsServicio.indexOf(idS); 
                listaIdsServicio.splice(pos, 1); //elimina de la lista si lo desmarcan

            }
        }

        else
        {
            if ($(this).css("background-color") == "rgb(255, 216, 0)")
            {
                $(this).css("background-color", "#28a745");
                Contador--;

                var pos = listaIdsServicio.indexOf(idS);
                listaIdsServicio.splice(pos, 1); //elimina de la lista si lo desmarcan
            }

            else
            {
                alert("Error, solo puede solicitar un numero de " + duracion + " horas");
            }
            
        }      

    }); // CIERRE A LA FUNCION CLICK DEL TD


    $("#btnReservar").click(function ()
    {
        if (Contador == duracion)
        {
            //Envio por AJAX al servidor en C# 
            $.post('/ReservarServicio/RecibirInfoServicio',
                $.param({ tipo: tipoS, duracion: duracion, cedE: enfermero, listaIds: listaIdsServicio }, true),
                function () {
                    $('#myModal').modal();

                    $('#divTipo').html("Tipo Servicio: " + tipoS);
                    $('#tipoServicioBD').val(tipoS);
                    $('#divDuracion').html("Duracion: " + duracion);
                    $('#duracionBD').val(duracion);
                    $('#divEnfermero').html("Enfermero: " + enfermero);
                    $('#enfermeroBD').val(enfermero);
                });  
        }

        else
        {
            alert("Error, debe seleccionar la cantidad de horas que digito anteriormente");
        }
        
    });
});