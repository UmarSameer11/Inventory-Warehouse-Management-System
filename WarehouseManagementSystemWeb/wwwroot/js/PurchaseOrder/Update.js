
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("purchaseOrderForm");
    const productRows = document.getElementById("productRows");
    const template = document.getElementById("productRowTemplate");
    const addProductBtn = document.getElementById("addProductBtn");
    const grandTotal = document.getElementById("grandTotal");
    const productMessage = document.getElementById("productMessage");
    const saveBtn = document.getElementById("savePurchaseOrderBtn");

    if (
        !form ||
        !productRows ||
        !template ||
        !addProductBtn ||
        !grandTotal ||
        !productMessage ||
        !saveBtn
    ) {
        console.error("Purchase Order Edit form elements were not found.");
        return;
    }

    function showMessage(message, type = "") {
        productMessage.textContent = message;
        productMessage.className = "po-message";

        if (type) {
            productMessage.classList.add(type);
        }
    }

    function getRows() {
        return Array.from(
            productRows.querySelectorAll(".po-product-row")
        );
    }

    function reindexRows() {
        getRows().forEach(function (row, index) {
            row.querySelectorAll("[name]").forEach(function (element) {
                element.name = element.name.replace(
                    /Details\[\d+\]/g,
                    `Details[${index}]`
                );

                element.name = element.name.replace(
                    /Details\[__index__\]/g,
                    `Details[${index}]`
                );
            });
        });
    }

    function calculateRow(row) {
        const quantityInput = row.querySelector(".quantity-input");
        const priceInput = row.querySelector(".price-input");
        const lineTotalElement = row.querySelector(".line-total");

        const quantity = Number(quantityInput.value) || 0;
        const price = Number(priceInput.value) || 0;
        const total = quantity * price;

        lineTotalElement.textContent = total.toLocaleString("en-US", {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
        });

        return total;
    }

    function calculateGrandTotal() {
        let total = 0;

        getRows().forEach(function (row) {
            total += calculateRow(row);
        });

        grandTotal.textContent = total.toLocaleString("en-US", {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
        });

        return total;
    }

    function addProductRow() {
        const fragment = template.content.cloneNode(true);
        const row = fragment.querySelector(".po-product-row");

        productRows.appendChild(fragment);

        reindexRows();
        calculateGrandTotal();

        const newRow = productRows.lastElementChild;
        const productSelect = newRow.querySelector(".product-select");

        if (productSelect) {
            productSelect.focus();
        }

        showMessage("New product row added.", "success");
    }

    addProductBtn.addEventListener("click", addProductRow);

    productRows.addEventListener("input", function (event) {
        if (
            event.target.matches(".quantity-input") ||
            event.target.matches(".price-input")
        ) {
            calculateGrandTotal();
        }
    });

    productRows.addEventListener("change", function (event) {
        if (event.target.matches(".product-select")) {
            showMessage("Product selection updated.");
        }

        if (
            event.target.matches(".quantity-input") ||
            event.target.matches(".price-input")
        ) {
            calculateGrandTotal();
        }
    });

    productRows.addEventListener("click", function (event) {
        const removeBtn = event.target.closest(".remove-product-btn");

        if (!removeBtn) {
            return;
        }

        const row = removeBtn.closest(".po-product-row");

        if (!row) {
            return;
        }

        row.remove();

        reindexRows();
        calculateGrandTotal();

        if (getRows().length === 0) {
            showMessage(
                "No products remain. Add at least one product before updating.",
                "error"
            );
        } else {
            showMessage("Product removed from the form.", "success");
        }
    });

    form.addEventListener("submit", function (event) {
        reindexRows();

        const rows = getRows();

        if (rows.length === 0) {
            event.preventDefault();

            showMessage(
                "Please add at least one product before updating the purchase order.",
                "error"
            );

            addProductBtn.focus();
            return;
        }

        let valid = true;

        rows.forEach(function (row) {
            const product = row.querySelector(".product-select");
            const quantity = row.querySelector(".quantity-input");
            const price = row.querySelector(".price-input");

            const productError = row.querySelector(".product-error");
            const quantityError = row.querySelector(".quantity-error");
            const priceError = row.querySelector(".price-error");

            productError.textContent = "";
            quantityError.textContent = "";
            priceError.textContent = "";

            if (!product.value) {
                productError.textContent = "Select a product.";
                valid = false;
            }

            if (
                !quantity.value ||
                !Number.isFinite(Number(quantity.value)) ||
                Number(quantity.value) < 0.001
            ) {
                quantityError.textContent = "Enter a valid quantity.";
                valid = false;
            }

            if (
                !price.value ||
                !Number.isFinite(Number(price.value)) ||
                Number(price.value) < 0.01
            ) {
                priceError.textContent = "Enter a valid unit price.";
                valid = false;
            }
        });

        if (!valid) {
            event.preventDefault();
            showMessage(
                "Please correct the highlighted product fields.",
                "error"
            );
            return;
        }

        calculateGrandTotal();

        saveBtn.disabled = true;
        saveBtn.innerHTML =
            '<span class="spinner-border spinner-border-sm" ' +
            'aria-hidden="true"></span> Updating Purchase Order...';
    });

    // Initialize totals for products loaded from the database.
    reindexRows();
    calculateGrandTotal();

    if (getRows().length === 0) {
        showMessage(
            "No products were loaded. Add products before updating.",
            "error"
        );
    } else {
        showMessage(
            `${getRows().length} existing product(s) loaded.`
        );
    }
});
