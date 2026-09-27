document.addEventListener("DOMContentLoaded", function () {

    const form = document.getElementById("deleteStockForm");
    const deleteButton = document.getElementById("deleteStockBtn");

    if (!form || !deleteButton) {
        return;
    }

    form.addEventListener("submit", function (event) {

        // Stop form submission until the user confirms
        event.preventDefault();

        Swal.fire({
            title: "Delete Stock?",
            text: "Are you sure you want to delete this stock record?",
            icon: "warning",

            showCancelButton: true,

            confirmButtonText: "Yes, Delete",
            cancelButtonText: "Cancel",

            confirmButtonColor: "#dc3545",
            cancelButtonColor: "#6c757d",

            reverseButtons: true,
            focusCancel: true

        }).then((result) => {

            if (!result.isConfirmed) {
                return;
            }

            deleteButton.disabled = true;

            deleteButton.innerHTML = `
                <span class="spinner-border spinner-border-sm me-2"
                      role="status"
                      aria-hidden="true">
                </span>
                Deleting...
            `;

            form.submit();

        });

    });

});
