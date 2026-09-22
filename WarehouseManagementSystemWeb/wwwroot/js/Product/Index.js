document.addEventListener("DOMContentLoaded", function () {

    /* ============================================
       Product Search
       ============================================ */

    const searchInput =
        document.getElementById("productSearch");

    const productTable =
        document.getElementById("productTable");


    if (searchInput && productTable) {

        searchInput.addEventListener("input", function () {

            const searchValue =
                this.value.toLowerCase().trim();

            const rows =
                productTable.querySelectorAll("tbody tr");

            rows.forEach(function (row) {

                const rowText =
                    row.textContent.toLowerCase();

                if (rowText.includes(searchValue)) {

                    row.style.display = "";

                } else {

                    row.style.display = "none";

                }

            });

        });

    }


    /* ============================================
       Delete Confirmation
       ============================================ */

    const deleteButtons =
        document.querySelectorAll(".delete-btn");


    deleteButtons.forEach(function (button) {

        button.addEventListener("click", function (event) {

            event.preventDefault();

            const deleteUrl =
                this.getAttribute("href");


            const confirmDelete =
                confirm(
                    "Are you sure you want to delete this product?\n\n" +
                    "This action cannot be undone."
                );


            if (confirmDelete) {

                window.location.href = deleteUrl;

            }

        });

    });


    /* ============================================
       Success Message
       ============================================ */

    const successMessage =
        document.getElementById("successMessage");


    if (successMessage) {

        const message =
            successMessage.getAttribute("data-message");


        if (
            message &&
            typeof showSuccess === "function"
        ) {

            showSuccess(message);

        }

    }


    /* ============================================
       Error Message
       ============================================ */

    const errorMessage =
        document.getElementById("errorMessage");


    if (errorMessage) {

        const message =
            errorMessage.getAttribute("data-message");


        if (
            message &&
            typeof showError === "function"
        ) {

            showError(message);

        }

    }

});
