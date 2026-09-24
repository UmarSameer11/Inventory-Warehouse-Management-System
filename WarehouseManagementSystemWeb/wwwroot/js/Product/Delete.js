document.addEventListener("DOMContentLoaded", function () {

    const form = document.getElementById("deleteProductForm");
    const deleteButton = document.getElementById("deleteProductBtn");

    if (!form || !deleteButton) {
        return;
    }


    form.addEventListener("submit", function (event) {

        // Stop form submission
        event.preventDefault();


        Swal.fire({
            title: "Delete Product?",
            text: "Are you sure you want to delete this product?",
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


            // Disable button
            deleteButton.disabled = true;


            // Show loading spinner
            deleteButton.innerHTML = `
                <span class="spinner-border spinner-border-sm me-2"
                      role="status"
                      aria-hidden="true">
                </span>
                Deleting...
            `;


            // Submit form
            form.submit();

        });

    });

});
