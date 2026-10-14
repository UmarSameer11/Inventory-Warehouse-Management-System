document.addEventListener("DOMContentLoaded", () => {
    const poInput = document.getElementById("PurchaseOrderNumber");
    const poIdInput = document.getElementById("PurchaseOrderId");
    const productRows = document.getElementById("productRows");
    const loading = document.getElementById("poLoading");
    const message = document.getElementById("poMessage");

    const form = poInput?.closest("form");

    // MVC endpoint: MVC service will call the API.
    const MVC_PO_LOOKUP_URL = "/GoodsReceipt/GetPurchaseOrderForGrn";
    async function loadPurchaseOrder(poNumber) {
        console.log("GRN: loadPurchaseOrder called for", poNumber);

        // Aapka existing code yahan se continue hoga.
    if (!poInput || !poIdInput || !productRows) {
        console.error("Required GRN form elements were not found.");
        return;
    }

    // Load Purchase Order when user presses Enter.
    poInput.addEventListener("keydown", async (event) => {
        if (event.key !== "Enter") return;

        event.preventDefault();

        console.log("GRN: Enter key pressed");

        const poNumber = poInput.value.trim();

        console.log("GRN: PO Number =", poNumber);

        if (!poNumber) {
            showMessage("Please enter a PO number.", "error");
            poInput.focus();
            return;
        }

        await loadPurchaseOrder(poNumber);
    });

    // Prevent GRN submission until a valid PO has loaded.
    form?.addEventListener("submit", (event) => {
        if (!poIdInput.value) {
            event.preventDefault();

            showMessage(
                "Please load a valid Purchase Order before saving the GRN.",
                "error"
            );

            poInput.focus();
            return;
        }

        const quantityInputs = productRows.querySelectorAll(
            ".quantity-input:not(:disabled)"
        );

        let hasReceivedQuantity = false;

        for (const input of quantityInputs) {
            const quantity = Number(input.value || 0);
            const maximum = Number(input.max);

            if (
                !Number.isFinite(quantity) ||
                quantity < 0 ||
                quantity > maximum
            ) {
                event.preventDefault();

                input.setCustomValidity(
                    `Enter a quantity between 0 and ${maximum}.`
                );

                input.reportValidity();
                input.focus();
                return;
            }

            input.setCustomValidity("");

            if (quantity > 0) {
                hasReceivedQuantity = true;
            }
        }

        if (!hasReceivedQuantity) {
            event.preventDefault();

            showMessage(
                "Enter a received quantity for at least one product.",
                "error"
            );
        }
    });

    async function loadPurchaseOrder(poNumber) {
        setLoading(true);
        showMessage("", "");

        // Clear old PO data before loading another PO.
        poIdInput.value = "";
        productRows.replaceChildren();
        updateTotals();

        try {
            const url =
                `${MVC_PO_LOOKUP_URL}?purchaseOrderNumber=` +
                encodeURIComponent(poNumber);

            const response = await fetch(url, {
                method: "GET",
                headers: {
                    Accept: "application/json"
                }
            });

            const responseText = await response.text();

            let result = {};

            if (responseText) {
                try {
                    result = JSON.parse(responseText);
                } catch {
                    throw new Error(
                        `Invalid server response. HTTP ${response.status}.`
                    );
                }
            }

            if (!response.ok) {
                throw new Error(
                    result.message ??
                    result.Message ??
                    `Purchase Order request failed. HTTP ${response.status}.`
                );
            }

            if (
                result.success === false ||
                result.Success === false
            ) {
                throw new Error(
                    result.message ??
                    result.Message ??
                    "Purchase Order could not be loaded."
                );
            }

            const po = result.data ?? result.Data ?? result;

            const purchaseOrderId =
                po.purchaseOrderId ?? po.PurchaseOrderId;

            if (!purchaseOrderId) {
                throw new Error(
                    "Purchase Order ID is missing from the response."
                );
            }

            const details =
                po.details ??
                po.Details ??
                po.purchaseOrderDetails ??
                po.PurchaseOrderDetails ??
                [];

            if (!Array.isArray(details) || details.length === 0) {
                throw new Error(
                    "No products were found for this Purchase Order."
                );
            }

            // Validate all details before rendering the rows.
            const rowsData = details.map((item, index) => {
                const ordered = toNumber(
                    item.orderedQuantity ??
                    item.OrderedQuantity ??
                    item.quantity ??
                    item.Quantity
                );

                const received = toNumber(
                    item.previouslyReceivedQuantity ??
                    item.PreviouslyReceivedQuantity
                );

                const remainingFromApi =
                    item.remainingQuantity ??
                    item.RemainingQuantity;

                const remaining = Math.max(
                    0,
                    toNumber(
                        remainingFromApi,
                        ordered - received
                    )
                );

                const productName =
                    item.productName ??
                    item.ProductName ??
                    "Product";

                const productId =
                    item.productId ?? item.ProductId;

                const detailId =
                    item.purchaseOrderDetailId ??
                    item.PurchaseOrderDetailId;

                const price = toNumber(
                    item.unitPrice ?? item.UnitPrice
                );

                if (!productId || !detailId) {
                    throw new Error(
                        `Product IDs are missing for row ${index + 1}.`
                    );
                }

                if (ordered < 0 || received < 0 || price < 0) {
                    throw new Error(
                        `Invalid quantity or price for ${productName}.`
                    );
                }

                return {
                    productId,
                    detailId,
                    productName,
                    ordered,
                    received,
                    remaining,
                    price
                };
            });

            rowsData.forEach((item, index) => {
                const row = document.createElement("tr");

                // Hidden fields submitted with the GRN form.
                appendHidden(
                    row,
                    `Details[${index}].PurchaseOrderDetailId`,
                    item.detailId
                );

                appendHidden(
                    row,
                    `Details[${index}].ProductId`,
                    item.productId
                );

                // Product name.
                const nameCell = document.createElement("td");
                const nameInput = document.createElement("input");

                nameInput.type = "text";
                nameInput.className = "form-control product-name";
                nameInput.readOnly = true;
                nameInput.value = item.productName;
                nameCell.appendChild(nameInput);
                row.appendChild(nameCell);

                // Ordered quantity.
                row.appendChild(
                    createTextCell(item.ordered.toFixed(3))
                );

                // Previously received quantity.
                row.appendChild(
                    createTextCell(item.received.toFixed(3))
                );

                // Remaining quantity.
                row.appendChild(
                    createTextCell(item.remaining.toFixed(3))
                );

                // Quantity being received now.
                const quantityCell = document.createElement("td");
                const quantityInput = document.createElement("input");

                quantityInput.type = "number";
                quantityInput.name = `Details[${index}].Quantity`;
                quantityInput.className =
                    "form-control quantity-input";
                quantityInput.min = "0";
                quantityInput.max = item.remaining.toString();
                quantityInput.step = "0.001";
                quantityInput.value = "0";
                quantityInput.required = item.remaining > 0;
                quantityInput.disabled = item.remaining <= 0;
                quantityInput.setAttribute(
                    "aria-label",
                    `Quantity to receive for ${item.productName}`
                );

                quantityCell.appendChild(quantityInput);
                row.appendChild(quantityCell);

                // Batch selection.
                const batchCell = document.createElement("td");
                const batchSelect = document.createElement("select");

                batchSelect.name = `Details[${index}].BatchId`;
                batchSelect.className = "form-select batch-select";

                const batchOption = document.createElement("option");
                batchOption.value = "";
                batchOption.textContent = "Select batch";
                batchSelect.appendChild(batchOption);

                batchCell.appendChild(batchSelect);
                row.appendChild(batchCell);

                // Unit price.
                const priceCell = document.createElement("td");
                const priceInput = document.createElement("input");

                priceInput.type = "number";
                priceInput.name = `Details[${index}].UnitPrice`;
                priceInput.className = "form-control price-input";
                priceInput.min = "0";
                priceInput.step = "0.01";
                priceInput.value = item.price.toFixed(2);
                priceInput.required = true;

                priceCell.appendChild(priceInput);
                row.appendChild(priceCell);

                // Line total.
                const totalCell = document.createElement("td");
                totalCell.className = "line-total";
                totalCell.textContent = "0.00";
                row.appendChild(totalCell);

                // Mark products that are fully received.
                if (item.remaining <= 0) {
                    row.classList.add("table-secondary");
                    row.title =
                        "This product has no remaining quantity.";
                }

                productRows.appendChild(row);
            });

            // Assign the PO ID only after all rows have been built.
            poIdInput.value = purchaseOrderId;

            showMessage(
                `Purchase Order ${poNumber} loaded successfully.`,
                "success"
            );

            updateTotals();

        } catch (error) {
            console.error(
                "GRN Purchase Order loading error:",
                error
            );

            poIdInput.value = "";
            productRows.replaceChildren();
            updateTotals();

            showMessage(
                error.message ||
                "PO load nahi hua. MVC controller aur API endpoint check karein.",
                "error"
            );
        } finally {
            setLoading(false);
        }
    }

    function appendHidden(row, name, value) {
        const input = document.createElement("input");

        input.type = "hidden";
        input.name = name;
        input.value = value;

        row.appendChild(input);
    }

    function createTextCell(value) {
        const cell = document.createElement("td");
        cell.textContent = value;
        return cell;
    }

    function toNumber(value, fallback = 0) {
        if (
            value === undefined ||
            value === null ||
            value === ""
        ) {
            return fallback;
        }

        const number = Number(value);

        return Number.isFinite(number) ? number : fallback;
    }

    function updateTotals() {
        let quantityTotal = 0;
        let amountTotal = 0;
        let productCount = 0;

        productRows.querySelectorAll("tr").forEach((row) => {
            const quantityInput =
                row.querySelector(".quantity-input");

            const priceInput =
                row.querySelector(".price-input");

            const totalCell =
                row.querySelector(".line-total");

            if (!quantityInput || !priceInput || !totalCell) {
                return;
            }

            const quantity = toNumber(quantityInput.value);
            const price = toNumber(priceInput.value);
            const lineTotal = quantity * price;

            totalCell.textContent = lineTotal.toFixed(2);

            quantityTotal += quantity;
            amountTotal += lineTotal;

            if (quantity > 0) {
                productCount++;
            }
        });

        const countElement =
            document.getElementById("totalProducts");

        const quantityElement =
            document.getElementById("totalQuantity");

        const totalElement =
            document.getElementById("grandTotal");

        if (countElement) {
            countElement.textContent = productCount;
        }

        if (quantityElement) {
            quantityElement.textContent =
                quantityTotal.toFixed(3);
        }

        if (totalElement) {
            totalElement.textContent = amountTotal.toFixed(2);
        }
    }

    // Quantity input validation and live total calculation.
    productRows.addEventListener("input", (event) => {
        if (event.target.matches(".quantity-input")) {
            const input = event.target;
            const quantity = Number(input.value);
            const maximum = Number(input.max);

            input.setCustomValidity(
                input.value !== "" &&
                    (
                        !Number.isFinite(quantity) ||
                        quantity < 0 ||
                        quantity > maximum
                    )
                    ? `Quantity must be between 0 and ${maximum}.`
                    : ""
            );
        }

        if (event.target.matches(".price-input")) {
            const input = event.target;
            const price = Number(input.value);

            input.setCustomValidity(
                input.value !== "" &&
                    (!Number.isFinite(price) || price < 0)
                    ? "Unit price cannot be negative."
                    : ""
            );
        }

        updateTotals();
    });

    // Recheck values when the user leaves or changes an input.
    productRows.addEventListener("change", (event) => {
        if (event.target.matches(".quantity-input")) {
            const input = event.target;
            const quantity = Number(input.value);
            const maximum = Number(input.max);

            input.setCustomValidity(
                !Number.isFinite(quantity) ||
                    quantity < 0 ||
                    quantity > maximum
                    ? `Quantity must be between 0 and ${maximum}.`
                    : ""
            );
        }

        if (event.target.matches(".price-input")) {
            const input = event.target;
            const price = Number(input.value);

            input.setCustomValidity(
                !Number.isFinite(price) || price < 0
                    ? "Unit price cannot be negative."
                    : ""
            );
        }

        updateTotals();
    });

    function setLoading(isLoading) {
        if (loading) {
            loading.classList.toggle("d-none", !isLoading);
        }

        poInput.disabled = isLoading;
    }

    function showMessage(text, type) {
        if (!message) return;

        message.textContent = text;

        message.classList.remove(
            "text-danger",
            "text-success",
            "text-warning"
        );

        if (type === "error") {
            message.classList.add("text-danger");
        } else if (type === "success") {
            message.classList.add("text-success");
        } else if (type === "warning") {
            message.classList.add("text-warning");
        }
    }
});