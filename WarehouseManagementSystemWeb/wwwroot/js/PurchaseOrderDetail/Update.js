document.addEventListener("DOMContentLoaded", function () {

    const form = document.getElementById("purchaseOrderDetailUpdateForm");
    const button = document.getElementById("updatePurchaseOrderDetailBtn");

    if (!form || !button) return;

    form.addEventListener("submit", function (event) {
        if (!form.checkValidity()) return;

        button.disabled = true;
        button.innerHTML =
            '<span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>' +
            'Updating...';
    });
});
