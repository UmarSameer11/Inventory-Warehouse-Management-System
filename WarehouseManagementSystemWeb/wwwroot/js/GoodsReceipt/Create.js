
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("goodsReceiptForm");
    const rowsContainer = document.getElementById("productRows");
    const template = document.getElementById("productRowTemplate");
    const addButton = document.getElementById("addProductBtn");
    const totalProducts = document.getElementById("totalProducts");
    const grandTotal = document.getElementById("grandTotal");
    const saveButton = document.getElementById("saveGrnBtn");

    if (!form || !rowsContainer || !template || !addButton) {
        return;
    }

    function getRows() {
        return Array.from(rowsContainer.querySelectorAll(".product-row"));
    }

    function reindexRows() {
        getRows().forEach(function (row, index) {
            row.querySelectorAll("[name]").forEach(function (element) {
                element.name = element.name.replace(
                    /Details\[\d+\]/g,
                    `Details[${index}]`
                );
            });

            row.querySelectorAll("[id]").forEach(function (element) {
                element.id = element.id.replace(
                    /Details_\d+__/g,
                    `Details_${index}__`
                );
            });

            row.dataset.index = index;
        });
    }

    function calculateTotals() {
        let grandTotalAmount = 0;
        const rows = getRows();

        rows.forEach(function (row) {
            const quantityInput = row.querySelector(".quantity-input");
            const priceInput = row.querySelector(".price-input");
            const lineTotalElement = row.querySelector(".line-total");

            const quantity = Number(quantityInput?.value) || 0;
            const price = Number(priceInput?.value) || 0;
            const lineTotal = quantity * price;

            grandTotalAmount += lineTotal;

            if (lineTotalElement) {
                lineTotalElement.textContent = lineTotal.toLocaleString(
                    "en-US",
                    {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }
                );
            }
        });

        totalProducts.textContent = rows.length;
        grandTotal.textContent = grandTotalAmount.toLocaleString("en-US", {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
        });
    }

    function addProductRow() {
        const currentIndex = getRows().length;

        const html = template.innerHTML.replace(
            /__index__/g,
            currentIndex
        );

        rowsContainer.insertAdjacentHTML("beforeend", html);

        reindexRows();
        calculateTotals();

        const rows = getRows();
        const lastRow = rows[rows.length - 1];

        lastRow?.querySelector(".product-select")?.focus();
    }

    addButton.addEventListener("click", addProductRow);

    rowsContainer.addEventListener("click", function (event) {
        const removeButton = event.target.closest(".remove-product-btn");

        if (!removeButton) {
            return;
        }

        const rows = getRows();

        // Keep at least one product row in the form.
        if (rows.length <= 1) {
            const row = rows[0];

            row.querySelector(".product-select").value = "";
            row.querySelector(".form-select:not(.product-select)").value = "";
            row.querySelector(".quantity-input").value = "";
            row.querySelector(".price-input").value = "";

            calculateTotals();
            return;
        }

        removeButton.closest(".product-row")?.remove();

        reindexRows();
        calculateTotals();
    });

    rowsContainer.addEventListener("input", function (event) {
        if (
            event.target.matches(".quantity-input") ||
            event.target.matches(".price-input")
        ) {
            calculateTotals();
        }
    });

    rowsContainer.addEventListener("change", function (event) {
        if (
            event.target.matches(".quantity-input") ||
            event.target.matches(".price-input")
        ) {
            calculateTotals();
        }
    });

    form.addEventListener("submit", function (event) {
        reindexRows();

        const rows = getRows();

        if (rows.length === 0) {
            event.preventDefault();
            alert("Please add at least one product.");
            return;
        }

        const invalidRow = rows.find(function (row) {
            const productId = row.querySelector(".product-select").value;
            const quantity = Number(
                row.querySelector(".quantity-input").value
            );
            const unitPrice = Number(
                row.querySelector(".price-input").value
            );

            return (
                !productId ||
                !Number.isFinite(quantity) ||
                quantity <= 0 ||
                !Number.isFinite(unitPrice) ||
                unitPrice < 0
            );
        });

        if (invalidRow) {
            event.preventDefault();
            alert(
                "Please select a product and enter a valid quantity and unit price for every row."
            );

            invalidRow.querySelector(".product-select")?.focus();
            return;
        }

        // Prevent accidental double submission.
        if (saveButton) {
            saveButton.disabled = true;
            saveButton.innerHTML =
                '<span class="spinner-border spinner-border-sm" aria-hidden="true"></span> Saving...';
        }
    });

    calculateTotals();
});
