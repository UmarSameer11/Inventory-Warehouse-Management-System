
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("purchaseOrderForm");
    const productRows = document.getElementById("productRows");
    const template = document.getElementById("productRowTemplate");
    const addProductBtn = document.getElementById("addProductBtn");
    const grandTotalElement = document.getElementById("grandTotal");
    const productMessage = document.getElementById("productMessage");
    const saveButton = document.getElementById("savePurchaseOrderBtn");

    if (!form || !productRows || !template) {
        return;
    }

    function formatMoney(value) {
        return value.toLocaleString("en-PK", {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
        });
    }

    function showMessage(message, isError = false) {
        productMessage.textContent = message;
        productMessage.classList.toggle("text-danger", isError);
        productMessage.classList.toggle("text-muted", !isError);
    }

 
function reindexRows() {
    const rows = productRows.querySelectorAll(".po-product-row");

    rows.forEach(function (row, index) {
        row.querySelectorAll("[name]").forEach(function (input) {
            input.name = input.name.replace(
                /Details\[(?:__index__|\d+)\]/,
                `Details[${index}]`
            );
        });
    });
}


    function updateTotals() {
        const rows = productRows.querySelectorAll(".po-product-row");

        let grandTotal = 0;
        let productCount = 0;

        rows.forEach(function (row) {
            const quantityInput = row.querySelector(".quantity-input");
            const priceInput = row.querySelector(".price-input");
            const lineTotalElement = row.querySelector(".line-total");

            const quantity = Number(quantityInput.value) || 0;
            const price = Number(priceInput.value) || 0;

            const lineTotal =
                quantity > 0 && price > 0
                    ? quantity * price
                    : 0;

            lineTotalElement.textContent = formatMoney(lineTotal);
            grandTotal += lineTotal;

            if (row.querySelector(".product-select").value) {
                productCount++;
            }
        });

        grandTotalElement.textContent = formatMoney(grandTotal);

        if (rows.length === 0) {
            showMessage(
                "No products added. Click Add Product to begin."
            );
        } else {
            showMessage(
                `${productCount} product(s) selected across ${rows.length} row(s).`
            );
        }
    }

    function clearRowErrors(row) {
        row.querySelectorAll(".text-danger").forEach(function (element) {
            if (
                element.classList.contains("product-error") ||
                element.classList.contains("quantity-error") ||
                element.classList.contains("price-error")
            ) {
                element.textContent = "";
            }
        });
    }

    function addProductRow() {
        const fragment = template.content.cloneNode(true);
        const row = fragment.querySelector(".po-product-row");

        productRows.appendChild(fragment);

        reindexRows();
        updateTotals();

        const newRow = productRows.lastElementChild;
        newRow.querySelector(".product-select").focus();
    }

    function validateRows() {
        const rows = productRows.querySelectorAll(".po-product-row");
        const selectedProducts = new Set();
        let isValid = true;

        if (rows.length === 0) {
            showMessage("Please add at least one product.", true);
            return false;
        }

        rows.forEach(function (row) {
            clearRowErrors(row);

            const productSelect = row.querySelector(".product-select");
            const quantityInput = row.querySelector(".quantity-input");
            const priceInput = row.querySelector(".price-input");

            const productId = productSelect.value;
            const quantity = Number(quantityInput.value);
            const price = Number(priceInput.value);

            if (!productId) {
                row.querySelector(".product-error").textContent =
                    "Select a product.";
                isValid = false;
            } else if (selectedProducts.has(productId)) {
                row.querySelector(".product-error").textContent =
                    "This product has already been added.";
                isValid = false;
            } else {
                selectedProducts.add(productId);
            }

            if (!quantityInput.value || !Number.isFinite(quantity) ||
                quantity < 0.001) {
                row.querySelector(".quantity-error").textContent =
                    "Quantity must be at least 0.001.";
                isValid = false;
            }

            if (!priceInput.value || !Number.isFinite(price) ||
                price < 0.01) {
                row.querySelector(".price-error").textContent =
                    "Unit price must be at least 0.01.";
                isValid = false;
            }
        });

        if (!isValid) {
            showMessage(
                "Please correct the product details before saving.",
                true
            );
        }

        return isValid;
    }

    addProductBtn.addEventListener("click", addProductRow);

    productRows.addEventListener("input", function (event) {
        if (
            event.target.matches(".quantity-input") ||
            event.target.matches(".price-input")
        ) {
            updateTotals();
        }
    });

    productRows.addEventListener("change", function (event) {
        if (event.target.matches(".product-select")) {
            const currentSelect = event.target;
            const currentValue = currentSelect.value;

            if (currentValue) {
                const duplicate = Array.from(
                    productRows.querySelectorAll(".product-select")
                ).some(function (select) {
                    return select !== currentSelect &&
                        select.value === currentValue;
                });

                if (duplicate) {
                    currentSelect.value = "";
                    showMessage(
                        "This product is already in the order. Select another product.",
                        true
                    );
                }
            }

            updateTotals();
        }
    });

    productRows.addEventListener("click", function (event) {
        const removeButton = event.target.closest(".remove-product-btn");

        if (!removeButton) {
            return;
        }

        const row = removeButton.closest(".po-product-row");

        if (row) {
            row.remove();
            reindexRows();
            updateTotals();
        }
    });

    form.addEventListener("submit", function (event) {
        reindexRows();

        if (!validateRows()) {
            event.preventDefault();
            return;
        }

        if (!form.checkValidity()) {
            event.preventDefault();
            form.reportValidity();
            return;
        }

        saveButton.disabled = true;
        saveButton.innerHTML =
            '<span class="spinner-border spinner-border-sm" ' +
            'role="status" aria-hidden="true"></span> Saving Purchase Order...';
    });

    // Start with one empty product row.
    addProductRow();
});
