
document.addEventListener("DOMContentLoaded", function () {

    /* =========================================
       Employee Search
       ========================================= */

    const searchInput =
        document.getElementById("employeeSearch");

    const table =
        document.getElementById("employeeTable");

    if (searchInput && table) {

        searchInput.addEventListener("input", function () {

            const searchValue =
                this.value.toLowerCase().trim();

            const rows =
                table.querySelectorAll("tbody tr");

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


    /* =========================================
       Create Employee Form
       ========================================= */

    const createForm =
        document.getElementById("employeeCreateForm");

    const createButton =
        document.getElementById("createEmployeeBtn");

    if (createForm && createButton) {

        createForm.addEventListener("submit", function (event) {

            /*
             * Client-side validation is handled by
             * jQuery Unobtrusive Validation.
             *
             * Only disable the button when the
             * form is valid.
             */

            if ($(createForm).valid()) {

                createButton.disabled = true;

                createButton.innerHTML = `
             < span class="spinner-border spinner-border-sm me-2"
                 role = "status"
                 aria - hidden="true" ></span >
                 Creating...
                              `;

            }

        });

    }


    /* =========================================
       CNIC Formatting
       ========================================= */

    const cnicInput =
        document.getElementById("CNIC");

    if (cnicInput) {

        cnicInput.addEventListener("input", function () {

            let value =
                this.value.replace(/\D/g, "");

            // Maximum 13 digits
            if (value.length > 13) {

                value =
                    value.substring(0, 13);
            }

            // 12345-1234567
            if (value.length > 5 &&
                value.length <= 12) {

                value =
                    value.substring(0, 5)
                    + "-"
                    + value.substring(5);

            }

            // 12345-1234567-1
            else if (value.length > 12) {

                value =
                    value.substring(0, 5)
                    + "-"
                    + value.substring(5, 12)
                    + "-"
                    + value.substring(12);
            }

            this.value = value;

        });

    }

});
