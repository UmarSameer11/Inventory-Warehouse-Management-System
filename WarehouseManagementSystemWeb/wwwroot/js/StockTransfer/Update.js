document.addEventListener("DOMContentLoaded", function () {

    const form = document.getElementById("stockTransferUpdateForm");
    const button = document.getElementById("updateStockTransferBtn");

    if (!form || !button) return;

    const notifyError = (message) => {
        if (typeof showError === "function") {
            showError(message);
        } else {
            alert(message);
        }
    };

    form.addEventListener("submit", function (event) {
        const from = document.getElementById("FromWarehouseId");
        const to = document.getElementById("ToWarehouseId");

        if (from && to && from.value && from.value === to.value) {
            event.preventDefault();
            notifyError("The source and destination warehouse must be different.");
            return;
        }

        if (!form.checkValidity()) return;

        button.disabled = true;
        button.innerHTML =
            '<span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>' +
            'Updating...';
    });
});
