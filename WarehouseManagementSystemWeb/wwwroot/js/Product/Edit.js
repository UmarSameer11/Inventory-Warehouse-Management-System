
document.addEventListener("DOMContentLoaded", function () {

    const form = document.getElementById("productUpdateForm");
    const updateButton = document.getElementById("updateProductBtn");

    if (!form || !updateButton) {
        return;
    }


    /*
     * Handle form submission
     */

    form.addEventListener("submit", function (event) {

        /*
         * jQuery Validation
         */

        if (!$(form).valid()) {
            return;
        }


        /*
         * Prevent double submission
         */

        updateButton.disabled = true;

        updateButton.innerHTML = `
         <span class="spinner-border spinner-border-sm me-2" role = "status"  aria - hidden="true" ></span >
          Updating...`;

    });


    /*
     * Validation Summary
     */

    function updateValidationSummary() {

        const summary =
            form.querySelector(".validation-summary");

        if (!summary) {
            return;
        }

        const hasErrors =
            summary.querySelector("li") !== null;

        summary.style.display =
            hasErrors ? "block" : "none";
    }


    /*
     * Initial validation summary state
     */

    updateValidationSummary();


    /*
     * Update validation summary
     * when input values change
     */

    $(form).on(
        "input change",
        "input, select",
        function () {

            setTimeout(function () {

                updateValidationSummary();

            }, 100);

        }
    );

});

