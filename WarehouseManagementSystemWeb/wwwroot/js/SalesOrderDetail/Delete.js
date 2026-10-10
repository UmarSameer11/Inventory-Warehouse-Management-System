document.addEventListener("DOMContentLoaded", function () {

    const form = document.getElementById("deleteSalesOrderDetailForm");
    const button = document.getElementById("deleteSalesOrderDetailBtn");

    if (!form || !button) return;

    form.addEventListener("submit", function (event) {

        const confirmed = confirm(
            "Are you sure you want to delete this sales order detail? This action cannot be undone."
        );

        if (!confirmed) {
            event.preventDefault();
            return;
        }

        button.disabled = true;
        button.innerHTML =
            '<span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>' +
            'Deleting...';
    });
});
