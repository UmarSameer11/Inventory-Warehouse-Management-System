
document.addEventListener("DOMContentLoaded", function () {

    const form = document.getElementById("goodsReceiptUpdateForm");
    const rows = document.querySelectorAll(".grn-product-row");
    const grandTotalElement = document.getElementById("grnGrandTotal");

    function calculateTotals() {

        let grandTotal = 0;

        rows.forEach(function (row) {

            const quantityInput = row.querySelector(".received-quantity");
            const priceInput = row.querySelector('input[name$=".UnitPrice"]');
            const totalElement = row.querySelector(".row-total");

            if (!quantityInput || !priceInput || !totalElement) {
                return;
            }

            let quantity = parseFloat(quantityInput.value) || 0;
            const unitPrice = parseFloat(priceInput.value) || 0;

            const rowTotal = quantity * unitPrice;

            totalElement.textContent = rowTotal.toFixed(2);

            grandTotal += rowTotal;
        });

        if (grandTotalElement) {
            grandTotalElement.textContent = grandTotal.toFixed(2);
        }
    }

    // Quantity change
    rows.forEach(function (row) {

        const quantityInput = row.querySelector(".received-quantity");

        if (quantityInput) {

            quantityInput.addEventListener("input", function () {

                if (parseFloat(this.value) < 0) {
                    this.value = 0;
                }

                calculateTotals();
            });
        }
    });

    // Initial calculation
    calculateTotals();

    // Prevent invalid negative quantities
    if (form) {
        form.addEventListener("submit", function (event) {

            let invalidQuantity = false;

            rows.forEach(function (row) {

                const quantityInput = row.querySelector(".received-quantity");

                if (quantityInput && (
                    quantityInput.value === "" ||
                    !Number.isFinite(Number(quantityInput.value)) ||
                    Number(quantityInput.value) < 0
                )) {
                    invalidQuantity = true;
                    quantityInput.classList.add("is-invalid");
                } else if (quantityInput) {
                    quantityInput.classList.remove("is-invalid");
                }
            });

            if (invalidQuantity) {
                event.preventDefault();
            }
        });
    }

});
