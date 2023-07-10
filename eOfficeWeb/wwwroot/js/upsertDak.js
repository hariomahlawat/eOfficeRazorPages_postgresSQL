function ValidateInput() {
    if (document.getElementById("uploadBox").value == "") {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'It seems you forgot to upload a file!',
        });
        return false;
    }
    return true;
}

function readURL(input) {
    if (input.files && input.files[0]) {
        src = URL.createObjectURL(event.target.files[0]);
        document.getElementById("myFrame").src = src;
    }
}



document.getElementById('uploadBoxCoveringLetter').addEventListener('change', function (e) {
    if (this.files && this.files[0]) {
        var src = URL.createObjectURL(e.target.files[0]);
        document.getElementById("myFrame").src = src;
    }
});

document.getElementById('uploadButton').addEventListener('click', function (e) {
    var uploadBox = document.getElementById("uploadBoxCoveringLetter");
    if (uploadBox.value == "") {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'It seems you forgot to upload a file!',
        });
        e.preventDefault();
    }
});
