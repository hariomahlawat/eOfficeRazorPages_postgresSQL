window.onload = function () {
    $('#tableDatatable').dataTable();

    var successDiv = document.getElementById('success-message');
    var errorDiv = document.getElementById('error-message');

    var successMessage = successDiv.dataset.message;
    var errorMessage = errorDiv.dataset.message;

    if (successMessage) {
        displayToast('success', successMessage);
    }

    if (errorMessage) {
        displayToast('error', errorMessage);
    }
};
