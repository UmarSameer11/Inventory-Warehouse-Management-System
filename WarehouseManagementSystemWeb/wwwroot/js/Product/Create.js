document.addEventListener("DOMContentLoaded", function () {

    const form = document.getElementById("productCreateForm");
    const submitButton = document.getElementById("createProductBtn");

    if (!form || !submitButton) {
        return;
    }


    form.addEventListener("submit", function (event) {

        /*
         * Client-side validation
         */

        if (!$(form).valid()) {
            return;
        }


        /*
         * Prevent double submission
         */

        submitButton.disabled = true;

        submitButton.innerHTML = `
            <span class="spinner-border spinner-border-sm me-2"
                  role="status"
                  aria-hidden="true">
            </span>
            Creating...
        `;

    });


    /*
     * Automatically remove validation summary
     * when there are no validation errors
     */

    function updateValidationSummary() {

        const summary = form.querySelector(".validation-summary");

        if (!summary) {
            return;
        }

        const hasErrors =
            summary.querySelector("li") !== null;

        summary.style.display =
            hasErrors ? "block" : "none";
    }


    /*
     * Run after page load
     */

    updateValidationSummary();


    /*
     * Update validation summary
     * whenever user changes an input
     */

    $(form).on("input change", "input, select", function () {

        setTimeout(function () {
            updateValidationSummary();
        }, 100);

    });

});