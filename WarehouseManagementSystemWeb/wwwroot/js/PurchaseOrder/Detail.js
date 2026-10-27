document.addEventListener("DOMContentLoaded", function () {

    const detailPage = document.querySelector(".po-page");

    if (!detailPage) {
        return;
    }

    const productRows = document.querySelectorAll(
        ".po-table tbody tr"
    );

    const productCount = document.querySelector(".po-product-count");

    const grandTotalElement = document.querySelector(
        ".po-grand-total"
    );

    const copyButton = document.getElementById("copyPurchaseOrderNumber");

    const printButton = document.getElementById("printPurchaseOrder");

    /*
     * PRODUCT COUNT
     */

    if (productCount) {

        const count = productRows.length;

        productCount.innerHTML = `
            <i class="bi bi-boxes"></i>
            ${count} Product(s)
        `;
    }

    /*
     * VERIFY GRAND TOTAL
     *
     * Quantity and unit price are displayed in the table.
     * This calculation is for client-side verification only.
     */

    if (grandTotalElement && productRows.length > 0) {

        let calculatedTotal = 0;

        productRows.forEach(function (row) {

            const cells = row.querySelectorAll("td");

            if (cells.length < 6) {
                return;
            }

            const quantityText = cells[3].textContent.trim();
            const unitPriceText = cells[4].textContent.trim();

            const quantity = parseFormattedNumber(quantityText);
            const unitPrice = parseFormattedNumber(unitPriceText);

            if (
                Number.isFinite(quantity) &&
                Number.isFinite(unitPrice)
            ) {
                calculatedTotal += quantity * unitPrice;
            }

        });

        const displayedTotal = parseFormattedNumber(
            grandTotalElement.textContent.trim()
        );

        /*
         * Do not replace the server-calculated total.
         * Log discrepancies for debugging only.
         */

        if (
            Number.isFinite(displayedTotal) &&
            Math.abs(calculatedTotal - displayedTotal) > 0.02
        ) {
            console.warn(
                "Purchase Order total mismatch.",
                {
                    displayedTotal: displayedTotal,
                    calculatedTotal: calculatedTotal
                }
            );
        }
    }

    /*
     * COPY PURCHASE ORDER NUMBER
     *
     * Add a button with id="copyPurchaseOrderNumber"
     * if you want to enable this feature.
     */

    if (copyButton) {

        copyButton.addEventListener("click", async function () {

            const numberElement = document.getElementById(
                "purchaseOrderNumber"
            );

            if (!numberElement) {
                return;
            }

            const purchaseOrderNumber =
                numberElement.textContent.trim();

            if (!purchaseOrderNumber) {
                return;
            }

            try {

                await navigator.clipboard.writeText(
                    purchaseOrderNumber
                );

                const originalHtml = copyButton.innerHTML;

                copyButton.innerHTML =
                    '<i class="bi bi-check2"></i> Copied';

                copyButton.disabled = true;

                setTimeout(function () {
                    copyButton.innerHTML = originalHtml;
                    copyButton.disabled = false;
                }, 1500);

            } catch (error) {

                console.error(
                    "Unable to copy purchase order number.",
                    error
                );

            }

        });
    }

    /*
     * PRINT PURCHASE ORDER DETAILS
     *
     * Add a button with id="printPurchaseOrder"
     * if you want to enable this feature.
     */

    if (printButton) {

        printButton.addEventListener("click", function () {
            window.print();
        });

    }

    /*
     * PARSE FORMATTED NUMBERS
     *
     * Supports values such as:
     * 1,250.000
     * 100.00
     */

    function parseFormattedNumber(value) {

        if (!value) {
            return 0;
        }

        const normalizedValue = value.replace(/,/g, "");

        const number = Number(normalizedValue);

        return Number.isFinite(number) ? number : NaN;
    }

});