
function downloadFile() {

    const ref = firebase.database().ref('file/');


    ref.once('value', (data) => {

        var x = data.val();
        
        //document.getElementById("btnDescargargar").attributes('href', x[name][route]);
        console.log(x);
        
    })
}    


function llenarHref() {

    const ref = firebase.database().ref('file/');

    ref.once('value', (data) => {

        var x = data.val();
        var name = document.getElementById("nom1").value;
        alert(x[name][route]);
        document.links('btnDescargar').href = x[name][route];

    })
}

jQuery(document).ready(function () {

    const ref = firebase.database().ref('file/');

    ref.once('value', (data) => {

        var x = data.val();
        var name = $("#ced").val();

        var hr = x[name]["route"];

        $("#divhref a").attr('href', hr);

    })
   
});