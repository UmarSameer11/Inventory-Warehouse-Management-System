document.addEventListener("DOMContentLoaded", function () {

    const deleteForm = document.getElementById("deleteEmployeeForm");

    if (!deleteForm) {
        return;
    }

    deleteForm.addEventListener("submit", function (event) {

        const confirmed = confirm(
            "Are you sure you want to delete this employee?"
        );

        if (!confirmed) {
            event.preventDefault();
        }
    });
});