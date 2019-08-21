var selectedFile;

$(document).ready(function () {

    $("#file").on("change", function (event) {
        selectedFile = event.target.files[0];
    });


});

function uploadFile() {
    var temp = document.getElementById("cedula").value;
    if (temp == "") {
        alert("Ingrese su cedula por favor ");
    }
    else {
        
        var filename = selectedFile.name;
        alert("hoja de vida "+filename);
        var storageRef = firebase.storage().ref('/hojasVida/' + filename);
        var uploadTask = storageRef.put(selectedFile);
        var databaseRef = firebase.database().ref('file/' + temp);

        // Register three observers:
        // 1. 'state_changed' observer, called any time the state changes
        // 2. Error observer, called on failure
        // 3. Completion observer, called on successful completion
        uploadTask.on('state_changed', function (snapshot) {

            // Observe state change events such as progress, pause, and resume
            // Get task progress, including the number of bytes uploaded and the total number of bytes to be uploaded
            var progress = (snapshot.bytesTransferred / snapshot.totalBytes) * 100;
            console.log('Upload is ' + progress + '% done');
            switch (snapshot.state) {
                case firebase.storage.TaskState.PAUSED: // or 'paused'
                    console.log('Upload is paused');
                    break;
                case firebase.storage.TaskState.RUNNING: // or 'running'
                    console.log('Upload is running');
                    break;
            }
        }, function (error) {
            // Handle unsuccessful uploads
        }, function () {
            // Handle successful uploads on complete
            // For instance, get the download URL: https://firebasestorage.googleapis.com/...
            uploadTask.snapshot.ref.getDownloadURL().then(function (downloadURL) {
                databaseRef.set({
                    name: filename,
                    route: downloadURL
                })
                console.log('File available at', downloadURL);
            });
        });
    }
}

