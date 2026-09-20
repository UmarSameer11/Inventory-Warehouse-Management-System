document.addEventListener("DOMContentLoaded", function () {

const form = document.getElementById("deleteDepartmentForm");
const deleteButton = document.getElementById("deleteDepartmentBtn");


if (!form || !deleteButton) {
    return;
}


form.addEventListener("submit", function (event) {

    const confirmed = confirm(
        "Are you sure you want to delete this department? This action cannot be undone."
    );


    if (!confirmed) {

        event.preventDefault();

        return;
    }


    // Prevent double submission

    deleteButton.disabled = true;

    deleteButton.innerHTML =
        '<span class="spinner-border spinner-border-sm me-2" ' +
        'role="status" aria-hidden="true"></span>' +
        'Deleting...';

});

});
