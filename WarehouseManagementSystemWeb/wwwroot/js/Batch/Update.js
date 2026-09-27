document.addEventListener("DOMContentLoaded", function () {

    const form = document.getElementById("batchUpdateForm");
    const submitButton = document.getElementById("updateBatchBtn");

    if (!form || !submitButton) {
        return;
    }

    const manufacturingInput = form.querySelector("#ManufacturingDate");
    const expiryInput = form.querySelector("#ExpiryDate");


    /*
     * Expiry date must be after the manufacturing date
     */

    let dateError = null;

    function showDateError(message) {

        if (!dateError) {
            dateError = document.createElement("span");
            dateError.className = "text-danger date-error";
            expiryInput.closest(".col-md-6").appendChild(dateError);
        }

        dateError.textContent = message;
        dateError.style.display = message ? "block" : "none";
    }

    function expiryIsValid() {

        if (!manufacturingInput || !expiryInput) {
            return true;
        }

        // Empty expiry date is allowed
        if (!manufacturingInput.value || !expiryInput.value) {
            showDateError("");
            return true;
        }

        // yyyy-MM-dd strings compare correctly as text
        if (expiryInput.value <= manufacturingInput.value) {
            showDateError("Expiry Date must be after Manufacturing Date.");
            return false;
        }

        showDateError("");
        return true;
    }

    function syncExpiryMin() {

        if (!manufacturingInput || !expiryInput) {
            return;
        }

        if (manufacturingInput.value) {
            expiryInput.min = manufacturingInput.value;
        } else {
            expiryInput.removeAttribute("min");
        }
    }

    syncExpiryMin();

    if (manufacturingInput) {
        manufacturingInput.addEventListener("change", function () {
            syncExpiryMin();
            expiryIsValid();
        });
    }

    if (expiryInput) {
        expiryInput.addEventListener("change", expiryIsValid);
    }


    /*
     * Handle form submission
     */

    form.addEventListener("submit", function (event) {

        // Client-side validation
        if (!$(form).valid() || !expiryIsValid()) {
            event.preventDefault();
            return;
        }

        // Prevent double submission
        submitButton.disabled = true;

        submitButton.innerHTML = `
            <span class="spinner-border spinner-border-sm me-2"
                  role="status"
                  aria-hidden="true">
            </span>
            Updating...
        `;

    });


    /*
     * Show the validation summary only when it has errors
     */

    function updateValidationSummary() {

        const summary = form.querySelector(".validation-summary");

        if (!summary) {
            return;
        }

        const hasErrors = summary.querySelector("li") !== null;

        summary.style.display = hasErrors ? "block" : "none";
    }

    updateValidationSummary();

    $(form).on("input change", "input, select", function () {

        setTimeout(updateValidationSummary, 100);

    });

});
