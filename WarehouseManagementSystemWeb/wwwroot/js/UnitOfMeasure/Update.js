document.addEventListener("DOMContentLoaded", function () {

    const form = document.getElementById("unitOfMeasureUpdateForm");
    const button = document.getElementById("updateUnitOfMeasureBtn");

    if (!form || !button) return;

    form.addEventListener("submit", function () {
        if (!form.checkValidity()) return;

        button.disabled = true;
        button.innerHTML =
            '<span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>' +
            'Updating...';
    });

    const status = document.getElementById("IsActive");
    const label = document.getElementById("statusLabel");

    if (status && label) {
        const updateLabel = () => label.textContent = status.checked ? "Active" : "Inactive";
        status.addEventListener("change", updateLabel);
        updateLabel();
    }
});
