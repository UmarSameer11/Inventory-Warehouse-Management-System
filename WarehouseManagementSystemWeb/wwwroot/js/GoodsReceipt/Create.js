
document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("goodsReceiptForm");
    const poInput = document.getElementById("PurchaseOrderNumber");
    const poIdInput = document.getElementById("PurchaseOrderId");
    const productRows = document.getElementById("productRows");
    const poMessage = document.getElementById("poMessage");
    const poLoading = document.getElementById("poLoading");

    if (!form || !poInput || !poIdInput || !productRows) return;

    let requestNumber = 0;

    poInput.addEventListener("keydown", async (event) => {
        if (event.key !== "Enter") return;

        event.preventDefault();

        const poNumber = poInput.value.trim();

        if (!poNumber) {
            showMessage("Please enter a PO number.", false);
            return;
        }

        const currentRequest = ++requestNumber;

        poIdInput.value = "";
        productRows.replaceChildren();
        updateTotals();
        showLoading(true);
        showMessage("Loading purchase order...", true);

        try {
            const url =
                `/GoodsReceipt/GetPurchaseOrderForGrn?purchaseOrderNumber=${encodeURIComponent(poNumber)}`;

            const response = await fetch(url, {
                headers: { Accept: "application/json" }
            });

            const result = await response.json();

            if (currentRequest !== requestNumber) return;

            if (!response.ok || !result.success || !result.data) {
                showMessage(
                    result.message || "Purchase order was not found.",
                    false
                );
                showEmptyRow("No products loaded.");
                return;
            }

            const po = result.data;
            const details = po.details ?? po.Details ?? [];

            console.log("Purchase Order Response:", po);
console.log("Purchase Order Details:", details);

details.forEach(item => {
    console.log({
        ProductName: item.productName ?? item.ProductName,
        OrderedQuantity: item.orderedQuantity ?? item.OrderedQuantity,
        PreviouslyReceivedQuantity:
            item.previouslyReceivedQuantity ??
            item.PreviouslyReceivedQuantity,
        RemainingQuantity:
            item.remainingQuantity ?? item.RemainingQuantity
    });
});

            poIdInput.value = po.purchaseOrderId ?? po.PurchaseOrderId;

            const pendingDetails = details.filter(item =>
                Number(item.remainingQuantity ?? item.RemainingQuantity) > 0
            );

            if (pendingDetails.length === 0) {
                showEmptyRow("This PO has no remaining quantities.");
                showMessage("Purchase order loaded. Nothing remains to receive.", true);
                return;
            }

            pendingDetails.forEach((item, index) => {
                const ordered = Number(
                    item.orderedQuantity ?? item.OrderedQuantity
                );
                const previouslyReceived = Number(
                    item.previouslyReceivedQuantity ??
                    item.PreviouslyReceivedQuantity
                );
                const remaining = Number(
                    item.remainingQuantity ?? item.RemainingQuantity
                );
                const productId = item.productId ?? item.ProductId;
                const detailId =
                    item.purchaseOrderDetailId ?? item.PurchaseOrderDetailId;
                const productName =
                    item.productName ?? item.ProductName ?? "Unknown Product";
                const unitPrice = Number(
                    item.unitPrice ?? item.UnitPrice ?? 0
                );

                const row = document.createElement("tr");

                addCell(row, productName);

                addCell(row, ordered.toFixed(3));
                addCell(row, previouslyReceived.toFixed(3));
                addCell(row, remaining.toFixed(3));

                // Receiving quantity
                const qtyCell = document.createElement("td");
                const qtyInput = document.createElement("input");

                qtyInput.type = "number";
                qtyInput.className = "form-control received-qty";
                qtyInput.name = `Details[${index}].QuantityReceived`;
                qtyInput.min = "0";
                qtyInput.max = remaining;
                qtyInput.step = "0.001";
                qtyInput.value = "0";
                qtyInput.required = true;

                qtyInput.addEventListener("input", () => {
                    const qty = Number(qtyInput.value);

                    qtyInput.setCustomValidity(
                        qty < 0 || qty > remaining
                            ? `Enter a quantity between 0 and ${remaining}.`
                            : ""
                    );

                    updateTotals();
                });

                qtyCell.appendChild(qtyInput);
                row.appendChild(qtyCell);

                // Batch ID: assumes the MVC Create model uses BatchId.
                const batchCell = document.createElement("td");
                const batchInput = document.createElement("input");

                batchInput.type = "number";
                batchInput.min = "1";
                batchInput.className = "form-control";
                batchInput.name = `Details[${index}].BatchId`;
                batchInput.placeholder = "Batch ID";

                batchCell.appendChild(batchInput);
                row.appendChild(batchCell);

                addCell(row, unitPrice.toFixed(2));

                const lineTotalCell = addCell(row, "0.00");
                lineTotalCell.classList.add("line-total");

                // Values needed by the MVC POST model.
                addHidden(row, `Details[${index}].PurchaseOrderDetailId`, detailId);
                addHidden(row, `Details[${index}].ProductId`, productId);
                addHidden(row, `Details[${index}].UnitPrice`, unitPrice);

                row.dataset.unitPrice = String(unitPrice);
                row.dataset.remaining = String(remaining);

                productRows.appendChild(row);
            });

            showMessage(
                `PO ${po.purchaseOrderNumber ?? po.PurchaseOrderNumber} loaded successfully.`,
                true
            );

            updateTotals();
        } catch (error) {
            console.error("Unable to load PO:", error);
            if (currentRequest === requestNumber) {
                showMessage("Unable to load purchase order. Please try again.", false);
                showEmptyRow("Unable to load products.");
            }
        } finally {
            if (currentRequest === requestNumber) {
                showLoading(false);
            }
        }
    });

    form.addEventListener("submit", (event) => {
        const rows = productRows.querySelectorAll("tr[data-remaining]");

        if (!poIdInput.value || rows.length === 0) {
            event.preventDefault();
            showMessage("Load a PO with pending products before saving.", false);
            return;
        }

        for (const row of rows) {
            const qtyInput = row.querySelector(".received-qty");
            const qty = Number(qtyInput.value);
            const remaining = Number(row.dataset.remaining);

            if (!qtyInput.value || qty <= 0 || qty > remaining) {
                event.preventDefault();
                qtyInput.setCustomValidity(
                    `Enter a quantity greater than 0 and no more than ${remaining}.`
                );
                qtyInput.reportValidity();
                return;
            }
        }
    });

    function addCell(row, value) {
        const cell = document.createElement("td");
        cell.textContent = value ?? "";
        row.appendChild(cell);
        return cell;
    }

    function addHidden(row, name, value) {
        const input = document.createElement("input");
        input.type = "hidden";
        input.name = name;
        input.value = value ?? "";
        row.appendChild(input);
    }

    function showEmptyRow(text) {
        productRows.replaceChildren();
        const row = document.createElement("tr");
        const cell = document.createElement("td");

        cell.colSpan = 8;
        cell.className = "text-center py-5 text-muted";
        cell.textContent = text;

        row.appendChild(cell);
        productRows.appendChild(row);
    }

    function showMessage(text, success) {
        poMessage.textContent = text;
        poMessage.className = success
            ? "small mt-2 text-success"
            : "small mt-2 text-danger";
    }

    function showLoading(show) {
        poLoading.classList.toggle("d-none", !show);
    }

    function updateTotals() {
        const rows = productRows.querySelectorAll("tr[data-remaining]");
        let totalProducts = 0;
        let totalQuantity = 0;
        let grandTotal = 0;

        rows.forEach(row => {
            const qtyInput = row.querySelector(".received-qty");
            const qty = Math.max(0, Number(qtyInput.value) || 0);
            const price = Number(row.dataset.unitPrice) || 0;

            totalQuantity += qty;
            grandTotal += qty * price;

            if (qty > 0) totalProducts++;

            row.querySelector(".line-total").textContent =
                (qty * price).toFixed(2);
        });

        document.getElementById("totalProducts").textContent = totalProducts;
        document.getElementById("totalQuantity").textContent =
            totalQuantity.toFixed(3);
        document.getElementById("grandTotal").textContent =
            grandTotal.toFixed(2);
    }
});
