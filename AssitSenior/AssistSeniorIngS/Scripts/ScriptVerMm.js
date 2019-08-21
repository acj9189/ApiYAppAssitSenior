
$(function ($) {

    var bandera = false; // bandera que muestra el segundo div

    $('#btn-vermas').click(function () {

        if (!bandera)
        {
            bandera = true;
            $('#Lista2').show();
            $('#Labelvermm').html("Ver menos -");
            
        }

        else
        {
            bandera = false;
            $('#Lista2').hide();
            $('#Labelvermm').html("Ver más +");
        }
        

    }); // CIERRE A LA FUNCION CLICK DEL TD

});