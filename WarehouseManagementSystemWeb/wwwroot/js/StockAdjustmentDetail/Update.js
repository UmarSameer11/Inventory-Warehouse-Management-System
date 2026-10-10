document.addEventListener("DOMContentLoaded", function () {

    const form = document.getElementById("stockAdjustmentDetailUpdateForm");
    const button = document.getElementById("updateStockAdjustmentDetailBtn");

    if (!form || !button) return;

    form.addEventListener("submit", function (event) {
        if (!form.checkValidity()) return;

        button.disabled = true;
        button.innerHTML =
            '<span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>' +
            'Updating...';
    });

    // Difference = physical quantity - system quantity (read only field).
    const systemQty = document.getElementById("SystemQuantity");
    const physicalQty = document.getElementById("PhysicalQuantity");
    const difference = document.getElementById("Difference");

    if (systemQty && physicalQty && difference) {
        const calculate = () => {
            const system = parseFloat(systemQty.value) || 0;
            const physical = parseFloat(physicalQty.value) || 0;
            difference.value = (physical - system).toFixed(2);
        };

        systemQty.addEventListener("input", calculate);
        physicalQty.addEventListener("input", calculate);
        calculate();
    }
});
