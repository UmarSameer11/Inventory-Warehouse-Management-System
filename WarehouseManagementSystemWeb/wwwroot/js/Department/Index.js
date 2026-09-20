document.addEventListener("DOMContentLoaded", function () {

    /* ================================
       Department Search
    ================================= */

    const searchInput = document.getElementById("departmentSearch");
    const table = document.getElementById("departmentTable");

    if (searchInput && table) {

        searchInput.addEventListener("keyup", function () {

            const searchValue = this.value.toLowerCase().trim();

            const rows = table.querySelectorAll("tbody tr");

            rows.forEach(function (row) {

                const rowText = row.textContent.toLowerCase();

                if (rowText.includes(searchValue)) {
                    row.style.display = "";
                }
                else {
                    row.style.display = "none";
                }

            });

        });

    }


    /* ================================
       Success Message
    ================================= */

    const successMessage =
        document.getElementById("successMessage");

    if (successMessage) {

        const message =
            successMessage.dataset.message;

        if (message && typeof showSuccess === "function") {

            showSuccess(message);

        }

    }


    /* ================================
       Error Message
    ================================= */

    const errorMessage =
        document.getElementById("errorMessage");

    if (errorMessage) {

        const message =
            errorMessage.dataset.message;

        if (message && typeof showError === "function") {

            showError(message);

        }

    }

});