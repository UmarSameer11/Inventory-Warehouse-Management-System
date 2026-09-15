document.addEventListener("DOMContentLoaded", function () {

const form =
    document.getElementById("employeeEditForm");

const updateButton =
    document.getElementById("updateEmployeeBtn");


if (!form || !updateButton) {
    return;
}


form.addEventListener("submit", function () {

    // Prevent multiple submissions
    updateButton.disabled = true;


    // Change button appearance
    updateButton.innerHTML = `
        < span class="spinner-border spinner-border-sm me-2"
    role = "status"
    aria - hidden="true" >
        </span >
        Updating...`;

});

});
