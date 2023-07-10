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

tinymce.init({
    selector: '#mytextarea'
});
