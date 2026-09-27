document.addEventListener("DOMContentLoaded", function () {

    const form = document.getElementById("stockUpdateForm");
    const submitButton = document.getElementById("updateStockBtn");

    if (!form || !submitButton) {
        return;
    }

    const productSelect = form.querySelector("#ProductId");
    const batchSelect = form.querySelector("#BatchId");
    const quantityInput = form.querySelector("#Quantity");
    const reservedInput = form.querySelector("#ReservedQuantity");
    const availablePreview = form.querySelector("#availablePreview");
    const quantityError = form.querySelector("#quantityError");


    /* ============================================
       Batch dropdown - only show the batches
       of the selected product
       ============================================ */

    if (productSelect && batchSelect) {

        // Keep every batch option (rendered by the server) in memory
        const allBatches = Array.from(batchSelect.options)
            .filter(function (option) {
                return option.value !== "" && option.value !== "0";
            })
            .map(function (option) {
                return {
                    value: option.value,
                    text: option.textContent.trim(),
                    productId: option.getAttribute("data-product-id") || "0"
                };
            });

        function renderBatches(productId, selectedValue) {

            const hasProduct = productId !== "" && productId !== "0";

            const batches = hasProduct
                ? allBatches.filter(function (batch) {
                    return batch.productId === productId;
                })
                : [];

            batchSelect.innerHTML = "";

            const placeholder = document.createElement("option");
            placeholder.value = "0";

            if (!hasProduct) {
                placeholder.textContent = "Select a product first";
            } else if (batches.length === 0) {
                placeholder.textContent = "No batches for this product";
            } else {
                placeholder.textContent = "Select Batch";
            }

            batchSelect.appendChild(placeholder);

            batches.forEach(function (batch) {

                const option = document.createElement("option");

                option.value = batch.value;
                option.textContent = batch.text;
                option.selected = batch.value === selectedValue;

                batchSelect.appendChild(option);
            });
        }

        // Initial state (keeps the current batch on Update / after a failed post)
        renderBatches(productSelect.value, batchSelect.value);

        productSelect.addEventListener("change", function () {
            renderBatches(productSelect.value, "0");
        });
    }


    /* ============================================
       Available quantity preview
       ============================================ */

    function toNumber(value) {

        const number = parseFloat(value);

        return isNaN(number) ? 0 : number;
    }

    function updateAvailable() {

        if (!quantityInput || !reservedInput || !availablePreview) {
            return true;
        }

        const available = toNumber(quantityInput.value) - toNumber(reservedInput.value);

        availablePreview.textContent = available.toLocaleString(undefined, {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
        });

        availablePreview.classList.toggle("text-danger", available < 0);

        if (quantityError) {
            quantityError.style.display = available < 0 ? "block" : "none";
        }

        return available >= 0;
    }

    updateAvailable();

    if (quantityInput) {
        quantityInput.addEventListener("input", updateAvailable);
    }

    if (reservedInput) {
        reservedInput.addEventListener("input", updateAvailable);
    }


    /* ============================================
       Handle form submission
       ============================================ */

    form.addEventListener("submit", function (event) {

        // Client-side validation
        if (!$(form).valid() || !updateAvailable()) {
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


    /* ============================================
       Show the validation summary only when it has errors
       ============================================ */

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
