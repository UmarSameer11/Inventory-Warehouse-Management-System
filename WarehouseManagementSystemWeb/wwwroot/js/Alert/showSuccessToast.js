function showSuccess(message) {

    Swal.fire({
        icon: 'success',
        title: 'Success',
        text: message,
        confirmButtonText: 'OK'
    });
}


function showError(message) {

    Swal.fire({
        icon: 'error',
        title: 'Error',
        text: message,
        confirmButtonText: 'OK'
    });
}


document.addEventListener("DOMContentLoaded", function () {

    const successMessage =
        document.getElementById("successMessage");

    const errorMessage =
        document.getElementById("errorMessage");


    if (successMessage) {

        const message =
            successMessage.getAttribute("data-message");

        if (message) {
            showSuccess(message);
        }
    }


    if (errorMessage) {

        const message =
            errorMessage.getAttribute("data-message");

        if (message) {
            showError(message);
        }
    }

});